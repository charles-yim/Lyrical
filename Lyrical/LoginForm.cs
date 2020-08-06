using Jil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Lyrical
{
    public partial class LoginForm : Form
    {

        private bool hasInit;
        

        public LoginForm()
        {
            InitializeComponent();
            hasInit = false;
        }

        private async void webBrowser1_DocumentCompletedAsync(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!hasInit) {
                hasInit = true;
                webBrowser1.Navigate("https://accounts.spotify.com/authorize?client_id=ea3a447f2aed418db7354d9ae538feb4&response_type=code&redirect_uri=http://localhost:8080/lyrical&scope=user-read-currently-playing");
                return;
            }
            string url = webBrowser1.Url.ToString();
            Console.WriteLine(url);
            if (url.Contains("http://localhost:8080/lyrical?code="))
            {
                string authorizationCode = url.Split('?')[1].Replace("code=", "");
                Console.WriteLine(authorizationCode);
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://accounts.spotify.com/api/token"))
                    {
                        request.Headers.TryAddWithoutValidation("Authorization", "Basic ZWEzYTQ0N2YyYWVkNDE4ZGI3MzU0ZDlhZTUzOGZlYjQ6ZDUyYThiN2JmZWVlNDE1MmFiOTFhODRkNzc4NjdlNWU=");

                        var contentList = new List<string>();
                        contentList.Add("grant_type=authorization_code");
                        contentList.Add("code=" + authorizationCode);
                        contentList.Add("redirect_uri=http://localhost:8080/lyrical");
                        request.Content = new StringContent(string.Join("&", contentList));
                        request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                        var response = await httpClient.SendAsync(request);
                        dynamic jsonResponse = JSON.DeserializeDynamic(await response.Content.ReadAsStringAsync());
                        Properties.Settings.Default.refresh_token = jsonResponse.refresh_token;
                        Properties.Settings.Default.Save();
                    }
                }
                this.Close();
            }
        }

    }

}
