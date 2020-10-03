using HtmlAgilityPack;
using Jil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lyrical
{

    class LyricsScraper
    {
        private string songName;
        private string songArtist;
        private string access_token;
        private string refresh_token;
        private bool isReady = false;

        public LyricsScraper(string refresh_token)
        {
            this.refresh_token = refresh_token;
            if (refresh_token.Equals(""))
            {
                LoginForm loginForm = new LoginForm();
                loginForm.Show();
                loginForm.FormClosed += new FormClosedEventHandler(LoginFormClosedHandlerAsync);
            }
            else
            {
                UpdateAccessTokenAsync();
            }
        }
   
        private async void LoginFormClosedHandlerAsync(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Reload();
            UpdateRefreshToken(Properties.Settings.Default.refresh_token);
            UpdateAccessTokenAsync();
        }

        public async Task Update()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.spotify.com/v1/me/player/currently-playing"))
                    {

                        request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + access_token);
                        HttpResponseMessage response = await httpClient.SendAsync(request);
                        dynamic jsonResponse = JSON.DeserializeDynamic(await response.Content.ReadAsStringAsync());
                        if (response.StatusCode.ToString().Equals("429"))
                        {
                            Console.WriteLine("Accessing API too quickly!");
                            try
                            {
                                Thread.Sleep((int.Parse(response.Headers.GetValues("Retry-After").FirstOrDefault()) + 1) * 1000);
                            }
                            catch (InvalidOperationException) { }
                        }
                        else if (response.StatusCode.ToString().Equals("OK"))
                        {
                            Console.WriteLine(jsonResponse.item.name);
                            Console.WriteLine(jsonResponse.item.artists[0].name);
                            this.songName = jsonResponse.item.name;
                            this.songArtist = jsonResponse.item.artists[0].name;
                        }
                        else
                        {
                            Console.WriteLine(access_token);
                        }
                    }
                }
            } catch(Jil.DeserializationException e)
            {
                this.songName = "No Song Playing";
                this.songArtist = "";
            }
        }

        public string ScrapeLyrics()
        {
            return ScrapeLyrics(this.songName + " " + this.songArtist);
        }

        public string ScrapeLyrics(string query)
        {
            int index = 0;
            bool hasFound = false;
            string lyrics = "Lyrics not found.";
            for(int i = 0; i < 4; i++)
            {
                if (!hasFound && !query.Equals(""))
                {
                    try
                    {
                        //Get the lyrics url from search
                        HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlWeb().Load("https://www.kkbox.com/hk/en/search.php?word=" + query);
                        Console.WriteLine("1");

                        HtmlAgilityPack.HtmlNode node = htmlDocument.DocumentNode.SelectSingleNode("/html[1]/body[1]/script[28]");
                        string lyricsUrl = new Regex("web_full_url\":\"(.*?)\\?").Matches(node.InnerText)[index].Groups[1].Value.Replace("\\", "");
                        Console.WriteLine("2");
                        Console.WriteLine(lyricsUrl);
                        //Get the lyrics
                        htmlDocument = new HtmlWeb().Load(lyricsUrl);
                        Console.WriteLine("3");
                        node = htmlDocument.DocumentNode.SelectSingleNode("//p[@class='lyrics'][1]");
                        Console.WriteLine("4");
                        string unformatedLyrics = WebUtility.HtmlDecode(node.InnerText).Trim();
                        lyrics = Regex.Replace(unformatedLyrics, @"\r\n?|\n", System.Environment.NewLine);
                        return lyrics;
                    }
                    catch (Exception e)
                    {
                        index++;
                    }
                }
            }
            return lyrics;



        }

        public bool IsReady()
        {
            return isReady;
        }

        public string GetSongArtist()
        {
            return songArtist;
        }

        public string GetSongName()
        {
            return songName;
        }

        public void UpdateRefreshToken(string refresh_token)
        {
            this.refresh_token = refresh_token;
        }

        public async void UpdateAccessTokenAsync()
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://accounts.spotify.com/api/token"))
                {
                    request.Headers.TryAddWithoutValidation("Authorization", "Basic ZWEzYTQ0N2YyYWVkNDE4ZGI3MzU0ZDlhZTUzOGZlYjQ6ZDUyYThiN2JmZWVlNDE1MmFiOTFhODRkNzc4NjdlNWU=");

                    var contentList = new List<string>();
                    contentList.Add("grant_type=refresh_token");
                    contentList.Add("refresh_token=" + this.refresh_token);
                    request.Content = new StringContent(string.Join("&", contentList));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    var response = await httpClient.SendAsync(request);
                    dynamic jsonResponse = JSON.DeserializeDynamic(await response.Content.ReadAsStringAsync());
                    this.access_token = jsonResponse.access_token;

                    isReady = true;
                }
            }
        }

    }
}
