using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchetsEditor
{
    abstract class Object
    {
        public Point Plek;
        public Point Eind;
        public SolidBrush Kwast;
        public char C;
        public int Soort;
        public abstract void maak(Graphics gr);
        public abstract bool Isgeklikt(Point p);
        public abstract void Save(Schets s);
    }

    class LetterObject : Object
    {
        public override void maak(Graphics gr)
        {
            Font font = new Font("Helvetica", 40);
            string tekst = C.ToString();
            gr.DrawString(tekst, font, Kwast,
                                          this.Plek, StringFormat.GenericTypographic);
        }

        public override void Save(Schets s)
        {
            this.maak(s.MaakBitmapGraphics());
        }

        public override bool Isgeklikt(Point p)
        {
            int x1 = Math.Min(Eind.X, Plek.X);
            int x2 = Math.Max(Eind.X, Plek.X);
            int y1 = Math.Min(Eind.Y, Plek.Y);
            int y2 = Math.Max(Eind.Y, Plek.Y);

            if (p.X >= x1 && p.X <= x2 && p.Y >= y1 && p.Y <= y2)
                return true;
            else return false;
        }
    }

    class TweepuntObject : Object
    {
        public Rectangle RechthoekO()
        {
            return new Rectangle(new Point(Math.Min(Plek.X, Eind.X), Math.Min(Plek.Y, Eind.Y))
                                , new Size(Math.Abs(Plek.X - Eind.X), Math.Abs(Plek.Y - Eind.Y)));
        }

        public bool Lijn(Point p)
        {
            Rectangle r = RechthoekO();
            double d;
            double dy = Eind.Y - Plek.Y;
            double dx = Eind.X - Plek.X;

            d = (Math.Abs(dy * p.X - dx * p.Y - Plek.X * Eind.Y + Eind.X * Plek.Y)) / (Math.Sqrt(dx * dx + dy * dy));
            if (d <= 3)
                return true;
            return false;

        }

        public override void Save(Schets s) { }

        public Pen MaakPen()
        {
            Pen pen = new Pen(Kwast, 3);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            return pen;
        }

        public override void maak(Graphics gr) { }

        public override bool Isgeklikt(Point p)
        {
            Rectangle r = RechthoekO();

            if (p.X >= r.X - 2 && p.X <= r.Right + 2 && p.Y >= r.Y - 2 && p.Y <= r.Bottom + 2)
                return true;
            else return false;
        }
    }

    class VolRechthoekObject : TweepuntObject
    {
        public override void maak(Graphics gr)
        {
            gr.FillRectangle(Kwast, RechthoekO());
        }

        public override void Save(Schets s)
        {
            this.maak(s.MaakBitmapGraphics());
        }
    }

    class RechthoekObject : VolRechthoekObject
    {

        public override void maak(Graphics gr)
        {
            gr.DrawRectangle(MaakPen(), RechthoekO());
        }

        public override bool Isgeklikt(Point p)
        {
            Rectangle r = RechthoekO();
            if (base.Isgeklikt(p))
            {
                if (p.X >= r.X + 2 && p.X <= r.Right - 2 && p.Y >= r.Y + 2 && p.Y <= r.Bottom - 2)
                    return false;
                return true;
            }
            else return false;
        }

        public override void Save(Schets s)
        {
            this.maak(s.MaakBitmapGraphics());
        }
    }

    class VolEllipsObject : TweepuntObject
    {
        public override void maak(Graphics gr)
        {
            gr.FillEllipse(Kwast, RechthoekO());
        }

        public override bool Isgeklikt(Point p)
        {
            Rectangle r = RechthoekO();
            double d;
            double x = p.X - (r.Width / 2) - r.X;
            double y = p.Y - (r.Height / 2) - r.Y;
            if (base.Isgeklikt(p))
            {
                d = Math.Pow(x, 2) / (Math.Pow((r.Width + 4) / 2, 2))
                    + Math.Pow(y, 2) / (Math.Pow((r.Height + 4) / 2, 2));
                if (d <= 1)
                    return true;
            }
            return false;
        }

        public override void Save(Schets s)
        {
            this.maak(s.MaakBitmapGraphics());
        }
    }

    class EllipsObject : VolEllipsObject
    {
        public override void maak(Graphics gr)
        {
            gr.DrawEllipse(MaakPen(), RechthoekO());
        }

        public override bool Isgeklikt(Point p)
        {
            Rectangle r = RechthoekO();
            double d;
            double x = p.X - (r.Width / 2) - r.X;
            double y = p.Y - (r.Height / 2) - r.Y;
            if (base.Isgeklikt(p))
            {
                d = Math.Pow(x, 2) / (Math.Pow((r.Width - 4) / 2, 2))
                    + Math.Pow(y, 2) / (Math.Pow((r.Height - 4) / 2, 2));
                if (d <= 1)
                    return false;
                return true;
            }
            return false;
        }

        public override void Save(Schets s)
        {
            this.maak(s.MaakBitmapGraphics());
        }
    }

    class LijnObject : TweepuntObject
    {
        public override void maak(Graphics gr)
        {
            gr.DrawLine(MaakPen(), Plek, Eind);
        }

        public override bool Isgeklikt(Point p)
        {
            if (base.Isgeklikt(p))
            {
                if (Lijn(p))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Save(Schets s)
        {
            this.maak(s.MaakBitmapGraphics());
        }
    }
}
