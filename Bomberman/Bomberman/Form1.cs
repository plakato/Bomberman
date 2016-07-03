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

        enum Sipky { ziadna, vpravo, vlavo, hore, dole };

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
            switch (KtoraJeStlacena())
            {
                case Sipky.ziadna:
                    break;
                case Sipky.hore:
                    Mapa.Feri.py = Mapa.Feri.py - 10;
                    m.Prekresli(g);
                    break;
                case Sipky.dole:
                    Mapa.Feri.py = Mapa.Feri.py + 10;
                    m.Prekresli(g);
                    break;
                case Sipky.vpravo:
                    Mapa.Feri.px = Mapa.Feri.px + 10;
                    m.Prekresli(g);
                    break;
                case Sipky.vlavo:
                    Mapa.Feri.px = Mapa.Feri.px - 10;
                    m.Prekresli(g);
                    break;
                default:
                    break;
            }
            this.Invalidate();
           
        }
        

    }

   
}
