using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace VimeHelper
{
    public partial class NewLike : Form
    {
        int n;
        public NewLike()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 200;
            Opacity = 0;
            Timer timer = new Timer();
            timer.Tick += new EventHandler((sender, e) =>
            {
                if ((Opacity += 0.05d) == 1) timer.Stop();
            });
            timer.Interval = 50;
            timer.Start();
            int n = 0;
            timer1.Start();
            string VWusername = @"C:\Users\" + userNameWin + @"\AppData\Roaming\VimeHelper\username.txt";
            PlayerName.Text = File.ReadAllText(VWusername);
            string text = PlayerName.Text;
            text = text.Trim();
            PlayerName.Text = text;
        }
        string userNameWin = System.Environment.UserName;

        private void timer1_Tick(object sender, EventArgs e)
        {
            n++;
            if (n == 80)
            {
                timer1.Stop();
                this.Close();
            }
        }

        private void viewsCount_Click(object sender, EventArgs e)
        {
            Process.Start("https://vimetop.ru/player/" + PlayerName.Text);
        }

        private void NewLike_Click(object sender, EventArgs e)
        {
            Process.Start("https://vimetop.ru/player/" + PlayerName.Text);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Process.Start("https://vimetop.ru/player/" + PlayerName.Text);
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            Process.Start("https://vimetop.ru/player/" + PlayerName.Text);
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            Process.Start("https://vimetop.ru/player/" + PlayerName.Text);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start("https://vimetop.ru/player/" + PlayerName.Text);
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Process.Start("https://vimetop.ru/player/" + PlayerName.Text);
        }
    }
}
