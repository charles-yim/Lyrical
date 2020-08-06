using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lyrical
{
    public partial class MainForm : Form
    {
        string title = "";

        LyricsScraper lyricsScraper;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine(Properties.Settings.Default.refresh_token);
            lyricsScraper = new LyricsScraper(Properties.Settings.Default.refresh_token);
            UpdateFormTitle(lyricsScraper.GetSongName() + lyricsScraper.GetSongArtist());
            numFontSize.Value = decimal.Parse(txtLyricsBox.Font.Size.ToString());
        }

        private void UpdateFormTitle(string title)
        {
            if(title.Length > 0)
            {
                this.Text = "Lyrical: " + title;
            }
            else
            {
                this.Text = "Lyrical: Waiting for song...";
            }
        }

        private async void timerLyricsScraper_TickAsync(object sender, EventArgs e)
        {
            try
            {
                await lyricsScraper.Update();
                string newTitle = lyricsScraper.GetSongName() + " - " + lyricsScraper.GetSongArtist();
                if (!title.Equals(newTitle) || title.Equals(""))
                {
                    title = newTitle;
                    UpdateFormTitle(this.title);

                    Thread t = new Thread(() =>
                    {
                        string lyrics = lyricsScraper.ScrapeLyrics();
                        txtLyricsBox.Invoke((MethodInvoker)delegate {
                            txtLyricsBox.Text = lyrics;
                        });
                    });
                    txtLyricsBox.Text = "Fetching lyrics...";
                    t.IsBackground = true;
                    t.Start();
                }
            }
            catch (Microsoft.CSharp.RuntimeBinder.RuntimeBinderException)
            {
                UpdateFormTitle("Failed to get song... have you signed in?");
            }
        }

        private void btnUpdateOAuth_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            loginForm.FormClosed += new FormClosedEventHandler(LoginFormClosedHandlerAsync);
        }

        private async void LoginFormClosedHandlerAsync(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.Reload();
            lyricsScraper.UpdateRefreshToken(Properties.Settings.Default.refresh_token);
            lyricsScraper.UpdateAccessTokenAsync();
        }

        private void numFontSize_ValueChanged(object sender, EventArgs e)
        {
            txtLyricsBox.Font = new Font(txtLyricsBox.Font.FontFamily, float.Parse(numFontSize.Value.ToString()));
        }
    }
}
