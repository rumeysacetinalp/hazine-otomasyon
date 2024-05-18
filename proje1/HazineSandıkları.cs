using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proje1
{
    public class HazineSandıkları
    {
        public Lokasyon Konum { get; set; }
        public string Tür { get; set; }
        public bool Toplandı { get; set; }
        public HazineSandıkları(Lokasyon konum)
        {
            Konum = konum;
            
        }

        public void Topla()
        {
            
            Toplandı = true;
        }

    }

    public class Hazine_sandıkları : HazineSandıkları
    {
        public AltınSandık AltınSandık { get; set; }
        public GümüşSandık GümüşSandık { get; set; }
        public BronzSandık BronzSandık { get; set; }
        public ZümrütSandık ZümrütSandık { get; set; }
        public int BoyutX { get; set; }
        public int BoyutY { get; set; }

       // private string Tür;

        private static Random random = new Random();
        public Hazine_sandıkları(Lokasyon konum, int boyutX, int boyutY) : base(konum)
        {
            BoyutX = boyutX;
            BoyutY = boyutY;
        }
        public Hazine_sandıkları(Lokasyon konum, int altın, string tür) : base(konum)
        {
            AltınSandık = new AltınSandık();
            GümüşSandık = null;
            BronzSandık = null;
            ZümrütSandık = null;

            BoyutX = AltınSandık.altınBoyut;
            BoyutY = AltınSandık.altınBoyut;
            Tür = tür;
        }
        public Hazine_sandıkları(Lokasyon konum, char gumus, string tür) : base(konum)
        {

            GümüşSandık = new GümüşSandık();
            AltınSandık = null;
            BronzSandık = null;
            ZümrütSandık = null;
            BoyutX = GümüşSandık.gümüşBoyut;
            BoyutY = GümüşSandık.gümüşBoyut;
            Tür = tür;
        }
        public Hazine_sandıkları(Lokasyon konum, string tür) : base(konum)
        {
            BronzSandık = new BronzSandık();
            AltınSandık = null;
            GümüşSandık = null;
            ZümrütSandık = null;
            BoyutX = BronzSandık.bronzBoyut;
            BoyutY =BronzSandık.bronzBoyut;
            Tür = tür;
        }
        public Hazine_sandıkları(Lokasyon konum, int zümrüt, char z, string tür) : base(konum)
        {
            ZümrütSandık = new ZümrütSandık();
            AltınSandık = null;
            GümüşSandık = null;
            BronzSandık = null;
            BoyutX = ZümrütSandık.zümrütBoyut;
            BoyutY = ZümrütSandık.zümrütBoyut;
            Tür = tür;
        }



    }

    public class AltınSandık
    {
        public string altınResimYolu { get; set; }
        public int altınBoyut { get; set; }

        private static Random random = new Random();
        public AltınSandık()
        {
            altınResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\altın.png";
            altınBoyut = 1;
        }


    }

    public class GümüşSandık
    {
        public string gümüşResimYolu { get; set; }
        public int gümüşBoyut { get; set; }
        private static Random random = new Random();
        public GümüşSandık()
        {
            gümüşResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\gumus.png";
            gümüşBoyut = 1;
        }


    }

    public class BronzSandık
    {
        public string bronzResimYolu { get; set; }
        public int bronzBoyut { get; set; }
        private static Random random = new Random();
        public BronzSandık()
        {
            bronzResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\bronz.png";
            bronzBoyut = 1;
        }
    }

    public class ZümrütSandık
    {
        public string zümrütResimYolu { get; set; }
        public int zümrütBoyut { get; set; }
        private static Random random = new Random();
        public ZümrütSandık()
        {
            zümrütResimYolu = "C:\\Users\\LENOVO\\source\\repos\\proje1\\proje1\\resimler\\zümrüt.png";
            zümrütBoyut = 1;
        }


    }
}
