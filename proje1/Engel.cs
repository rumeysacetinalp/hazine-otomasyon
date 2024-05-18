using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1
{
    public class Engel
    {
        public Lokasyon Konum { get; set; }

        public Engel(Lokasyon konum)
        {
            Konum = konum;
        }

    }

    public class SabitEngel : Engel
    {

        

        public YazAğaç YazAğaç { get; set; }
        public YazTaş YazTaş { get; set; }
        public KışAğaç KışAğaç { get; set; }
        public KışTaş KışTaş { get; set; }
        public KışDuvar KışDuvar { get; set; }
        public YazDuvar YazDuvar { get; set; }
        public YazDag Yazdag { get; set; }
        public KışDag KışDag { get; set; }

        public int BoyutX { get; set; }
        public int BoyutY { get; set; }

        private static Random random = new Random();


        public SabitEngel(Lokasyon konum) : base(konum)
        {
            YazAğaç = new YazAğaç();
            YazTaş = null;
            KışAğaç = null;
            KışTaş = null;
            KışDuvar = null;
            KışDag = null;
            Yazdag = null;
            YazDuvar = null;
            BoyutX = YazAğaç.AğaçBoyut;
            BoyutY = YazAğaç.AğaçBoyut;

        }
        public SabitEngel(Lokasyon konum, int boyutX, int boyutY ) : base(konum)
        {
            BoyutX = boyutX;
            BoyutY = boyutY;
        }
        public SabitEngel(Lokasyon konum , double ytaş) : base(konum)
        {

            YazTaş = new YazTaş();
            YazAğaç = null;
            KışAğaç = null;
            KışTaş = null;
            KışDuvar = null;
            KışDag = null;
            Yazdag = null;
            YazDuvar = null;
            BoyutX = YazTaş.TaşBoyut;
            BoyutY = YazTaş.TaşBoyut;

        }

        public SabitEngel(Lokasyon konum, int kağaç) : base(konum)
        {

            KışAğaç = new KışAğaç();
            YazTaş = null;
            YazAğaç = null;
            KışTaş = null;
            KışDuvar = null;
            KışDag = null;
            Yazdag = null;
            YazDuvar = null;

            BoyutX = KışAğaç.AğaçBoyut;
            BoyutY = KışAğaç.AğaçBoyut;
        }

        public SabitEngel(Lokasyon konum, float ktaş , int kıştaş  , char ktas) : base(konum)
        {

            KışTaş = new KışTaş();
            YazTaş = null;
            YazAğaç = null;
            KışAğaç = null;
            KışDuvar = null;
            KışDag = null;
            Yazdag = null;
            YazDuvar = null;

            BoyutX = KışTaş.TaşBoyut;
            BoyutY = KışTaş.TaşBoyut;

        }

        public SabitEngel(Lokasyon konum, char duvar) : base(konum)
        {

            KışDuvar = new KışDuvar();
            YazAğaç = null;
            YazTaş = null;
            KışAğaç = null;
            KışTaş = null;
            Yazdag = null;
            KışDag = null;
            YazDuvar = null;
            BoyutX = KışDuvar.DuvarBoyutX;
            BoyutY = KışDuvar.DuvarBoyutY;
        }

        public SabitEngel(Lokasyon konum, string yazduvar) : base(konum)
        {

            YazDuvar = new YazDuvar();
            YazAğaç = null;
            YazTaş = null;
            KışAğaç = null;
            KışTaş = null;
            Yazdag = null;
            KışDag = null;
            KışDuvar = null;
            BoyutX = YazDuvar.DuvarBoyutX;
            BoyutY = YazDuvar.DuvarBoyutY;
        }

        public SabitEngel(Lokasyon konum, byte yazdag , char yazdag1 , char ydag) : base(konum)
        {

            Yazdag = new YazDag();
            YazAğaç = null;
            YazTaş = null;
            KışAğaç = null;
            KışTaş = null;
            KışDuvar = null;
            KışDag = null;
            YazDuvar = null;
            BoyutX = Yazdag.dagBoyut;
            BoyutY = Yazdag.dagBoyut;
        }

        public SabitEngel(Lokasyon konum, char kışdag , int kışdag1) : base(konum)
        {

            KışDag = new KışDag();
            YazAğaç = null;
            YazTaş = null;
            KışAğaç = null;
            KışTaş = null;
            KışDuvar = null;
            Yazdag = null;
            YazDuvar = null;
            BoyutX = KışDag.dagBoyut;
            BoyutY = KışDag.dagBoyut;
        }



    }

    public class HareketliEngel : Engel
    {
        //    Hareketli engellere özgü özellikler eklenebilir60
        public Kus Kus { get; set; }
        public int HareketYonu { get; set; }
        public int HareketAlani { get; set; }
        public int BaslangicY { get; set; }

        public int BitisY { get; set; }
        public Ari Ari { get; set; }
        public int HareketYonuAri { get; set; }
        public int BaslangicX { get; set; }
        public int BitisX { get; set; }

        public int BoyutX { get; set; }
        public int BoyutY { get; set; }


        private static Random random = new Random();

        public HareketliEngel(Lokasyon konum, int boyutX, int boyutY) : base(konum)
        {
            BoyutX = boyutX;
            BoyutY = boyutY;
        }
        public HareketliEngel(Lokasyon konum) : base(konum)
        {
            Kus = new Kus();
            Ari = null;
            BaslangicY = konum.Y - 5;
            BitisY = konum.Y + 5;
            BoyutX = Kus.KusBoyut;
            BoyutY = Kus.KusBoyut;

        }
        public HareketliEngel(Lokasyon konum, char arı) : base(konum)
        {
            Ari = new Ari();
            Kus = null;
            BaslangicX = konum.X - 3;
            BitisX = konum.X + 3;
            BoyutX = Ari.AriBoyut;
            BoyutY = Ari.AriBoyut;
        }
    }

public class Kus
{
    public string KusResimYolu { get; set; }
    public int KusBoyut { get; set; }


    private static Random random = new Random();
    public Kus()
    {
        KusResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\kus.png";
        KusBoyut = 2;
    }
}
public class Ari
{
    public string AriResimYolu { get; set; }
    public int AriBoyut { get; set; }

    private static Random random = new Random();
    public Ari()
    {
        AriResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\ari.gif";
        AriBoyut = 2;
    }
}




public class YazAğaç
    {
        public string AğaçResimYolu { get; set; }
        public int AğaçBoyut { get; set; }

        private static Random random = new Random();
        public YazAğaç()
        {
            AğaçResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\ağaç.png";
            AğaçBoyut = AğaçRastgeleBoyutSec();
        }

        private int AğaçRastgeleBoyutSec()
        {
            int[] boyutlar = { 2, 3, 4, 5 };
            return boyutlar[random.Next(boyutlar.Length)];
        }
    }

    public class YazTaş
    {
        public string taşResimYolu { get; set; }
        public int TaşBoyut { get; set; }
        private static Random random = new Random();
        public YazTaş()
        {
            taşResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\yaztaş.png";
            TaşBoyut = TaşRastgeleBoyutSec();
        }

        private int TaşRastgeleBoyutSec()
        {
            int[] boyutlar = { 2, 3 };
            return boyutlar[random.Next(boyutlar.Length)];
        }
    }

    public class KışAğaç
    {
        public string AğaçResimYolu { get; set; }
        public int AğaçBoyut { get; set; }

        private static Random random = new Random();
        public KışAğaç()
        {
            AğaçResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\kışağaç.png";
            AğaçBoyut = AğaçRastgeleBoyutSec();
        }

        private int AğaçRastgeleBoyutSec()
        {
            int[] boyutlar = { 2, 3, 4, 5 };
            return boyutlar[random.Next(boyutlar.Length)];
        }
    }
    public class KışTaş
    {
        public string taşResimYolu { get; set; }
        public int TaşBoyut { get; set; }
        private static Random random = new Random();
        public KışTaş()
        {
            taşResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\kıştaş.png";
            TaşBoyut = TaşRastgeleBoyutSec();
        }

        private int TaşRastgeleBoyutSec()
        {
            int[] boyutlar = { 2, 3 };
            return boyutlar[random.Next(boyutlar.Length)];
        }
    }


    public class KışDuvar
    {
        public string DuvarResimYolu { get; set; }
        public int DuvarBoyutX { get; set; }
        public int DuvarBoyutY { get; set; }

        private static Random random = new Random();
        public KışDuvar()
        {
            DuvarResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\kışduvar.png";
            DuvarBoyutX = 10;
            DuvarBoyutY = 1;
        }
        
    }


    public class YazDuvar
    {
        public string DuvarResimYolu { get; set; }
        public int DuvarBoyutX { get; set; }
        public int DuvarBoyutY { get; set; }

        private static Random random = new Random();
        public YazDuvar()
        {
            DuvarResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\yazduvar.png";
            DuvarBoyutX = 10;
            DuvarBoyutY = 1;
        }

    }

    public class YazDag
    {
        public string dagResimYolu { get; set; }
        public int dagBoyut { get; set; }
        private static Random random = new Random();
        public YazDag()
        {
            dagResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\yazdag.png";
            dagBoyut = 15;
        }

        
    }

    public class KışDag
    {
        public string dagResimYolu { get; set; }
        public int dagBoyut { get; set; }
        private static Random random = new Random();
        public KışDag()
        {
            dagResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\kışdag.png";
            dagBoyut = 15;
        }


    }

}



