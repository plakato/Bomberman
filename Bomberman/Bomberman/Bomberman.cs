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
        int branax;
        int branay;
        int darcekx;
        int darceky;
        char darcek;
        public static bool BranaJeOtvorena = false;
        bool vzdialilSaOdBomby = true;
        Bitmap[] ikonky;
        public Bman Feri;
        public List<Postavicka> Postavicky = new List<Postavicka>();

        public Mapa(String cestaMapa, String cestaIkonky, Graphics g)
        {
            NacitajIkonky(cestaIkonky);
            NacitajMapu(cestaMapa);
            Feri = new Bman(52, 52, 35, new Bitmap("bman.png"));
            Postavicky.Add(Feri);
        }

        public void NacitajMapu(String cesta)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(cesta);
            int pocetKamenov = int.Parse(sr.ReadLine());
            sirka = int.Parse(sr.ReadLine());
            vyska = int.Parse(sr.ReadLine());           
            mapa = new char[vyska, sirka];
            int kamen = 0;
            char c;
            Random rnd = new Random();
            int brana = rnd.Next(0, 32);
            int d = rnd.Next(0, 32);
            while (d==brana)            //aby brana a darcek neboli na jednom policku
            {
                d = rnd.Next(0, 32);
            }
            
            for (int i = 0; i < vyska; i++)
            {
                String riadok = sr.ReadLine();
                for (int j = 0; j < sirka; j++)
                {
                    c = riadok[j];
                    if (c=='K')                     //nahodny kamen bude ukryvat branu a darcek
                    {
                        if (kamen==brana)
                        {
                            branax = i;
                            branay = j;
                            Console.WriteLine("Brana: {0},{1}", branax, branay);
                        }
                        if (kamen==d)
                        {
                            darcekx = i;
                            darceky = j;
                            switch (Form1.MojFormular.level % 3)
                            {
                                case 1:
                                    darcek = 'F';
                                    break;
                                case 2:
                                    darcek = 'E';
                                    break;
                                case 0:
                                    darcek = 'R';
                                    break;
                                default:
                                    break;
                            }
                            Console.WriteLine("Darcek: {0},{1}", darcekx, darceky);
                        }
                        kamen++;
                    }

                    if (c=='D')                 // vytvorim si duchov
                    {
                        Duch buuu = new Duch(j * sx, i * sx, 46, new Bitmap("duch1.png"));
                        Postavicky.Add(buuu);
                        mapa[i,j] = 'n';
                    }
                    else mapa[i, j] = c;
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
                    int index = "nKPBV<>v^-|kGFER".IndexOf(c);     //nic,kamen,prekazka,bomba,vybuch+jehosmery,k=vybuchnuty kamen,gate,fire,extra bomb,rubber duck
                    g.DrawImage(ikonky[index], sx * x, sx * y);
                }
            }
            foreach (var p in Postavicky)
            {
                g.DrawImage(p.bitmapa, p.px, p.py);
            }
            
        }

        public bool MozePostavickaVkrocit(int x, int y, Postavicka p)                   //kontroluje ci nejaky roh postavicky uz je na skale
        {
            bool vystup = true;
            if (JeSkalaAleboBomba(x, y, p)) vystup = false;
            if (JeSkalaAleboBomba(x + p.radius, y, p)) vystup = false;
            if (JeSkalaAleboBomba(x, y + p.radius, p)) vystup = false;
            if (JeSkalaAleboBomba(x + p.radius, y + p.radius, p)) vystup = false;

            if (p==Feri)               //zistim, ci je bomberman mimo bomby - teda uz na nu nemoze vkrocit
            {
                if (mapa[PoziciaVMape(y), PoziciaVMape(x)] == 'n' &&
                    mapa[PoziciaVMape(y + p.radius), PoziciaVMape(x + p.radius)] == 'n')
                {
                    vzdialilSaOdBomby = true;
                }
            }
            AkJeDarcekVezmi();
                
            return vystup;
        }
        public int PoziciaVMape(int x)
        {
            return (int)(x / sx);
        }
        bool JeSkalaAleboBomba(int x, int y, Postavicka p)
        {
            int j = PoziciaVMape(x);
            int i = PoziciaVMape(y);
            char[] vybuch = { 'V', 'v', '>', '<', '^', '|', '-' };
            if (mapa[i, j] == 'P' || mapa[i, j] == 'K' || mapa[i,j] == 'k')
                return true;
            else if (mapa[i, j] == 'B')
            {
                if (vzdialilSaOdBomby || p != Feri) return true;
                else return false;
            }
            else if (mapa[i, j] == 'G' && Feri.JevBrane(sx))
                return true;
            else if (vybuch.Contains(mapa[i, j])) 
            {
                p.Umrel();
            }
            return false;
        }

        public void TuDajBombu(int x, int y, Mapa m)
        {
            int j = PoziciaVMape(x);
            int i = PoziciaVMape(y);
            if (mapa[i, j] != 'B' && mapa[i,j] !='G')
            {
                mapa[i, j] = 'B';
                Vybuch v = new Vybuch(i, j, m);
                vzdialilSaOdBomby = false;
                //Console.WriteLine("zmenil som na false");
            }
        }
        public bool TuJeBranaAleboDarcek(int x, int y)
        {
            if (x == branax && y == branay)
            {
                mapa[x, y] = 'G';
                return true;
            }
            if (x == darcekx && y == darceky)
            {
                mapa[x, y] = darcek;
                return true;
            }
            return false;
        }
        void AkJeDarcekVezmi()
        {
            int j = (Feri.px + (Feri.radius / 2)) / sx;
            int i = (Feri.py + (Feri.radius / 2)) / sx;
            bool BolDarcek = false;
            switch (mapa[i,j])
            {
                case 'F':
                    Vybuch.dosah++;
                    BolDarcek = true;
                    break;
                case 'E':
                    Vybuch.maxNaraz++;
                    BolDarcek = true;
                    break;
                case 'R':
                    mapa[branax, branay] = 'G';
                    BolDarcek = true;
                    break;
                default:
                    break;
            }
            if (BolDarcek)
            {
                mapa[i, j] = 'n';
                darcekx = -1;
                darceky = -1;
            }
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
        public Bitmap bitmapa;
        public int xzmena;
        public int yzmena;
        public static List<Postavicka> RemovePostavicky = new List<Postavicka>();

        public Postavicka(int x, int y, int r, Bitmap bmp)
        {
            px = x;
            py = y;
            radius = r;
            xzmena = 0;
            yzmena = 0;
            bitmapa = bmp;
        }
        public virtual void Umrel()
        {
            this.bitmapa = new Bitmap("smrtka.png");
            RemovePostavicky.Add(this);
        }
        public virtual void ZmenSmer()
        {

        }
    }

    public class Bman : Postavicka
    {
        public Bman(int x, int y, int r, Bitmap bmp) : base(x,y,r,bmp)
        {

        }
        public bool JevBrane(int sx)
        {
            int x = (px + (radius / 2)) / sx;
            int y = (py + (radius / 2)) / sx;
            char c = Mapa.Get(y,x);
            if (c == 'G' && Mapa.BranaJeOtvorena)
            {
                Form1.MojFormular.PrejdiDoStavu(Form1.Stav.vyhra);
                return true;
            }
            return false;
        }
        public override void Umrel()
        {
            base.Umrel();
            Form1.MojFormular.PrejdiDoStavu(Form1.Stav.prehra);
        }
    }
    public class Duch : Postavicka
    {
        static Random rnd = new Random(4);
        int[] smery = { 0, 0, -5, +5 };
        int i;

        public Duch(int x, int y, int r, Bitmap bmp) : base(x,y,r,bmp)
        {
            i = rnd.Next(4);
            xzmena = smery[i];
            yzmena = smery[(i + 2) % 4];
        }
        public override void ZmenSmer()
        {
            i = (i + 1) % 4;
            xzmena = smery[i];
            yzmena = smery[(i + 2) % 4];
        }
        public override void Umrel()
        {
            base.Umrel();
            this.smery[3] = 0;
            this.smery[2] = 0;
            this.xzmena = 0;
            this.yzmena = 0;
        }

        public void ZistiAVyhodnotDotykSBmanom(Bman f)
        {
            if (this.px <= f.px + f.radius &&           //podmienka ktora plati <=> 2 obdlzniky sa prelinaju 
                this.py <= f.py + f.radius &&           //(X2' >= X1 && X1' <= X2) && (Y2' >= Y1 && Y1' <= Y2)
                this.px + this.radius >= f.px &&
                this.py + this.radius >= f.py)
                f.Umrel();
        }

    }

    public class Vybuch
    {
        int doba;
        public static int dosah = 1;
        int x;
        int y;
        public static int maxNaraz = 1;
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
            foreach (var v in removeList)
            {
                vybuchujuciList.Remove(v);
            }
            removeList.Clear();
            //Console.WriteLine("Cakajuci list size "+cakajuciList.Count+", vybuchujuci list size: "+vybuchujuciList.Count);
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
            char[] zakazVojst = { 'P', 'k', 'G' ,'K','B'};
            char c;
            c = Mapa.Get(x , y);
            bool v = true;
                if (zakazVojst.Contains(c)) v = false;
                 if (c == 'K')
                {
                    Mapa.Set(x, y, 'k');
                }
                 if (c == 'B')
                {
                    NajdiVybuch(x, y).doba = 11;        //10 je doba, pri ktorej bomba vybuchuje
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
                else if (!m.TuJeBranaAleboDarcek(x + i, y)) Mapa.Set(x + i, y, 'n');
            }
            for (int i = 1; i <= dosah; i++)
            {
                c = Mapa.Get(x - i, y);
                if (c == 'P' || c == 'K') break;
                else if (!m.TuJeBranaAleboDarcek(x - i, y)) Mapa.Set(x - i, y, 'n');
            }
            for (int i = 1; i <= dosah; i++)
            {
                c = Mapa.Get(x , y + i);
                if (c == 'P' || c == 'K') break;
                else if (!m.TuJeBranaAleboDarcek(x, y + i)) Mapa.Set(x, y + i, 'n');
            }
            for (int i = 1; i <= dosah; i++)
            {
                c = Mapa.Get(x, y - i);
                if (c == 'P' || c == 'K') break;
                else if (!m.TuJeBranaAleboDarcek(x, y - i)) Mapa.Set(x, y - i, 'n');
            }

            foreach (var p in Postavicka.RemovePostavicky)      //odstrani vybuchnute postavicky
            {
                m.Postavicky.Remove(p);
            }

            if (m.Postavicky.Count==1)              //ak uz zije len Bman, otvor mu branu
            {
                Mapa.BranaJeOtvorena = true;
            }
        }

        public static bool MozesBombovat()
        {
            if (cakajuciList.Count < maxNaraz)
            {
                return true;
            }
            else return false;

        }
    }
}
