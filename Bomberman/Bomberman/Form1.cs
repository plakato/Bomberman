using System;
using System.Drawing;
using System.Windows.Forms;
using NullFX.Win32;

namespace Bomberman
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ClientSize = new Size(50 * 15, 50 * 13);
            frame = new Bitmap(50 * 15, 50 * 13);
            KeyPreview = true;
        }
        Graphics g;
        Mapa m;
        Bitmap frame;

        enum Sipky { ziadna, vpravo, vlavo, hore, dole };
        enum Stav { nezacala, bezi, koniec};
        Stav stav = Stav.nezacala;

        static Sipky KtoraJeStlacena()
        {
            KeyStateInfo hore = KeyboardInfo.GetKeyState(Keys.Up);
            KeyStateInfo dole = KeyboardInfo.GetKeyState(Keys.Down);
            KeyStateInfo vpravo = KeyboardInfo.GetKeyState(Keys.Right);
            KeyStateInfo vlavo = KeyboardInfo.GetKeyState(Keys.Left);

            if (hore.IsPressed) return Sipky.hore;
            else if (dole.IsPressed) return Sipky.dole;
            else if (vpravo.IsPressed) return Sipky.vpravo;
            else if (vlavo.IsPressed) return Sipky.vlavo;
            else return Sipky.ziadna;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
            stav = Stav.bezi;
            button1.Visible = false;
            g = Graphics.FromImage(frame);
            this.DoubleBuffered = true;
            m = new Mapa("mapa1.txt", "ikonky.png", g);
            timer1.Enabled = true;                       
            m.Prekresli(g);
            this.Focus();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawImage(frame, 0, 0);
        }

        int novex;
        int novey;
        private void timer1_Tick(object sender, EventArgs e)
        {
            KeyStateInfo medzernik = KeyboardInfo.GetKeyState(Keys.Space);
            if (medzernik.IsPressed && Vybuch.MozesBombovat(m.Feri.px + 20, m.Feri.py + 20)) 
                    m.TuDajBombu(m.Feri.px + 20, m.Feri.py + 20, m);
            Vybuch.VyhodnotBomby();
            novex = m.Feri.px;
            novey = m.Feri.py;
            switch (KtoraJeStlacena())
            {
                case Sipky.ziadna:
                    break;
                case Sipky.hore:
                    novey = novey - 10;
                    break;
                case Sipky.dole:
                    novey = novey + 10;
                    break;
                case Sipky.vpravo:
                    novex = novex + 10;
                    break;
                case Sipky.vlavo:
                    novex = novex - 10;
                    break;
                default:
                    break;
            }
            if (m.MozeBmanVkrocit(novex, novey))
            {
                m.Feri.px = novex;
                m.Feri.py = novey;
            }               
            m.Prekresli(g);
            this.Invalidate();           
        }        
    }  
}
