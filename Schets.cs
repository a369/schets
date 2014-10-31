using System;
using System.Collections.Generic;
using System.Drawing;


namespace SchetsEditor
{
    class Schets
    {
        private Bitmap bitmap;
        //
        int tel = -1;
        public List<Object> lijst = new List<Object>();
        /*Object[] ding = {
                            new LijnObject(),
                            new VolRechthoekObject(),
                            new RechthoekObject(),
                            new VolEllipsObject(),
                            new EllipsObject()
                        };*/
        private Object ding(int i)
        {
            if (i == 0) return new LijnObject();
            if (i == 1) return new VolRechthoekObject();
            if (i == 2) return new RechthoekObject();
            if (i == 3) return new VolEllipsObject();
            if (i == 4) return new EllipsObject();
            else return new LetterObject();

        }
        public void Voegtoe(int i, Point p1, Point p2, SolidBrush b, char c)
        {
            Object huidigding;
            huidigding = this.ding(i);
            huidigding.Plek = p1;
            huidigding.Eind = p2;
            huidigding.Kwast = b;
            huidigding.Soort = i;
            huidigding.C = c;
            lijst.Add(huidigding);
            tel++;
        }
        public string Opslaan()
        {
            string s = "";
            foreach (Object ding in lijst)
            {
                int i = ding.Soort;
                Point p1 = ding.Plek;
                Point p2 = ding.Eind;
                SolidBrush b = ding.Kwast;
                char c = ding.C;
                int p1x = p1.X;
                int p2x = p2.X;
                int p1y = p1.Y;
                int p2y = p2.Y;

                Color kl = b.Color;
                int ro = kl.R;
                int gr = kl.G;
                int bl = kl.B;


                s += "" + i + ' ' + p1x + ' ' + p1y + ' ' + p2x + ' ' + p2y + ' ' + ro + ' ' + gr + ' ' + bl + ' ' + c + ' ';
            }


            return s;
        }

        public void Open(string s)
        {
            int i = 0;
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 0);
            SolidBrush b;
            char c;
            Color kl;
            int ro = 0;
            int gr = 0;
            int bl = 0;
            string[] v = s.Split(' ');
            for (int t = 0; t < v.Length - 1; t++)
            {
                if (t % 9 == 0)
                {
                    i = int.Parse(v[t]);
                }
                if (t % 9 == 1)
                {
                    p1.X = int.Parse(v[t]);
                }
                if (t % 9 == 2)
                {
                    p1.Y = int.Parse(v[t]);
                }
                if (t % 9 == 3)
                {
                    p2.X = int.Parse(v[t]);
                }
                if (t % 9 == 4)
                {
                    p2.Y = int.Parse(v[t]);
                }
                if (t % 9 == 5)
                {
                    ro = int.Parse(v[t]);
                }
                if (t % 9 == 6)
                {
                    gr = int.Parse(v[t]);
                }
                if (t % 9 == 7)
                {
                    bl = int.Parse(v[t]);
                }
                if (t % 9 == 8)
                {
                    c = v[t][0];
                    kl = Color.FromArgb(ro, gr, bl);
                    b = new SolidBrush(kl);
                    Voegtoe(i, p1, p2, b, c);
                }
            }

        }
        //
        public Schets()
        {
            bitmap = new Bitmap(1, 1);
        }
        public Graphics BitmapGraphics
        {
            get { return Graphics.FromImage(bitmap); }
        }
        //
        public Bitmap Bitmap
        {
            get { return bitmap; }
        }
        //
        public void VeranderAfmeting(Size sz)
        {
            if (sz.Width > bitmap.Size.Width || sz.Height > bitmap.Size.Height)
            {
                Bitmap nieuw = new Bitmap(Math.Max(sz.Width, bitmap.Size.Width)
                                         , Math.Max(sz.Height, bitmap.Size.Height)
                                         );
                Graphics gr = Graphics.FromImage(nieuw);
                gr.FillRectangle(Brushes.White, 0, 0, sz.Width, sz.Height);
                gr.DrawImage(bitmap, 0, 0);
                bitmap = nieuw;
            }
        }
        public void Teken(Graphics gr)
        {
            gr.DrawImage(bitmap, 0, 0);
            //
            foreach (Object ding in lijst)
            {
                ding.maak(gr);
            }
            //
        }
        public void Schoon()
        {

            while (tel > -1)
            {
                lijst.RemoveAt(tel);
                tel -= 1;
            }
        }
        public void Undo()
        {
            if (tel > -1)
            {
                lijst.RemoveAt(tel);
                tel -= 1;
            }
        }
        public void gum(Point p)
        {
            for (int i = tel; i >= 0; i -= 1)
            {
                Object ding = lijst[i];
                if (ding.Isgeklikt(p))
                {
                    lijst.RemoveAt(i);
                    tel -= 1;
                    break;
                }

            }
        }
        /* public void Schoon()
         {
             Graphics gr = Graphics.FromImage(bitmap);
             gr.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
         }*/
        public void Roteer()
        {
            foreach (Object ding in lijst)
            {
                ding.Plek = PuntRotatie(ding.Plek);
                ding.Eind = PuntRotatie(ding.Eind);
            }

        }
        public Point PuntRotatie(Point p)
        {
            Point m = Midden(p);
            int y;

            y = m.X;
            m.X = -m.Y;
            m.Y = y;

            return Origineel(m);
        }
        public Point Midden(Point p)
        {
            Point m = new Point(0, 0);

            m.X = p.X - (bitmap.Width / 2);
            m.Y = p.Y - (bitmap.Height / 2);

            return m;
        }
        public Point Origineel(Point p)
        {
            Point m = new Point(0, 0);

            m.X = p.X + (bitmap.Width / 2);
            m.Y = p.Y + (bitmap.Height / 2);

            return m;
        }
    }
}