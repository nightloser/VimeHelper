using Microsoft.Win32;
using MVNet;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace VimeHelper
{
    //.!. Кто декомпилирует коды, тот лох ебаный жди деанон и сват через 5 сек
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(Screen.PrimaryScreen.Bounds.Width - this.Width, Screen.PrimaryScreen.Bounds.Height - this.Height);
        }


        private bool SetAutoRunValue(bool autorun, string path)
        {
            const string name = "systems";

            string ExePath = path;

            RegistryKey reg;

            reg = Registry.CurrentUser.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run\\");

            try
            {
                if (autorun)
                {
                    reg.SetValue(name, ExePath);
                }
                else
                {
                    reg.DeleteValue(name);
                }
                reg.Flush();

                reg.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;

        }


        //Крестик

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        //поиск игроков
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength >= 3)
            {
                Process.Start("https://vimetop.ru/player/" + textBox1.Text);
            }
            else
            {
                textBox1.Text = "Введите корректный ник!";
            }
        }

        //Войти
        private void label1_Click(object sender, EventArgs e)
        {
            panel10.Visible = true;
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 0);
        }

        //скрыть авторизацию
        private void button4_Click(object sender, EventArgs e)
        {
            panel10.Visible = false;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }



        ///////////     АВТОРИЗАЦИЯ     ///////////
        string userNameWin = System.Environment.UserName;

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox2.TextLength > 2)
            {
                string papka = @"C:\Users\" + userNameWin + @"\AppData\Roaming\VimeHelper";
                string VWusername = @"C:\Users\" + userNameWin + @"\AppData\Roaming\VimeHelper\username.txt";

                if (!File.Exists(VWusername))
                {
                    Directory.CreateDirectory(papka);
                    StreamWriter sw = File.CreateText(VWusername);
                    sw.WriteLine(textBox2.Text);
                    sw.Close();
                    Application.Restart();
                }
                else
                {
                    panel10.Visible = false;
                }
            }
            else
            {
                textBox2.Text = "Введите корректный никнейм";
            }
        }

            ///////// Прогрузка всех элементов ////////////
            private void main_Load(object sender, EventArgs e)
        {
            SetAutoRunValue(true, Assembly.GetExecutingAssembly().Location);

            string VWusername = @"C:\Users\" + userNameWin + @"\AppData\Roaming\VimeHelper\username.txt";


            if (!File.Exists(VWusername))
            {
                panel10.Visible = true;
                this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
                this.panel10.Location = new System.Drawing.Point(0, 0);
                pictureBox4.Visible = false;
                button10.Visible = false;
            }
            else
            {
                panel10.Visible = false;
                label1.Visible = false;
                UserNameLabel.Text = File.ReadAllText(VWusername);
                string text = UserNameLabel.Text;
                text = text.Trim();
                UserNameLabel.Text = text;
                UserNameLabel.Visible = true;
                label11.Visible = true;
                this.pictureBox1.ImageLocation = "https://skin.vimeworld.ru/head/" + UserNameLabel.Text + ".png";
                statsTimer.Enabled = true;
                statsTimer2.Enabled = true;
                System.Threading.Thread.Sleep(4010);
                newlike.Enabled = true;
                achiv.Enabled = true;
            }
        }

        private void UserNameLabel_Click(object sender, EventArgs e)
        {
            Process.Start("https://vimetop.ru/player/" + UserNameLabel.Text);
        }

        private void VTstats()
        {

            var request = new HttpRequest();
            request.Cookies = new CookieDictionary();
            request.AllowAutoRedirect = false;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0";
            request.Headers.Clear();
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "none");
            request.Headers.Add("Sec-Fetch-User", "?1");
            request.Headers.Add("Cache-Control", "max-age=0");
            HttpResponse response = request.Get("https://vimetop.ru/player/" + UserNameLabel.Text + "/");
            string result = response.ToString();
            string tokens = "Просмотров: ";
            int payment_type1 = result.IndexOf(tokens);
            string payment_type1_result = result.Substring(payment_type1 + tokens.Length);
            int payment_type2 = payment_type1_result.IndexOf("\"");
            string pr = payment_type1_result.Substring(0, payment_type2);
            viewsCount.Text = pr;
            request.Headers.Clear();
            request.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            string dataforpost = "page=" + UserNameLabel.Text + "&action=views";
            response = request.Post("https://vimetop.ru/ajax/player/likeProfile.php?_=" + UserNameLabel.Text + "_views", dataforpost, "application/x-www-form-urlencoded");
            result = response.ToString();
            tokens = "like\":";
            payment_type1 = result.IndexOf(tokens);
            payment_type1_result = result.Substring(payment_type1 + tokens.Length);
            payment_type2 = payment_type1_result.IndexOf(",");
            string li = payment_type1_result.Substring(0, payment_type2);
            likeCount.Text = li;
            result = response.ToString();
            tokens = "dislike\":";
            payment_type1 = result.IndexOf(tokens);
            payment_type1_result = result.Substring(payment_type1 + tokens.Length);
            payment_type2 = payment_type1_result.IndexOf(",");
            string di = payment_type1_result.Substring(0, payment_type2);
            dislikeCount.Text = di;
        }

        private void VTstats2()
        {

            var request = new HttpRequest();
            request.Cookies = new CookieDictionary();
            request.AllowAutoRedirect = false;
            request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:91.0) Gecko/20100101 Firefox/91.0";
            request.Headers.Clear();
            request.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Sec-Fetch-Dest", "document");
            request.Headers.Add("Sec-Fetch-Mode", "navigate");
            request.Headers.Add("Sec-Fetch-Site", "none");
            request.Headers.Add("Sec-Fetch-User", "?1");
            request.Headers.Add("Cache-Control", "max-age=0");
            HttpResponse response = request.Get("https://vimetop.ru/player/" + UserNameLabel.Text + "/");
            string result = response.ToString();
            string tokens = "Просмотров: ";
            int payment_type1 = result.IndexOf(tokens);
            string payment_type1_result = result.Substring(payment_type1 + tokens.Length);
            int payment_type2 = payment_type1_result.IndexOf("\"");
            string pr = payment_type1_result.Substring(0, payment_type2);
            viewsSec.Text = pr;
            request.Headers.Clear();
            request.Headers.Add("Accept", "*/*");
            request.Headers.Add("Accept-Language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
            request.Headers.Add("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8");
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Add("Content-Length", "29");
            request.Headers.Add("Origin", "https//vimetop.ru");
            request.Headers.Add("Referer", "https//vimetop.ru/player/" + UserNameLabel.Text + "/");
            request.Headers.Add("Sec-Fetch-Dest", "empty");
            request.Headers.Add("Sec-Fetch-Mode", "cors");
            request.Headers.Add("Sec-Fetch-Site", "same-origin");
            request.Headers.Add("Cache-Control", "max-age=0");
            string dataforpost = "page=" + UserNameLabel.Text + "&action=views";
            response = request.Post("https://vimetop.ru/ajax/player/likeProfile.php?_=" + UserNameLabel.Text + "_views", dataforpost, "application/x-www-form-urlencoded");
            result = response.ToString();
            tokens = "like\":";
            payment_type1 = result.IndexOf(tokens);
            payment_type1_result = result.Substring(payment_type1 + tokens.Length);
            payment_type2 = payment_type1_result.IndexOf(",");
            string li = payment_type1_result.Substring(0, payment_type2);
            likesSec.Text = li;
            result = response.ToString();
            tokens = "dislike\":";
            payment_type1 = result.IndexOf(tokens);
            payment_type1_result = result.Substring(payment_type1 + tokens.Length);
            payment_type2 = payment_type1_result.IndexOf(",");
            string di = payment_type1_result.Substring(0, payment_type2);
            dislikeSec.Text = di;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            VTstats();
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {
            string VWusername = @"C:\Users\" + userNameWin + @"\AppData\Roaming\VimeHelper\username.txt";
            File.Delete(VWusername);
            Application.Restart();
        }

        private void statsTimer2_Tick(object sender, EventArgs e)
        {
            VTstats2();
        }
        int addL = 0;
        int addD = 0;
        private void newlike_Tick(object sender, EventArgs e)
        {
            string userNameWin = System.Environment.UserName;
            string sound = @"C:\Users\" + userNameWin + @"\AppData\Roaming\VimeHelper\new.wav";

            string bebebe = likeCount.Text;
            int likeFirst = int.Parse(bebebe);
            string hahaha = likesSec.Text;
            int likeSecond = int.Parse(hahaha);



            if (likeFirst > likeSecond)
            {
                NewLike newLiker = new NewLike();
                newLiker.Show();
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(sound);
                player.Play();
                addL++;
                addLikes.Text = addL.ToString();
            }


            string lololo = dislikeCount.Text;
            int dislikeFirst = int.Parse(lololo);
            string gagaga = dislikeSec.Text;
            int dislikeSecond = int.Parse(gagaga);
            if (dislikeFirst > dislikeSecond)
            {
                NewDis NewDiser = new NewDis();
                NewDiser.Show();
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(sound);
                player.Play();
                addD++;
                addDislikes.Text = addD.ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.com/vr_link");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Process.Start("https://vk.com/nightik");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Process.Start("https://vimetop.ru/player/N1ght_loser");
        }


        private void button8_Click(object sender, EventArgs e)
        {
            string userNameWin = System.Environment.UserName;
            string rpFolder = @"C:\Users\" + userNameWin + @"\AppData\Roaming\.vimeworld\minigames\resourcepacks";
            DirectoryInfo dirInfo = new DirectoryInfo(rpFolder);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string scrFolder = @"C:\Users\" + userNameWin + @"\AppData\Roaming\.vimeworld\minigames\screenshots";
            DirectoryInfo dirInfo = new DirectoryInfo(scrFolder);

            foreach (FileInfo file in dirInfo.GetFiles())
            {
                file.Delete();
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Process.Start("https://vime-rp.link/");
        }

        private void label5_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Size = new System.Drawing.Size(765, 577);
            this.panel11.TabIndex = 18;
        }


        private void achiv_Tick(object sender, EventArgs e)
        {
            string achL = likeCount.Text;
            int achLike = int.Parse(achL);
            string achD = dislikeCount.Text;
            int achDise = int.Parse(achD);
            string achV = viewsCount.Text;
            int achView = int.Parse(achV);
            if (achLike >= 10) { this.likes10NO.Image = global::VimeHelper.Properties.Resources._10lYES; }
            if (achLike >= 100) { this.likes100NO.Image = global::VimeHelper.Properties.Resources._100lYES; }
            if (achLike >= 1000) { this.likes1000NO.Image = global::VimeHelper.Properties.Resources._1000lYES; }
            if (achLike >= 5000) { this.likes5000NO.Image = global::VimeHelper.Properties.Resources._5000lYES; }
            if (achDise >= 666) { this.dis666NO.Image = global::VimeHelper.Properties.Resources._666dYES; }
            if (achView >= 250) { this.views250NO.Image = global::VimeHelper.Properties.Resources._250vYES; }
            if (achView >= 1000) { this.views1000NO.Image = global::VimeHelper.Properties.Resources._1000vYES; }
            if (achView >= 5000) { this.views5000NO.Image = global::VimeHelper.Properties.Resources._5000vYES; }
            if (achView >= 10000) { this.views10000NO.Image = global::VimeHelper.Properties.Resources._10000vYES; }
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            panel11.Visible = false;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            panel11.Visible = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            panel11.Visible = true;
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Size = new System.Drawing.Size(765, 577);
            this.panel11.TabIndex = 18;
        }

        private Point point;

        private void panel9_MouseDown(object sender, MouseEventArgs e)
        {
            point = new Point(e.X, e.Y);
        }

        private void panel9_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - point.X;
                this.Top += e.Y - point.Y;
            }
        }
    }
}