﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchetsEditor
{
    abstract class Object
    {
        public Point Plek;
        public Point Eind;
        public Brush Kwast;
        public char C;
        public abstract void maak(Graphics gr);
        public abstract bool Isgeklikt(Point p);

    }
    class LetterObject : Object
    {
        public override void maak(Graphics gr)
        {
            Font font = new Font("Tahoma", 40);
            string tekst = C.ToString();
            gr.DrawString(tekst, font, Kwast,
                                          this.Plek, StringFormat.GenericTypographic);
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
            double dy = r.Height;
            double dx = r.Width;

            d = Math.Abs(dy * p.X - dx * p.Y - Plek.X * Eind.Y + Eind.X * Plek.Y) / Math.Sqrt(dx * dx + dy * dy);
            if (d >= 2 && d <= 2)
                return true;
            return false;

        }
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
            
            if (p.X >= r.X - 1 && p.X <= r.Right + 1 && p.Y >= r.Y - 1 && p.Y <= r.Bottom + 1)
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
                if (p.X >= r.X + 1 && p.X <= r.Right - 1 && p.Y >= r.Y + 1 && p.Y <= r.Bottom - 1)
                    return false;
                return true;
            }
            else return false;
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
            if (base.Isgeklikt(p))
            {
                d = ((p.X) * (p.X)) / (((r.Width + 2) / 2) * ((r.Width + 2) / 2))
                    + ((p.Y) * (p.Y)) / (((r.Height + 2) / 2) * ((r.Height + 2) / 2));
                if (d <= 1)
                    return true;
            }
            return false;
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
            if (base.Isgeklikt(p))
            {
                d = ((p.X) * (p.X)) / (((r.Width - 2) / 2) * ((r.Width - 2) / 2))
                    + ((p.Y) * (p.Y)) / (((r.Height - 2) / 2) * ((r.Height - 2) / 2));
                if (d <= 1)
                    return false;
                return true;
            }
            return false;
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
            if (Lijn(p))
            {
                return true;
            }
            else return false;


        }

    }
}
