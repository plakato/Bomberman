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
        int pocetKamenov;
        int sx;         //sirka stvorceka v pixeloch
        int branax;
        int branay;
        bool BranaJeOtvorena = true;
        Bitmap[] ikonky;
        public static Bitmap bmanBitmapa;
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
            pocetKamenov = int.Parse(sr.ReadLine());
            sirka = int.Parse(sr.ReadLine());
            vyska = int.Parse(sr.ReadLine());           
            mapa = new char[vyska, sirka];
            Random rnd = new Random();
            int brana = rnd.Next(0, 32);
            int kamen = 0;

            for (int i = 0; i < vyska; i++)
            {
                String riadok = sr.ReadLine();
                for (int j = 0; j < sirka; j++)
                {
                    if (riadok[j]=='K')
                    {
                        if (kamen==brana)
                        {
                            branax = i;
                            branay = j;
                        }
                        kamen++;
                    }
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
                    int index = "nKPBV<>v^-|kG".IndexOf(c);
                    g.DrawImage(ikonky[index], sx * x, sx * y);
                }
            }
            g.DrawImage(bmanBitmapa, Feri.px, Feri.py);
        }
        public bool MozeBmanVkrocit(int x, int y)                   //kontroluje ci nejaky roh postavicky uz je na skale
        {
            bool vystup = true;
            if (JeSkalaAleboBomba(x, y)) vystup = false;
            if (JeSkalaAleboBomba(x+ Feri.radius, y)) vystup = false;
            if (JeSkalaAleboBomba(x, y+Feri.radius)) vystup = false;
            if (JeSkalaAleboBomba(x+Feri.radius, y+Feri.radius)) vystup = false;
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

        bool JeSkalaAleboBomba(int x, int y)
        {
            bool vystup = false;
            int j = PoziciaBx(x);
            int i = PoziciaBy(y);
            if (mapa[i, j] == 'P' || mapa[i, j] == 'K') vystup = true;
            else if (mapa[i, j] == 'B') ;
            else if (mapa[i, j] == 'G')
                    if (BranaJeOtvorena)
                    Form1.MojFormular.PrejdiDoStavu(Form1.Stav.vyhra);
            else if (mapa[i, j] != 'n')
            {
                Bman.Umrel();
                Form1.MojFormular.PrejdiDoStavu(Form1.Stav.prehra);
            }
            return vystup;
        }

        public void TuDajBombu(int x, int y, Mapa m)
        {
            int j = PoziciaBx(x);
            int i = PoziciaBy(y);
            if (mapa[i, j] != 'B')
            {
                mapa[i, j] = 'B';
                Vybuch v = new Vybuch(i, j, m);
            }
        }
        public bool TuJeBrana(int x, int y)
        {
            if (x == branax && y == branay && mapa[x, y] == 'k') return true;
            return false;
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
        public static void Umrel()
        {
            Mapa.bmanBitmapa = new Bitmap("smrtka.png");
        }
    }

    public class Vybuch
    {
        int doba;
        public static int dosah = 2;
        int x;
        int y;
        public static int maxNaraz = 3;
        public static List<Vybuch> cakajuciList = new List<Vybuch>();
        public static List<Vybuch> vybuchujuciList = new List<Vybuch>();
        public static List<Vybuch> removeList = new List<Vybuch>();
        static Mapa m;

        public Vybuch(int xx, int yy, Mapa mm)
        {
            x = xx;
            y = yy;
            doba = 40;
            cakajuciList.Add(this);
            m = mm;
        }

        public static void VyhodnotBomby()
        {
            foreach (var v in cakajuciList)
            {
                v.doba--;
                if (v.doba == 10)
                {
                    VykresliVybuch(v.x, v.y);
                    vybuchujuciList.Add(v);
                }
            }
            foreach (var v in vybuchujuciList)
            {
                if (v.doba == 10) cakajuciList.Remove(v);
                v.doba--;
                if (v.doba == 0)
                {
                    removeList.Add(v);
                    UpracVybuch(v.x, v.y);
                }
            }
            removeList.Clear();
        }

        static void VykresliVybuch(int x, int y)
        {
            Mapa.Set(x, y, 'V');
            for (int i = 1; i <= dosah; i++)
            {
                if (!MozeVybuchPokracovat(x+i,y,i, 'v', '|')) break;
            }
            for (int i = 1; i <= dosah; i++)
            {
                if (!MozeVybuchPokracovat(x - i, y, i, '^', '|')) break;
            }
            for (int i = 1; i <= dosah; i++)
            {
                if (!MozeVybuchPokracovat(x, y+i, i, '>', '-')) break;
            }
            for (int i = 1; i <= dosah; i++)
            {
                if (!MozeVybuchPokracovat(x, y-i, i, '<', '-')) break;
            }
        }
        static bool MozeVybuchPokracovat(int x, int y,int i, char koniec, char most)
        {
            char c;
            c = Mapa.Get(x , y);
            bool v = true;
                if (c == 'P' || c=='k') v = false;
                 if (c == 'K')
                {
                    Mapa.Set(x, y, 'k');
                    v=false;
                }
                 if (c == 'B')
                {
                    NajdiVybuch(x, y).doba = 11;        //10 je doba, pri ktorej bomba vybuchuje
                    v = false; ;
                }
                if (c=='n')
                {
                if (i == dosah)
                    Mapa.Set(x, y, koniec);               
                else Mapa.Set(x, y, most);
                }
                return v;
        }
        static Vybuch NajdiVybuch(int x, int y)
        {
            foreach (var v in cakajuciList)
            {
                if (v.x == x && v.y == y) return v;
            }
            throw new System.InvalidProgramException("Nastala nezhoda v bombach, prosim vyhladajte odbornika.");
        }
        static void UpracVybuch(int x, int y)
        {
            char c;
            Mapa.Set(x, y, 'n');
            for (int i = 1; i <= dosah; i++)
            {
                c = Mapa.Get(x + i, y);
                if (c == 'P' || c == 'K') break;
                else if (m.TuJeBrana(x + i, y)) Mapa.Set(x + i, y, 'G');
                else Mapa.Set(x + i, y, 'n');
            }
            for (int i = 1; i <= dosah; i++)
            {
                c = Mapa.Get(x - i, y);
                if (c == 'P' || c == 'K') break;
                else if (m.TuJeBrana(x - i, y)) Mapa.Set(x - i, y, 'G');
                else Mapa.Set(x - i, y, 'n');
            }
            for (int i = 1; i <= dosah; i++)
            {
                c = Mapa.Get(x , y + i);
                if (c == 'P' || c == 'K') break;
                else if (m.TuJeBrana(x, y + i)) Mapa.Set(x, y + i, 'G');
                else Mapa.Set(x , y+i, 'n');
            }
            for (int i = 1; i <= dosah; i++)
            {
                c = Mapa.Get(x, y - i);
                if (c == 'P' || c == 'K') break;
                else if (m.TuJeBrana(x, y - i)) Mapa.Set(x, y - i, 'G');
                else Mapa.Set(x , y-i, 'n');
            }
        }

        public static bool MozesBombovat(int x, int y)
        {
            if (cakajuciList.Count < maxNaraz)
            {
                return true;
            }
            else return false;

        }
    }
}
