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
        private char[,] mapa;
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
                    int index = "nKP".IndexOf(c);
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

        bool JeSkala(int x, int y)
        {
            bool vystup = false;
            int j = (int)(x / sx);
            int i = (int)(y / sx);
            if (mapa[i, j] == 'P' || mapa[i,j]=='K') vystup = true;
            return vystup;
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
}
