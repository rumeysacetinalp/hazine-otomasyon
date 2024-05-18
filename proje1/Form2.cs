using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace proje1
{

    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            this.DoubleBuffered = true;
           
        }

       
        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
    }

    public partial class Form2 : Form
    {
        private const int MinBirimBoyutu = 5;
        private const int MaxBirimBoyutu = 50;
        private int SabitEngelAdedi = 1;
        private int SandıkAdedi = 5;
        private int HareketliEngelAdedi = 1;

        private System.Windows.Forms.Timer hareketTimer;

        private int haritaBoyutu;
        private int birimBoyutu;

        private int hareketYonu = 1; // 1 Aşağı, -1 Yukarı
        private int hareketYonuAri = 1;

        private List<HareketliEngel> hareketliengeller = new List<HareketliEngel>();
        private List<SabitEngel> sabitEngeller = new List<SabitEngel>();
        private List<HazineSandıkları> hazinesandıkları = new List<HazineSandıkları>();
        private List<Engel> engeller = new List<Engel>();
        private List<Karakter> karakter = new List<Karakter>();
        private List<Lokasyon> gecilenKonumlar = new List<Lokasyon>();
        private List<Point> kusGecisKonumlari = new List<Point>();
        private List<Point> ariGecisKonumlari = new List<Point>();



        private int[,] engelMatris;
      

        public Form2(int boyut)
        {
            InitializeComponent();
           
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

            haritaBoyutu = Math.Max(boyut, 10);
            birimBoyutu = Math.Max(ClientSize.Width / haritaBoyutu, MinBirimBoyutu);

            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

            timer1.Enabled = true; 
            timer1.Interval = 1000; 
            timer1.Tick += Timer1_Tick;

            engelMatris = new int[haritaBoyutu, haritaBoyutu];
                   


            InitializeGrid();
            Engelyerleştir();


            InitializeComponent();

           
        }

        private void InitializeGrid()
        {

            DoubleBufferedPanel panel = new DoubleBufferedPanel
            {
                Width = haritaBoyutu * birimBoyutu,
                Height = haritaBoyutu * birimBoyutu,
                Location = new Point(10, 10),
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true


            };

            panel.Paint += Panel_Paint;

            Controls.Add(panel);


            Button başlat = new Button
            {
                Text = "Başlat",
                Location = new Point(0,0),
                Size = new Size(70, 30),
                BackColor = Color.FromArgb(50, Color.Black)
            };
            başlat.Click += (sender, e) => Başlat_Click(sender, e, panel);
            panel.Controls.Add(başlat);

            Button button = new Button
            {
                Text = "Yeni Harita",
                Location = new Point(72, 0),
                Size = new Size(70, 30),
                BackColor = Color.FromArgb(50, Color.Black)
            };
            button.Click += Button_Click;
            panel.Controls.Add(button);

        }
 

        private void Panel_Paint(object sender, PaintEventArgs e)
        {
           
            string imagePath = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\arkaplan.jpg";
            Image backgroundImage = Image.FromFile(imagePath);
            e.Graphics.DrawImage(backgroundImage, new Rectangle(0, 0, haritaBoyutu * birimBoyutu, haritaBoyutu * birimBoyutu));

            Pen pen = new Pen(Color.Black);
            Graphics g = e.Graphics;

            for (int i = 0; i <= haritaBoyutu; i++)
            {
                int x = i * birimBoyutu;
                int y = i * birimBoyutu;

                e.Graphics.DrawLine(pen, x, 0, x, haritaBoyutu * birimBoyutu);
                e.Graphics.DrawLine(pen, 0, y, haritaBoyutu * birimBoyutu, y);
            }

            


            foreach (Lokasyon konum in gecilenKonumlar)
            {
                Brush brush = new SolidBrush(Color.Green);
                e.Graphics.FillRectangle(brush, konum.X * birimBoyutu, konum.Y * birimBoyutu, birimBoyutu, birimBoyutu);
            }

            foreach (Point konum in kusGecisKonumlari)
            {
                Rectangle rect = new Rectangle(konum.X * birimBoyutu, konum.Y * birimBoyutu, birimBoyutu, birimBoyutu);
                Rectangle rect2 = new Rectangle(((konum.X) + 1) * birimBoyutu, (konum.Y) * birimBoyutu, birimBoyutu, birimBoyutu);
                e.Graphics.FillRectangle(Brushes.Red, rect);
                e.Graphics.FillRectangle((Brush)Brushes.Red, rect2);
            }

            foreach (Point konum in ariGecisKonumlari)
            {
                Rectangle rect = new Rectangle(konum.X * birimBoyutu, konum.Y * birimBoyutu, birimBoyutu, birimBoyutu);
                Rectangle rect2 = new Rectangle((konum.X) * birimBoyutu, ((konum.Y) + 1) * birimBoyutu, birimBoyutu, birimBoyutu);
                e.Graphics.FillRectangle(Brushes.Red, rect);
                e.Graphics.FillRectangle(Brushes.Red, rect2);
            }

            foreach (SabitEngel s_engel in sabitEngeller)
            {
                CizimiYap(e.Graphics, s_engel);
            }

            

            foreach (HareketliEngel h_engel in hareketliengeller)
            {
                CizimiYapHareketli(e.Graphics, h_engel);
            }

            foreach (Hazine_sandıkları h_sandıkları in hazinesandıkları)
            {

                SandıkCizimiYap(e.Graphics, h_sandıkları);
            }

            

            foreach (KarakterÖzellik karakter_Ö in karakter)
            {
                karakterCizimiYap(e.Graphics, karakter_Ö);
            }



            // Sis için kaldırılabilir ......

            for (int x = 0; x < haritaBoyutu; x++)
            {
                for (int y = 0; y < haritaBoyutu; y++)
                {
                    if (gecilenKonumlar.Any(l => Math.Abs(l.X - x) <= 3 && Math.Abs(l.Y - y) <= 3))
                    {
                        
                        SolidBrush brush = new SolidBrush(Color.FromArgb(0, Color.Black));
                        g.FillRectangle(brush, x * birimBoyutu, y * birimBoyutu, birimBoyutu, birimBoyutu);
                    }
                    else
                    {
                        
                        SolidBrush brush = new SolidBrush(Color.FromArgb(240, Color.Black));
                        g.FillRectangle(brush, x * birimBoyutu, y * birimBoyutu, birimBoyutu, birimBoyutu);
                    }
                }
            }



        }



        private void Button_Click(object sender, EventArgs e)
        {


            Form1 form1 = new Form1();

            form1.Show();
            this.Hide();



        }
        private void Başlat_Click(object sender, EventArgs e, Panel panel)
        {

            hareketTimer = new System.Windows.Forms.Timer();
            hareketTimer.Interval = 150;
            hareketTimer.Tick += HareketTimer_Tick;

            hareketTimer.Start();
           
        }





        // Hareketli engel çizimi
        private void CizimiYapHareketli(Graphics g, HareketliEngel h_engel)
        {
            if (h_engel.Kus != null)
            {
                

                Image KusResmi = Image.FromFile(h_engel.Kus.KusResimYolu);
                g.DrawImage(KusResmi, h_engel.Konum.X * birimBoyutu, h_engel.Konum.Y * birimBoyutu, h_engel.Kus.KusBoyut * birimBoyutu, h_engel.Kus.KusBoyut * birimBoyutu);
            }
            if (h_engel.Ari != null)
            {
                Image AriResmi = Image.FromFile(h_engel.Ari.AriResimYolu);
                g.DrawImage(AriResmi, h_engel.Konum.X * birimBoyutu, h_engel.Konum.Y * birimBoyutu, h_engel.Ari.AriBoyut * birimBoyutu, h_engel.Ari.AriBoyut * birimBoyutu);
            }
        }


        //KArakter Çizimi >>>>>>>-------------<<<<<<<<
        private void karakterCizimiYap(Graphics g, KarakterÖzellik karakter_Ö)
        {
            if (karakter_Ö.Karakter != null)
            {


                Image karakterResmi = Image.FromFile(karakter_Ö.Karakter.karakterResimYolu);
                g.DrawImage(karakterResmi, karakter_Ö.Konum.X * birimBoyutu, karakter_Ö.Konum.Y * birimBoyutu, karakter_Ö.Karakter.karakterBoyut * birimBoyutu, karakter_Ö.Karakter.karakterBoyut * birimBoyutu);
            }




        }

        // SAndıkların çizimi >>
        private void SandıkCizimiYap(Graphics g, Hazine_sandıkları h_sandıkları)
        {
            if (h_sandıkları.AltınSandık != null)
            {

                Image altınsandıkResmi = Image.FromFile(h_sandıkları.AltınSandık.altınResimYolu);
                g.DrawImage(altınsandıkResmi, h_sandıkları.Konum.X * birimBoyutu, h_sandıkları.Konum.Y * birimBoyutu, h_sandıkları.AltınSandık.altınBoyut * birimBoyutu, h_sandıkları.AltınSandık.altınBoyut * birimBoyutu);
            }

            if (h_sandıkları.GümüşSandık != null)
            {
                Image gümüşsandıkResmi = Image.FromFile(h_sandıkları.GümüşSandık.gümüşResimYolu);
                g.DrawImage(gümüşsandıkResmi, h_sandıkları.Konum.X * birimBoyutu, h_sandıkları.Konum.Y * birimBoyutu, h_sandıkları.GümüşSandık.gümüşBoyut * birimBoyutu, h_sandıkları.GümüşSandık.gümüşBoyut * birimBoyutu);
            }

            if (h_sandıkları.BronzSandık != null)
            {
                Image bronzsandıkResmi = Image.FromFile(h_sandıkları.BronzSandık.bronzResimYolu);
                g.DrawImage(bronzsandıkResmi, h_sandıkları.Konum.X * birimBoyutu, h_sandıkları.Konum.Y * birimBoyutu, h_sandıkları.BronzSandık.bronzBoyut * birimBoyutu, h_sandıkları.BronzSandık.bronzBoyut * birimBoyutu);
            }
            if (h_sandıkları.ZümrütSandık != null)
            {
                Image zümrütsandıkResmi = Image.FromFile(h_sandıkları.ZümrütSandık.zümrütResimYolu);
                g.DrawImage(zümrütsandıkResmi, h_sandıkları.Konum.X * birimBoyutu, h_sandıkları.Konum.Y * birimBoyutu, h_sandıkları.ZümrütSandık.zümrütBoyut * birimBoyutu, h_sandıkları.ZümrütSandık.zümrütBoyut * birimBoyutu);
            }
        }

        // Sabit engellerin çizimi >>>
        private void CizimiYap(Graphics g, SabitEngel s_engel)
        {

            if (s_engel.YazAğaç != null)
            {
                Image agacResmi = Image.FromFile(s_engel.YazAğaç.AğaçResimYolu);
                g.DrawImage(agacResmi, s_engel.Konum.X * birimBoyutu, s_engel.Konum.Y * birimBoyutu, s_engel.YazAğaç.AğaçBoyut * birimBoyutu, s_engel.YazAğaç.AğaçBoyut * birimBoyutu);
            }

            if (s_engel.YazTaş != null)
            {
                Image taşResmi = Image.FromFile(s_engel.YazTaş.taşResimYolu);

                g.DrawImage(taşResmi, s_engel.Konum.X * birimBoyutu, s_engel.Konum.Y * birimBoyutu, s_engel.YazTaş.TaşBoyut * birimBoyutu, s_engel.YazTaş.TaşBoyut * birimBoyutu);
            }
            if (s_engel.KışAğaç != null)
            {
                Image kışagacResmi = Image.FromFile(s_engel.KışAğaç.AğaçResimYolu);
                g.DrawImage(kışagacResmi, s_engel.Konum.X * birimBoyutu, s_engel.Konum.Y * birimBoyutu, s_engel.KışAğaç.AğaçBoyut * birimBoyutu, s_engel.KışAğaç.AğaçBoyut * birimBoyutu);
            }
            if (s_engel.KışTaş != null)
            {
                Image kıştaşResmi = Image.FromFile(s_engel.KışTaş.taşResimYolu);
                g.DrawImage(kıştaşResmi, s_engel.Konum.X * birimBoyutu, s_engel.Konum.Y * birimBoyutu, s_engel.KışTaş.TaşBoyut * birimBoyutu, s_engel.KışTaş.TaşBoyut * birimBoyutu);
            }

            if (s_engel.KışDuvar != null)
            {
                Image duvar = Image.FromFile(s_engel.KışDuvar.DuvarResimYolu);
                g.DrawImage(duvar, s_engel.Konum.X * birimBoyutu, s_engel.Konum.Y * birimBoyutu, s_engel.KışDuvar.DuvarBoyutX * birimBoyutu, s_engel.KışDuvar.DuvarBoyutY * birimBoyutu);
            }
            if (s_engel.YazDuvar != null)
            {
                Image duvar = Image.FromFile(s_engel.YazDuvar.DuvarResimYolu);
                g.DrawImage(duvar, s_engel.Konum.X * birimBoyutu, s_engel.Konum.Y * birimBoyutu, s_engel.YazDuvar.DuvarBoyutX * birimBoyutu, s_engel.YazDuvar.DuvarBoyutY * birimBoyutu);
            }

            if (s_engel.Yazdag != null)
            {
                Image yazdag = Image.FromFile(s_engel.Yazdag.dagResimYolu);
                g.DrawImage(yazdag, s_engel.Konum.X * birimBoyutu, s_engel.Konum.Y * birimBoyutu, s_engel.Yazdag.dagBoyut * birimBoyutu, s_engel.Yazdag.dagBoyut * birimBoyutu);
            }
            if (s_engel.KışDag != null)
            {
                Image kışdag = Image.FromFile(s_engel.KışDag.dagResimYolu);
                g.DrawImage(kışdag, s_engel.Konum.X * birimBoyutu, s_engel.Konum.Y * birimBoyutu, s_engel.KışDag.dagBoyut * birimBoyutu, s_engel.KışDag.dagBoyut * birimBoyutu);
            }



        }


        private void Engelyerleştir()
        {
            int adet = (SabitEngelAdedi) * haritaBoyutu / 30;

            SabitEngelAdedi = (SabitEngelAdedi) * haritaBoyutu / 50;



            engelMatris = new int[haritaBoyutu, haritaBoyutu];
            Random random = new Random();

            for (int i = 0; i < adet; i++) //Yaz ağaç
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSag(random);
                SabitEngel s_engel = new SabitEngel(randomKonum);

                if (!EngelVarMi(randomKonum, s_engel.BoyutX, s_engel.BoyutY))
                {
                    // Çakışma varsa, konumu güncelle ve tekrar kontrol et


                    sabitEngeller.Add(s_engel);
                    MarkEngelBolgesi(randomKonum, s_engel.BoyutX, s_engel.BoyutY);
                }
                else
                {
                    // Eğer bu konumda engel varsa farklı bir konum seç
                    i--;
                }

            }

            for (int i = 0; i < adet; i++) //Yaz Taş
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSag(random);
                SabitEngel s_engel = new SabitEngel(randomKonum, 1.0);

                if (!EngelVarMi(randomKonum, s_engel.BoyutX, s_engel.BoyutY))
                {


                    sabitEngeller.Add(s_engel);
                    MarkEngelBolgesi(randomKonum, s_engel.BoyutX, s_engel.BoyutY);
                }
                else
                {
                    // Eğer bu konumda engel varsa farklı bir konum seç
                    i--;
                }


            }


            for (int i = 0; i < adet; i++) //Kış ağaç
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSol(random);
                SabitEngel s_engel = new SabitEngel(randomKonum, 1);

                if (!EngelVarMi(randomKonum, s_engel.BoyutX, s_engel.BoyutY))
                {


                    sabitEngeller.Add(s_engel);
                    MarkEngelBolgesi(randomKonum, s_engel.BoyutX, s_engel.BoyutY);
                }
                else
                {
                    // Eğer bu konumda engel varsa farklı bir konum seç
                    i--;
                }


            }

            for (int i = 0; i < adet; i++) //Kış taş
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSol(random);
                SabitEngel s_engel = new SabitEngel(randomKonum, 10, 1, 'a');

                if (!EngelVarMi(randomKonum, s_engel.BoyutX, s_engel.BoyutY))
                {


                    sabitEngeller.Add(s_engel);
                    MarkEngelBolgesi(randomKonum, s_engel.BoyutX, s_engel.BoyutY);
                }
                else
                {
                    // Eğer bu konumda engel varsa farklı bir konum seç
                    i--;
                }

            }

            for (int i = 0; i < adet; i++) // Kış duvar
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSol(random);
                SabitEngel s_engel = new SabitEngel(randomKonum, 'a');

                if (!EngelVarMi(randomKonum, s_engel.BoyutX, s_engel.BoyutY))
                {


                    sabitEngeller.Add(s_engel);
                    MarkEngelBolgesi(randomKonum, s_engel.BoyutX, s_engel.BoyutY);
                }
                else
                {
                    // Eğer bu konumda engel varsa farklı bir konum seç
                    i--;
                }


            }


            for (int i = 0; i < adet; i++) // Yaz duvar
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSag(random);
                SabitEngel s_engel = new SabitEngel(randomKonum, "duvar");

                if (!EngelVarMi(randomKonum, s_engel.BoyutX, s_engel.BoyutY))
                {

                    sabitEngeller.Add(s_engel);
                    MarkEngelBolgesi(randomKonum, s_engel.BoyutX, s_engel.BoyutY);
                }
                else
                {
                    // Eğer bu konumda engel varsa farklı bir konum seç
                    i--;
                }

            }

            // SabitEngelAdedi = (SabitEngelAdedi) * haritaBoyutu / 50;

            for (int i = 0; i < SabitEngelAdedi; i++) // Yaz dag
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSag(random);
                SabitEngel s_engel = new SabitEngel(randomKonum, 1, 'a', 'a');

                if (!EngelVarMi(randomKonum, s_engel.BoyutX, s_engel.BoyutY))
                {

                    sabitEngeller.Add(s_engel);
                    MarkEngelBolgesi(randomKonum, s_engel.BoyutX, s_engel.BoyutY);

                }
                else
                {
                    // Eğer bu konumda engel varsa farklı bir konum seç
                    i--;
                }


            }

            for (int i = 0; i < SabitEngelAdedi; i++) // Kış dag
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSol(random);
                SabitEngel s_engel = new SabitEngel(randomKonum, 'a', 1);

                if (!EngelVarMi(randomKonum, s_engel.BoyutX, s_engel.BoyutY))
                {

                    sabitEngeller.Add(s_engel);
                    MarkEngelBolgesi(randomKonum, s_engel.BoyutX, s_engel.BoyutY);
                }
                else
                {
                    // Eğer bu konumda engel varsa farklı bir konum seç
                    i--;
                }


            }


            // SAndık Engelleri >>>>>>>>>>>-------------------------------------------<<<<<<<<<<<<<<<<<<<<

            for (int i = 0; i < SandıkAdedi; i++) // Altın SAndık
            {
                Lokasyon randomKonum = RastgeleKonumOlustur(random);

                Hazine_sandıkları h_sandıkları = new Hazine_sandıkları(randomKonum, 1, "Altın");

                if (!EngelVarMi(randomKonum, h_sandıkları.BoyutX, h_sandıkları.BoyutY))
                {

                    hazinesandıkları.Add(h_sandıkları);
                    MarkEngelBolgesi(randomKonum, h_sandıkları.BoyutX, h_sandıkları.BoyutY);
                }
                else
                {
                    i--;
                }
            }

            for (int i = 0; i < SandıkAdedi; i++) // Gümüş SAndık
            {
                Lokasyon randomKonum = RastgeleKonumOlustur(random);

                Hazine_sandıkları h_sandıkları = new Hazine_sandıkları(randomKonum, 'a', "Gümüş");

                if (!EngelVarMi(randomKonum, h_sandıkları.BoyutX, h_sandıkları.BoyutY))
                {

                    hazinesandıkları.Add(h_sandıkları);
                    MarkEngelBolgesi(randomKonum, h_sandıkları.BoyutX, h_sandıkları.BoyutY);
                }
                else
                {
                    i--;
                }
            }

            for (int i = 0; i < SandıkAdedi; i++) // Bronz SAndık
            {
                Lokasyon randomKonum = RastgeleKonumOlustur(random);

                Hazine_sandıkları h_sandıkları = new Hazine_sandıkları(randomKonum, "Bronz");

                if (!EngelVarMi(randomKonum, h_sandıkları.BoyutX, h_sandıkları.BoyutY))
                {

                    hazinesandıkları.Add(h_sandıkları);
                    MarkEngelBolgesi(randomKonum, h_sandıkları.BoyutX, h_sandıkları.BoyutY);
                }
                else
                {
                    i--;
                }
            }

            for (int i = 0; i < SandıkAdedi; i++) // Zümrüt SAndık
            {
                Lokasyon randomKonum = RastgeleKonumOlustur(random);

                Hazine_sandıkları h_sandıkları = new Hazine_sandıkları(randomKonum, 1, 'z', "Zümrüt");

                if (!EngelVarMi(randomKonum, h_sandıkları.BoyutX, h_sandıkları.BoyutY))
                {

                    hazinesandıkları.Add(h_sandıkları);
                    MarkEngelBolgesi(randomKonum, h_sandıkları.BoyutX, h_sandıkları.BoyutY);
                }
                else
                {
                    i--;

                }
            }

            for (int i = 0; i < 1; i++) // KArkater
            {
                Lokasyon randomKonum = RastgeleKonumOlustur(random);

                KarakterÖzellik karakter_Ö = new KarakterÖzellik(randomKonum, 1);

                if (!EngelVarMi(randomKonum, karakter_Ö.BoyutX, karakter_Ö.BoyutY))
                {

                    karakter.Add(karakter_Ö);
                    MarkEngelBolgesi(randomKonum, karakter_Ö.BoyutX, karakter_Ö.BoyutY);
                }
                else
                {
                    i--;

                }
            }


            //Hareketliler 

            for (int i = 0; i < HareketliEngelAdedi; i++) // Arı 
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSag(random);

                HareketliEngel h_engel = new HareketliEngel(randomKonum, 'b');

                if (!EngelVarMi(randomKonum,9, 2))
                {

                    hareketliengeller.Add(h_engel);
                    MarkEngelBolgesi(randomKonum,9,2);
                }
                else
                {
                    i--;

                }
            }

            for (int i = 0; i < HareketliEngelAdedi; i++) // Arı 
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSol(random);

                HareketliEngel h_engel = new HareketliEngel(randomKonum, 'b');

                if (!EngelVarMi(randomKonum, 9, 2))
                {

                    hareketliengeller.Add(h_engel);
                    MarkEngelBolgesi(randomKonum, 9, 2);
                }
                else
                {
                    i--;

                }
            }


            for (int i = 0; i < HareketliEngelAdedi; i++) //kuş 
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSag(random);

                HareketliEngel h_engel = new HareketliEngel(randomKonum);

                if (!EngelVarMi(randomKonum,2, 12))
                {

                    hareketliengeller.Add(h_engel);
                    MarkEngelBolgesi(randomKonum,2, 12);
                }
                else
                {
                    i--;

                }
            }
            for (int i = 0; i < HareketliEngelAdedi; i++) //kuş 
            {
                Lokasyon randomKonum = RastgeleKonumOlusturSol(random);

                HareketliEngel h_engel = new HareketliEngel(randomKonum);

                if (!EngelVarMi(randomKonum, 2, 12))
                {

                    hareketliengeller.Add(h_engel);
                    MarkEngelBolgesi(randomKonum, 2, 12);
                }
                else
                {
                    i--;

                }
            }

            // Debug Ekranına Matrisi yazdırmak için AÇılacak ... 

            // DebugMatrisYazdir();
           
              RefreshPanel();
        }




        private void DebugMatrisYazdir()
        {
            Debug.WriteLine("Engel Matrisi:");
            for (int i = 0; i < haritaBoyutu; i++)
            {
                for (int j = 0; j < haritaBoyutu; j++)
                {
                    Debug.Write($"{engelMatris[i, j]} ");
                }
                Debug.WriteLine("");
            }
        }

        private void RefreshPanel()
        {
            Panel panel = (Panel)Controls[0];
            panel.Size = new Size(haritaBoyutu * birimBoyutu, haritaBoyutu * birimBoyutu);
            panel.Location = new Point(10, 10);


           

            this.ClientSize = new Size(panel.Right + 20, panel.Bottom + 40);


            panel.Invalidate();
        }
        private void MarkEngelBolgesi(Lokasyon konum, int boyutX, int boyutY)
        {
            for (int i = konum.X; i < konum.X + boyutX; i++)
            {
                for (int j = konum.Y; j < konum.Y + boyutY; j++)
                {
                    engelMatris[i, j] = 1;
                }
            }
        }



        private bool EngelVarMi(Lokasyon konum, int boyutX, int boyutY)
        {

            for (int i = konum.X; i < konum.X + boyutX; i++)
            {
                for (int j = konum.Y; j < konum.Y + boyutY; j++)
                {
                    if (engelMatris[i, j] == 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }




        private Lokasyon RastgeleKonumOlusturSag(Random random)
        {

            int x = random.Next(haritaBoyutu / 2 + 15, haritaBoyutu - 15);
            int y = random.Next(15, haritaBoyutu - 15);


            return new Lokasyon(x, y);

        }

        private Lokasyon RastgeleKonumOlusturSol(Random random)
        {

            int x = random.Next(15, haritaBoyutu / 2 - 15);
            int y = random.Next(15, haritaBoyutu - 15);



            return new Lokasyon(x, y);
        }

        private Lokasyon RastgeleKonumOlustur(Random random)
        {

            int x = random.Next(1, haritaBoyutu-1);
            int y = random.Next(1, haritaBoyutu-1);

            return new Lokasyon(x, y);
        }

        private void HareketliEngelHareketEttirKus()
        {
            

            foreach (HareketliEngel h_engel in hareketliengeller)
            {
                if (h_engel.Kus != null)
                {

                    int baslangicY = h_engel.BaslangicY;
                    int bitisY = h_engel.BitisY;

                    h_engel.Konum.Y += hareketYonu;


                    if (h_engel.Konum.Y <= baslangicY || h_engel.Konum.Y >= bitisY)
                    {
                        hareketYonu *= -1; 
                        h_engel.Konum.Y += hareketYonu;
                        
                    }
                    kusGecisKonumlari.Add(new Point(h_engel.Konum.X, h_engel.Konum.Y));
                }
            }
        }
        private void HareketliEngelHareketEttirAri()
        {
           
            foreach (HareketliEngel h_engel in hareketliengeller)
            {
                if (h_engel.Ari != null)
                {

                    int baslangicX = h_engel.BaslangicX;
                    int bitisX = h_engel.BitisX;
                 
                    h_engel.Konum.X += hareketYonuAri;
             
                    if (h_engel.Konum.X <= baslangicX || h_engel.Konum.X >= bitisX)
                    {
                        hareketYonuAri *= -1; 
                        h_engel.Konum.X += hareketYonuAri;
                        
                    }
                    ariGecisKonumlari.Add(new Point(h_engel.Konum.X, h_engel.Konum.Y));
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            HareketliEngelHareketEttirKus();
            HareketliEngelHareketEttirAri();
            RefreshPanel();
        }


    

      private Lokasyon HedefKonumuBelirle(Lokasyon karakterKonum)
        {
            Lokasyon enYakinHedef = EnYakinHazineSandigiBul(karakterKonum, 3);


            if (enYakinHedef == null)
            {
                return RastgeleYonBelirle(karakterKonum);
            }

            return enYakinHedef;
        }

        private Lokasyon EnYakinHazineSandigiBul(Lokasyon karakterKonum, int aramaMesafesi)
        {
            Lokasyon enYakinHedef = null;
            double enKisaMesafe = double.MaxValue;



            for (int i = karakterKonum.X - aramaMesafesi; i <= karakterKonum.X + aramaMesafesi; i++)
            {
                for (int j = karakterKonum.Y - aramaMesafesi; j <= karakterKonum.Y + aramaMesafesi; j++)
                {

                    if (i >= 0 && i < haritaBoyutu && j >= 0 && j < haritaBoyutu)
                    {

                        Lokasyon hedefKonum = new Lokasyon(i, j);
                        AStar astar = new AStar(engelMatris, karakterKonum, hedefKonum);
                        double mesafe = astar.HesaplaManhattanMesafe(karakterKonum, hedefKonum);


                        if (mesafe < enKisaMesafe && HazineSandigiMi(hedefKonum))
                        {
                            enKisaMesafe = mesafe;
                            enYakinHedef = hedefKonum;
                        }
                    }
                }
            }

            return enYakinHedef;
        }

        private bool HazineSandigiMi(Lokasyon konum)
        {

            foreach (var sandik in hazinesandıkları)
            {
                if (sandik.Konum.X == konum.X && sandik.Konum.Y == konum.Y && !sandik.Toplandı)
                {
                    return true;
                }
            }
            return false;
        }

        private Lokasyon RastgeleYonBelirle(Lokasyon karakterKonum)
        {
            Random random = new Random();
            int rastgeleYon = random.Next(4);

            Lokasyon hedefKonum = new Lokasyon(karakterKonum.X, karakterKonum.Y);
            switch (rastgeleYon)
            {
                case 0:
                    if (karakterKonum.X < haritaBoyutu - 1) 
                        hedefKonum.X++;
                    break;
                case 1:
                    if (karakterKonum.X > 0)
                        hedefKonum.X--;
                    break;
                case 2:
                    if (karakterKonum.Y > 0) 
                        hedefKonum.Y--;
                    break;
                case 3:
                    if (karakterKonum.Y < haritaBoyutu - 1) 
                        hedefKonum.Y++;
                    break;
                default:
                    break;
            }

            return hedefKonum;
        }

        private void HareketEt(int deltaX, int deltaY)
        {
            int sandıktoplandı = 0;

            int yeniX = karakter[0].Konum.X + deltaX;
            int yeniY = karakter[0].Konum.Y + deltaY;


            if (yeniX >= 0 && yeniX < haritaBoyutu && yeniY >= 0 && yeniY < haritaBoyutu)
            {

                gecilenKonumlar.Add(new Lokasyon(yeniX, yeniY));


                foreach (var sandık in hazinesandıkları)
                {
                    if (sandık.Konum.X == yeniX && sandık.Konum.Y == yeniY && !sandık.Toplandı)
                    {

                        sandık.Topla();

                        switch (sandık.Tür)
                        {
                            case "Altın":

                                Debug.WriteLine($"Altın sandık toplandı! ({yeniX},{yeniY}-{sandık.Konum.X},{sandık.Konum.Y}) konumunda bulundu.");

                                break;
                            case "Gümüş":
                                Debug.WriteLine($"Gümüş sandık toplandı! ({yeniX},{yeniY}-{sandık.Konum.X},{sandık.Konum.Y}) konumunda bulundu.");
                                break;
                            case "Bronz":
                                Debug.WriteLine($"Bronz sandık toplandı! ({yeniX},{yeniY}-{sandık.Konum.X},{sandık.Konum.Y}) konumunda bulundu.");
                                break;
                            case "Zümrüt":
                                Debug.WriteLine($"Zümrüt sandık toplandı! ({yeniX},{yeniY}-{sandık.Konum.X},{sandık.Konum.Y}) konumunda bulundu.");
                                break;
                            default:
                                break;
                        }
                       
                        for (int i = sandık.Konum.X; i < sandık.Konum.X + 1; i++)
                        {
                            for (int j = sandık.Konum.Y; j < sandık.Konum.Y + 1; j++)
                            {
                                engelMatris[i, j] = 0;
                            }

                             
                        }

                        hazinesandıkları.Remove(sandık);
                        sandıktoplandı += sandıktoplandı + 1;
                        if (sandıktoplandı == 20)
                        {
                            Debug.WriteLine("Tebrikler Tüm Sandıkları topladınız.. :) ");
                            this.Close();
                        }
                        RefreshPanel();
                        return;
                    }
                }


                if (!EngelVarMi(new Lokasyon(yeniX, yeniY), 1, 1)) // 1*1 engel kontorlü
                {

                    karakter[0].Konum = new Lokasyon(yeniX, yeniY);

                    RefreshPanel();

                }
            }
            else
            {           
                Debug.WriteLine("Karakter harita sınırlarına ulaştı.");
            }
        }


        private void StartAutoMovement()
        {
            Lokasyon karakterKonum = karakter[0].Konum;
            Lokasyon hedefKonum = HedefKonumuBelirle(karakterKonum);


            if (hedefKonum != null)
            {
                AStar aStar = new AStar(engelMatris, karakterKonum, hedefKonum);
                List<Lokasyon> yol = aStar.YoluBul();

                if (yol != null && yol.Count > 1)
                {

                  
                    for (int i = 1; i < yol.Count; i++)
                    {
                      
                        List<Lokasyon> gecilenYol = new List<Lokasyon>();

                        Lokasyon adim = yol[i];
                        int deltaX = adim.X - karakter[0].Konum.X;
                        int deltaY = adim.Y - karakter[0].Konum.Y;

                      
                        gecilenYol.Add(new Lokasyon(karakter[0].Konum.X + deltaX, karakter[0].Konum.Y + deltaY));

                     
                        if (gecilenYol.Contains(adim))
                        {
                     
                            hedefKonum = RastgeleYonBelirle(karakterKonum);
                            aStar = new AStar(engelMatris, karakterKonum, hedefKonum);
                            yol = aStar.YoluBul();
                            if (yol == null || yol.Count <= 1)
                            {
                             
                                break;
                            }
                           
                            adim = yol[1];
                            deltaX = adim.X - karakter[0].Konum.X;
                            deltaY = adim.Y - karakter[0].Konum.Y;
                        }

                     
                        HareketEt(deltaX, deltaY);

                       

                    }
                }
            }
        }




        public class Dugum
        {
            public Lokasyon Konum { get; set; }        
            public double G { get; set; }              
            public double H { get; set; }             
            public double F => G + H;                  
            public Dugum Onceki { get; set; }          

            public Dugum(Lokasyon konum, double g, double h, Dugum onceki)
            {
                Konum = konum;
                G = g;
                H = h;
                Onceki = onceki;
            }
        }

        public class AStar
        {
            private int[,] harita;                      
            private int genislik;                       
            private int yukseklik;                      
            private Lokasyon baslangic;                  
            private Lokasyon hedef;                     
            public AStar(int[,] harita, Lokasyon baslangic, Lokasyon hedef)
            {

                this.harita = harita;
                this.genislik = harita.GetLength(0);
                this.yukseklik = harita.GetLength(1);
                this.baslangic = baslangic;
                this.hedef = hedef;
            }

           //AStar Algoritma
            public List<Lokasyon> YoluBul()
            {
            
                List<Dugum> acikListe = new List<Dugum>();
                HashSet<Lokasyon> kapaliListe = new HashSet<Lokasyon>();
                //Başlandgıç
                Dugum baslangicDugumu = new Dugum(baslangic, 0, HesaplaManhattanMesafe(baslangic, hedef), null);
                acikListe.Add(baslangicDugumu);

                while (acikListe.Any())
                {
                   
                    Dugum simdikiDugum = acikListe.OrderBy(d => d.F).First();                   
                    if (simdikiDugum.Konum.X == hedef.X && simdikiDugum.Konum.Y == hedef.Y)
                    {
                        return YoluOlustur(simdikiDugum);
                    }

                    
                    acikListe.Remove(simdikiDugum);
                    kapaliListe.Add(simdikiDugum.Konum);

                    // DÜğüm kontorlü ...
                    foreach (var komsum in KomusulariBul(simdikiDugum.Konum))
                    {
                        if (kapaliListe.Contains(komsum)) continue;
                       
                        double g = simdikiDugum.G + 1;
                       
                        Dugum eskiDugum = acikListe.FirstOrDefault(d => d.Konum.X == komsum.X && d.Konum.Y == komsum.Y);

                        if (eskiDugum == null || g < eskiDugum.G)
                        {

                           
                            double h = HesaplaManhattanMesafe(komsum, hedef);
                            if (eskiDugum == null)
                            {
                                eskiDugum = new Dugum(komsum, g, h, simdikiDugum);
                                acikListe.Add(eskiDugum);
                            }
                            else
                            {

                                eskiDugum.G = g;
                                eskiDugum.H = h;
                                eskiDugum.Onceki = simdikiDugum;
                            }
                        }
                    }
                }

               
                return null;
            }



           
            public double HesaplaManhattanMesafe(Lokasyon lokasyon1, Lokasyon lokasyon2)
            {

                return Math.Abs(lokasyon1.X - lokasyon2.X) + Math.Abs(lokasyon1.Y - lokasyon2.Y);
            }

            private List<Lokasyon> KomusulariBul(Lokasyon konum)
            {
                List<Lokasyon> komusular = new List<Lokasyon>();
               
                if (konum.X > 0) komusular.Add(new Lokasyon(konum.X - 1, konum.Y));
                if (konum.X < genislik - 1) komusular.Add(new Lokasyon(konum.X + 1, konum.Y)); 
                if (konum.Y > 0) komusular.Add(new Lokasyon(konum.X, konum.Y - 1)); 
                if (konum.Y < yukseklik - 1) komusular.Add(new Lokasyon(konum.X, konum.Y + 1)); 

                return komusular;
            }

           
            private List<Lokasyon> YoluOlustur(Dugum dugum)
            {
                List<Lokasyon> yol = new List<Lokasyon>();
                Dugum simdiki = dugum;
                while (simdiki != null)
                {
                    yol.Add(simdiki.Konum);
                    simdiki = simdiki.Onceki;
                }
                yol.Reverse();
                return yol;
            }

        }
        private void HareketTimer_Tick(object sender, EventArgs e)
        {
            
            StartAutoMovement();
        }
    }
}