using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NullFX.Win32;

namespace Bomberman
{
    public class Mapa
    {
        static char[,] mapa;
        int sirka;          //hracieho pola
        int vyska;
        int sx;         //sirka stvorceka v pixeloch
        Bitmap[] ikonky;
        Bitmap bmanBitmapa;
        public Bman Feri;

        public Mapa(String cestaMapa, String cestaIkonky, Graphics g)
        {
            NacitajIkonky(cestaIkonky);
            NacitajMapu(cestaMapa);
            Feri = new Bman(52, 52, 30);
            bmanBitmapa = new Bitmap("b-man.bmp");
        }

        public void NacitajMapu(String cesta)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(cesta);
            sirka = int.Parse(sr.ReadLine());
            vyska = int.Parse(sr.ReadLine());
            mapa = new char[vyska, sirka];

            for (int i = 0; i < vyska; i++)
            {
                String riadok = sr.ReadLine();
                for (int j = 0; j < sirka; j++)
                {
                    mapa[i, j] = riadok[j];
                }
            }
            sr.Close();
        }

        public void NacitajIkonky(String cesta)
        {
            Bitmap bmp = new Bitmap(cesta);
            this.sx = bmp.Height;
            int pocet = bmp.Width / sx;
            ikonky = new Bitmap[pocet];

            for (int i = 0; i < pocet; i++)
            {
                Rectangle rect = new Rectangle(sx * i, 0, sx, sx);
                ikonky[i] = bmp.Clone(rect, System.Drawing.Imaging.PixelFormat.DontCare);
            }
        }

        public void Prekresli(Graphics g)
        {
            for (int y = 0; y < vyska; y++)
            {
                for (int x = 0; x < sirka; x++)
                {
                    char c = mapa[y, x];
                    int index = "nKPBV<>v^-|k".IndexOf(c);
                    g.DrawImage(ikonky[index], sx * x, sx * y);
                }
            }
            g.DrawImage(bmanBitmapa, Feri.px, Feri.py);
        }
        public bool MozeBmanVkrocit(int x, int y)                   //kontroluje ci nejaky roh postavicky uz je na skale
        {
            bool vystup = true;
            if (JeSkala(x, y)) vystup = false;
            if (JeSkala(x+ Feri.radius, y)) vystup = false;
            if (JeSkala(x, y+Feri.radius)) vystup = false;
            if (JeSkala(x+Feri.radius, y+Feri.radius)) vystup = false;
            return vystup;
        }

        int PoziciaBx(int x)
        {
            return (int)(x / sx);
        }
        int PoziciaBy(int y)
        {
            return (int)(y / sx);
        }

        bool JeSkala(int x, int y)
        {
            bool vystup = false;
            int j = PoziciaBx(x);
            int i = PoziciaBy(y);
            if (mapa[i, j] == 'P' || mapa[i,j]=='K') vystup = true;
            return vystup;
        }

        public void TuDajBombu(int x, int y, Mapa m)
        {
            int j = PoziciaBx(x);
            int i = PoziciaBy(y);
            mapa[i, j] = 'B';
            Vybuch v = new Vybuch(i, j, m);
        }
        public static char Get(int x, int y)
        {
            return mapa[x, y];
        }
        public static void Set(int x, int y, char c)
        {
            mapa[x, y] = c;
        }

    }

    public class Postavicka
    {
        public int px;
        public int py;
        public int radius;
    }

    public class Bman : Postavicka
    {
        public Bman(int x, int y, int r)
        {
            px = x;
            py = y;
            radius = r;
        }
    }

    public class Vybuch
    {
        int doba;
        public static int dosah = 2;
        int x;
        int y;
        public static int maxNaraz = 2;
        public static List<Vybuch> vList = new List<Vybuch>();
        public static List<Vybuch> removeList = new List<Vybuch>();
        static Mapa m;

        public Vybuch(int xx, int yy, Mapa mm)
        {
            x = xx;
            y = yy;
            doba = 50;
            vList.Add(this);
            m = mm;
        }

        public static void VyhodnotBomby()
        {
            foreach (var v in vList)
            {
                v.doba--;
                if (v.doba == 20) VykresliVybuch(v.x, v.y);
                if (v.doba == 0)
                {
                    UpracVybuch(v.x, v.y);
                    removeList.Add(v);
                }
            }
            foreach (var v in removeList)
                    vList.Remove(v);
            removeList.Clear();
        }

        static void VykresliVybuch(int x, int y)
        {
            char c;
            Mapa.Set(x, y, 'V');
            for (int i = 0; i <= dosah; i++)
            {
                c = Mapa.Get(x + i, y);
                if (c == 'P') break;
                if (c=='K')
                {
                    Mapa.Set(x + i, y, 'k');
                    break;
                }
                if (c=='B')
                {
                    NajdiVybuch(x + i, y).doba = 20;        //20 je doba, pri ktorej bomba vybuchuje
                    break;
                }
                if (c == 'n')
                {
                    if (i == dosah) Mapa.Set(x + i, y, 'v');
                    else Mapa.Set(x + 1, y, '|');
                }

            }
        }
        static Vybuch NajdiVybuch(int x, int y)
        {
            foreach (var v in vList)
            {
                if (v.x == x && v.y == y) return v;
            }
            throw new System.InvalidProgramException("Nastala nezhoda v bombach, prosim vyhladajte odbornika.");
        }
        static void UpracVybuch(int x, int y)
        {

        }

        public static bool MozesBombovat(int x, int y)
        {
            if (vList.Count < maxNaraz)
            {
                return true;
            }
            else return false;

        }
    }
}
