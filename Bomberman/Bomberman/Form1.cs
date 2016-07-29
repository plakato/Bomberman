/**************
BOMBERMAN

Patrícia Lakatošová
Programovanie II
MFF UK 2016
***************/

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
            MojFormular = this;
            InitializeComponent();
            ClientSize = new Size(50 * 15, 50 * 13);
            frame = new Bitmap(50 * 15, 50 * 13);
            KeyPreview = true;
            this.DoubleBuffered = true;
            PrejdiDoStavu(Stav.uvod);
        }
        Graphics g;
        Mapa m;
        Bitmap frame;
        Random rnd = new Random();
        public static Form1 MojFormular;
        TimeSpan ts;
        public int level;
        public int zivoty;

        enum Sipky { ziadna, vpravo, vlavo, hore, dole };
        public enum Stav { uvod, bezi, vyhra, prehra, vyhodnotenie, koniec, info};
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
            if (stav==Stav.koniec)
            {
                level = 1;
                Vybuch.dosah = 1;
                Vybuch.maxNaraz = 1;
            }
            m = new Mapa("mapa" + level.ToString() + ".txt", "ikonky.png", g);
            m.Prekresli(g);
            PrejdiDoStavu(Stav.bezi);

        }
        public void Resetuj()
        {
            Vybuch.cakajuciList.Clear();
            Vybuch.vybuchujuciList.Clear();
            Postavicka.RemovePostavicky.Clear();
            BNovaHra.Visible = false;
            BSkusitZnovu.Visible = false;
            BVzdatTo.Visible = false;
            BDalsiLevel.Visible = false;
            BReplay.Visible = false;
            BInfo.Visible = false;
            Bomberman.Mapa.BranaJeOtvorena = false;
        }
        public void PrejdiDoStavu(Stav novyStav)
        {
            switch (novyStav)
            {
                case Stav.uvod:
                    g = Graphics.FromImage(frame);
                    g.DrawImage(new Bitmap("background.png"), 0, 0);
                    this.Invalidate();
                    BNovaHra.Visible = true;
                    LLevel.Visible = false;
                    LZivoty.Visible = false;
                    zivoty = 12;
                    TCasomiera.Visible = false;
                    level = 1;
                    stav = Stav.uvod;
                    break;
                case Stav.bezi:
                    Resetuj();
                    TCasomiera.Visible = true;        
                    this.Focus();                   //aby medzernik nestlacal defaultne tlacidlo, ale hadzal bomby
                    timer1.Enabled = true;
                    timer2.Enabled = true;
                    LZivoty.Visible = true;
                    LZivoty.Text = (zivoty/4).ToString();
                    LLevel.Visible = true;
                    LLevel.Text = "Level " + level.ToString();
                    ts = new TimeSpan(0, 4, 0);        //tu mozno menit maximalny cas na jeden level
                    stav = Stav.bezi;
                    break;
                case Stav.vyhra:
                    timer1.Enabled = false;
                    timer2.Enabled = false;
                    Bitmap youveWon = new Bitmap("vyhra.png");
                    g.DrawImage(youveWon, 0, 0);
                    this.Invalidate();
                    BDalsiLevel.Visible = true;
                    BReplay.Visible = true;
                    LLevel.Visible = false;
                    LZivoty.Visible = false;
                    TCasomiera.Visible = false;
                    BInfo.Visible = true;
                    stav = Stav.vyhra;
                    break;
                case Stav.prehra:
                    timer1.Enabled = false;
                    TCasomiera.Visible = false;
                    zivoty--;
                    Console.WriteLine("zivoty "+zivoty.ToString());
                    if (zivoty == 0)
                    {
                        PrejdiDoStavu(Stav.uvod);
                        timer2.Enabled = false;
                    }
                    else
                    {
                        timer2.Enabled = true;
                        stav = Stav.prehra;
                    }
                    break;
                case Stav.vyhodnotenie:
                    timer2.Enabled = false;
                    Bitmap youveLost = new Bitmap("youveLost2.png");
                    g.DrawImage(youveLost,0,0);
                    this.Invalidate();
                    BSkusitZnovu.Visible = true;
                    BVzdatTo.Visible = true;
                    LZivoty.Visible = false;
                    LLevel.Visible = false;
                    BNovaHra.Visible = false;
                    BInfo.Visible = true;
                    stav = Stav.vyhodnotenie;
                    break;
                case Stav.koniec:
                    Bitmap tbc = new Bitmap("toBeContinued.png");
                    g.DrawImage(tbc, 0, 0);
                    this.Invalidate();
                    BDalsiLevel.Visible = false;
                    BNovaHra.Visible = true;
                    LZivoty.Visible = false;
                    LLevel.Visible = false;
                    BNovaHra.Location = new System.Drawing.Point(Form1.MojFormular.Width/2 - BNovaHra.Width/2,(Form1.MojFormular.Height /5)*3);
                    break;
                case Stav.info:
                    Bitmap infoPic = new Bitmap("infoPic.png");
                    BNovaHra.Visible = false;
                    BSkusitZnovu.Visible = false;
                    BReplay.Visible = false;
                    BVzdatTo.Visible = false;
                    LLevel.Visible = false;
                    BDalsiLevel.Visible = false;
                    g.DrawImage(infoPic, 0, 0);
                    this.Invalidate();
                    break;
                default:
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
           e.Graphics.DrawImage(frame, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ts = ts.Subtract(TimeSpan.FromMilliseconds(100));
            KeyStateInfo medzernik = KeyboardInfo.GetKeyState(Keys.Space);
            if (medzernik.IsPressed && Vybuch.MozesBombovat()) 
                    m.TuDajBombu(m.Feri.px + m.Feri.radius/2, m.Feri.py + m.Feri.radius / 2, m);       //bomba na stred postavicky
            Vybuch.VyhodnotBomby();
            m.Feri.xzmena = 0;
            m.Feri.yzmena = 0;
            switch (KtoraJeStlacena())
            {
                case Sipky.ziadna:
                    break;
                case Sipky.hore:
                    m.Feri.yzmena = -10;
                    break;
                case Sipky.dole:
                    m.Feri.yzmena = 10;
                    break;
                case Sipky.vpravo:
                    m.Feri.xzmena = 10;
                    break;
                case Sipky.vlavo:
                    m.Feri.xzmena = -10;
                    break;
                default:
                    break;
            }

            PohniPostavickamiAPrekresli();          
        }
        public void PohniPostavickamiAPrekresli()
        {
            int n = 0;
            foreach (var p in m.Postavicky)
            {
                while (n == 0)
                {
                    n = rnd.Next(-1, 1);        //duch meni sme nahodne v smere alebo proti smeru hodinovych ruciciek
                }
                for (int i = 0; i < 4; i++)                                             //opakuje sa 4-krat, aby duchovia mohli zmenit smer v slepej ulicke
                {
                    if (m.MozePostavickaVkrocit(p.px + p.xzmena, p.py + p.yzmena, p))
                    {
                        p.px += p.xzmena;
                        p.py += p.yzmena;
                        break;
                    }
                    else p.ZmenSmer(n);
                }

                if (p is Duch)
                {
                    Duch buuu = (Duch)p;
                    buuu.ZistiAVyhodnotDotykSBmanom(m.Feri);
                }
            }
            m.Prekresli(g);

            if (stav == Stav.prehra)
            {
                Bitmap boom = new Bitmap("boom.png");
                g.DrawImage(boom, frame.Width / 2 - boom.Width / 2, frame.Height / 2 - boom.Height / 2);
            }
            if (stav == Stav.vyhra)
            {
                Bitmap youveWon = new Bitmap("vyhra.png");
                g.DrawImage(youveWon, 0, 0);
            }
            if (stav == Stav.uvod)
            {
                PrejdiDoStavu(Stav.uvod);
            }

            TCasomiera.Text = ts.ToString(@"m\:ss");
            this.Invalidate();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (stav==Stav.prehra)
                PrejdiDoStavu(Stav.vyhodnotenie);
            else
            {
                foreach (var p in m.Postavicky)
                {
                    p.ZmenSmer(1);
                }
                if (ts == TimeSpan.FromSeconds(0))
                    PrejdiDoStavu(Stav.prehra);
            }
        }

        //---------- CLICKY --------------
        private void BVzdatTo_Click(object sender, EventArgs e)
        {
            Form1.MojFormular.Close();
        }
        private void BSkusitZnovu_Click(object sender, EventArgs e)
        {
            switch (level)
            {
                case 1:
                case 3:
                    if (Vybuch.dosah == (2 + level / 3))
                        Vybuch.dosah--;
                    break;
                case 2:
                case 4:
                    if (Vybuch.maxNaraz == (2 + level / 3))
                        Vybuch.maxNaraz--;
                    break;
                default:
                    break;
            }
            BNovaHra_Click(sender,e);
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void BDalsiLevel_Click(object sender, EventArgs e)
        {
           if (level < 6)
            {
                level++;
                BNovaHra_Click(sender, e);
            }
           else
            {
                PrejdiDoStavu(Stav.koniec);
            }
        }
        private void BReplay_Click(object sender, EventArgs e)
        {
            BSkusitZnovu_Click(sender, e);
        }
        private void BInfo_Click(object sender, EventArgs e)
        {
            if (this.BInfo.Tag.ToString() == "info")            //kliknutim na rovnake tlacidlo sa zobrazi alebo skryje infopanel
            {
                PrejdiDoStavu(Stav.info);
                this.BInfo.BackgroundImage = new Bitmap("x.png");
                this.BInfo.Tag = "x";
            }
            else
            {
                PrejdiDoStavu(stav);
                this.BInfo.BackgroundImage = new Bitmap("info.png");
                this.BInfo.Tag = "info";
            }
        }
    }  
}
