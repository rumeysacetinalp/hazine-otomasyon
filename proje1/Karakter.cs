using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1
{
    public class Karakter
    {
        public int ID { get; set; }
       
        public Lokasyon Konum { get; set; }
        public string karakterResimYolu { get; set; }
        public int karakterBoyut { get; set; }
        public Karakter(Lokasyon konum)
        {
            Konum = konum;
        }

        private static Random random = new Random();

        public Karakter(int id )
        {
            ID = id;
 
        }

        public Karakter()
        {
            karakterResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\karakter3.png";
            karakterBoyut = 1;
        }

    }

    public class KarakterÖzellik : Karakter
    {
        public Karakter Karakter { get; set; }
        public int BoyutX { get; set; }
        public int BoyutY { get; set; }

        private static Random random = new Random();


        public KarakterÖzellik(Lokasyon konum, int boyutX, int boyutY) : base(konum)
        {
            BoyutX = boyutX;
            BoyutY = boyutY;
        }

        public KarakterÖzellik(Lokasyon konum, int karakter) : base(konum)
        {
            Karakter = new Karakter();
            BoyutX = 1;
            BoyutY = 1;
        }


    }



}