using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(50 * 15, 50 * 13);
            frame = new Bitmap(50 * 15, 50 * 13);
        }
        Graphics g;
        Mapa m;
        Bitmap frame;

        enum Sipka { neni, hore, dole, vpravo, vlavo };

        Sipka sipka = Sipka.neni;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                sipka = Sipka.hore;
                return true;
            }
            if (keyData == Keys.Down)
            {
                sipka = Sipka.dole;
                return true;
            }
            if (keyData == Keys.Left)
            {
                sipka = Sipka.vlavo;
                return true;
            }
            if (keyData == Keys.Right)
            {
                sipka = Sipka.vpravo;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            sipka = Sipka.neni;
        }
        private void Form1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            g = Graphics.FromImage(frame);
            this.DoubleBuffered = true;
            m = new Mapa("mapa1.txt", "ikonky.png", g);
            timer1.Enabled = true;                       
            m.Prekresli(g);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(frame, 0, 0);
        } 

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (sipka)
            {
                case Sipka.neni:
                    break;
                case Sipka.hore:
                    Mapa.Feri.py = Mapa.Feri.py - 10;
                    m.Prekresli(g);
                    break;
                case Sipka.dole:
                    Mapa.Feri.py = Mapa.Feri.py + 10;
                    m.Prekresli(g);
                    break;
                case Sipka.vpravo:
                    Mapa.Feri.px = Mapa.Feri.px + 10;
                    m.Prekresli(g);
                    break;
                case Sipka.vlavo:
                    Mapa.Feri.px = Mapa.Feri.px - 10;
                    m.Prekresli(g);
                    break;
                default:
                    break;
            }
            this.Invalidate();
            sipka = Sipka.neni;
           
        }
        

    }

    public class Mapa
    {
        private char[,] mapa;
        int sirka;          //hracieho pola
        int vyska;
        int sx;         //sirka stvorceka v pixeloch
        Bitmap[] ikonky;
        Bitmap bmanBitmapa;
        public static Bman Feri;

        public Mapa(String cestaMapa,String cestaIkonky, Graphics g)
        {
            NacitajIkonky(cestaIkonky);
            NacitajMapu(cestaMapa);
            Feri = new Bman(52, 52, 40);
            bmanBitmapa = new Bitmap("b-man.bmp"); 
        }

        public void NacitajMapu(String cesta)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(cesta);
            sirka = int.Parse(sr.ReadLine());
            vyska = int.Parse(sr.ReadLine());
            mapa = new char[vyska,sirka];

            for (int i = 0; i < vyska; i++)
            {
                String riadok = sr.ReadLine();
                for (int j = 0; j < sirka; j++)
                {                   
                    mapa[i,j] = riadok[j];
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
                    char c = mapa[y,x];
                    int index = "nKP".IndexOf(c);
                    g.DrawImage(ikonky[index],sx*x,sx*y);
                }
            }
            g.DrawImage(bmanBitmapa, Feri.px, Feri.py);
        }
    }

    public class Postavicka
    {
        public int px;
        public int py;
        public int radius;
    }

    public class Bman:Postavicka
    {
        public Bman(int x,int y,int r)
        {
            px = x;
            py = y;
            radius = r;
        }
    }
}
