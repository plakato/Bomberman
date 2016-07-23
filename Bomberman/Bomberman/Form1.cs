﻿using System;
using System.Drawing;
using System.Windows.Forms;
using NullFX.Win32;

namespace Bomberman
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            MojFormular = this;
            InitializeComponent();
            ClientSize = new Size(50 * 15, 50 * 13);
            frame = new Bitmap(50 * 15, 50 * 13);
            KeyPreview = true;
        }
        Graphics g;
        Mapa m;
        Bitmap frame;
        public static Form1 MojFormular;
        TimeSpan ts;

        enum Sipky { ziadna, vpravo, vlavo, hore, dole };
        public enum Stav { uvod, bezi, vyhra, prehra, vyhodnotenie};
        Stav stav = Stav.uvod;  

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

        private void BNovaHra_Click(object sender, EventArgs e)
        {
            PrejdiDoStavu(Stav.bezi);
            g = Graphics.FromImage(frame);
            this.DoubleBuffered = true;
            m = new Mapa("mapa1.txt", "ikonky.png", g);                     
            m.Prekresli(g);
        }

        public void PrejdiDoStavu(Stav novyStav)
        {
            switch (novyStav)
            {
                case Stav.uvod:
                    TCasomiera.Visible = false;
                    stav = Stav.uvod;
                    break;
                case Stav.bezi:
                    BNovaHra.Visible = false;
                    BSkusitZnovu.Visible = false;
                    BVzdatTo.Visible = false;
                    TCasomiera.Visible = true;
                    Vybuch.cakajuciList.Clear();               
                    this.Focus();
                    timer1.Enabled = true;
                    timer2.Enabled = true;
                    ts = new TimeSpan(0, 4, 0);
                    stav = Stav.bezi;
                    break;
                case Stav.vyhra:
                    timer1.Enabled = false;
                    TCasomiera.Visible = false;
                    Bitmap youveWon = new Bitmap("vyhra.png");
                    g.DrawImage(youveWon, 0, 0);
                    this.Invalidate();
                    BNovaHra.Visible = true;
                    stav = Stav.vyhra;
                    break;
                case Stav.prehra:
                    timer1.Enabled = false;
                    timer2.Enabled = true;
                    TCasomiera.Visible = false;
                    stav = Stav.prehra;
                    break;
                case Stav.vyhodnotenie:
                    timer2.Enabled = false;
                    Bitmap youveLost = new Bitmap("youveLost2.png");
                    g.DrawImage(youveLost,0,0);
                    this.Invalidate();
                    BSkusitZnovu.Visible = true;
                    BVzdatTo.Visible = true;
                    stav = Stav.vyhodnotenie;
                    break;
                default:
                    break;
            }
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
            if (medzernik.IsPressed && Vybuch.MozesBombovat()) 
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
                m.Prekresli(g);
            }
           
            if (stav==Stav.prehra)
            {
                Bitmap boom = new Bitmap("boom.png");
                g.DrawImage(boom, frame.Width/2 - boom.Width/2,frame.Height/2 - boom.Height/2);
            }
            TCasomiera.Text = ts.Minutes + ":" + ts.Seconds;
            this.Invalidate();           
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (stav==Stav.prehra) PrejdiDoStavu(Stav.vyhodnotenie);
            else
            {
                ts.Subtract(TimeSpan.FromMinutes(1));
            }
        }

        private void BVzdatTo_Click(object sender, EventArgs e)
        {
            Form1.MojFormular.Close();
        }

        private void BSkusitZnovu_Click(object sender, EventArgs e)
        {
            BNovaHra_Click(sender,e);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }  
}
