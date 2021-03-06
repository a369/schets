﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchetsEditor
{
    public interface ISchetsTool
    {
        void MuisVast(SchetsControl s, Point p);
        void MuisDrag(SchetsControl s, Point p);
        void MuisLos(SchetsControl s, Point p);
        void Letter(SchetsControl s, char c);
        void Soort(SchetsControl s);
    }

    public abstract class StartpuntTool : ISchetsTool
    {
        protected Point startpunt;
        protected SolidBrush kwast;
        protected int i;

        public virtual void MuisVast(SchetsControl s, Point p)
        {
            startpunt = p;
            s.start = p;
        }

        public virtual void MuisLos(SchetsControl s, Point p)
        {
            kwast = new SolidBrush(s.PenKleur);
            s.kwast = kwast;
        }

        public abstract void MuisDrag(SchetsControl s, Point p);

        public abstract void Letter(SchetsControl s, char c);

        public abstract void Soort(SchetsControl s);
    }

    public class TekstTool : StartpuntTool
    {
        public override string ToString() { return "tekst"; }

        public override void MuisDrag(SchetsControl s, Point p) { }

        public override void Letter(SchetsControl s, char c)
        {
            if (c >= 32 && c != ' ')
            {
                Point eind = new Point(0, 0);
                s.soort = i;
                s.letter = c;
                s.start = startpunt;
                Graphics gr = s.MaakBitmapGraphics;
                Font font = new Font("Helvetica", 40);
                string tekst = c.ToString();
                SizeF sz =
                gr.MeasureString(tekst, font, this.startpunt, StringFormat.GenericTypographic);
                eind.X = startpunt.X + (int)sz.Width;
                eind.Y = startpunt.Y + (int)sz.Height;
                s.eind = eind;
                s.maak();
                startpunt.X += (int)sz.Width;
            }
        }

        public override void Soort(SchetsControl s)
        {
            i = 5;
        }
    }

    public abstract class TweepuntTool : StartpuntTool
    {
        protected Point eindpunt;
        bool b = false;

        public static Rectangle Punten2Rechthoek(Point p1, Point p2)
        {
            return new Rectangle(new Point(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y))
                                , new Size(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y))
                                );
        }

        public static Pen Pen()
        {
            Pen pen = new Pen(Color.Gray, 3);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            return pen;
        }

        public override void MuisVast(SchetsControl s, Point p)
        {
            base.MuisVast(s, p);
            b = true;
            kwast = new SolidBrush(Color.Gray);
        }

        public override void MuisDrag(SchetsControl s, Point p)
        {
            s.Refresh();
            this.Bezig(s.CreateGraphics(), this.startpunt, p);
        }

        public override void MuisLos(SchetsControl s, Point p)
        {
            base.MuisLos(s, p);
            s.eind = p;
            s.soort = i;
            if(b)
            s.maak();
        }

        public override void Letter(SchetsControl s, char c) { }

        public abstract void Bezig(Graphics g, Point p1, Point p2);

        public abstract override void Soort(SchetsControl s);
    }

    public class RechthoekTool : TweepuntTool
    {
        public override string ToString() { return "kader"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            g.DrawRectangle(Pen(), TweepuntTool.Punten2Rechthoek(p1, p2));
        }

        public override void Soort(SchetsControl s)
        {
            i = 2;
        }
    }

    public class EllipsTool : TweepuntTool
    {
        public override string ToString() { return "ellips"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            g.DrawEllipse(Pen(), TweepuntTool.Punten2Rechthoek(p1, p2));
        }

        public override void Soort(SchetsControl s)
        {
            i = 4;
        }
    }

    public class VolEllipsTool : EllipsTool
    {
        public override string ToString() { return "disk"; }

        public override void Soort(SchetsControl s)
        {
            i = 3;
        }
    }

    public class VolRechthoekTool : RechthoekTool
    {
        public override string ToString() { return "vlak"; }

        public override void Soort(SchetsControl s)
        {
            i = 1;
        }
    }

    public class LijnTool : TweepuntTool
    {
        public override string ToString() { return "lijn"; }

        public override void Bezig(Graphics g, Point p1, Point p2)
        {
            g.DrawLine(Pen(), p1.X, p1.Y, p2.X, p2.Y);
        }

        public override void Soort(SchetsControl s)
        {
            i = 0;
        }
    }

    public class PenTool : LijnTool
    {
        public override string ToString() { return "pen"; }

        public override void MuisDrag(SchetsControl s, Point p)
        {
            this.MuisLos(s, p);
            this.MuisVast(s, p);
        }

        public override void Soort(SchetsControl s)
        {
            i = 0;
        }
    }

    public class GumTool : StartpuntTool
    {
        public override string ToString() { return "gum"; }

        public override void MuisDrag(SchetsControl s, Point p) { this.MuisVast(s, p); }

        public override void Letter(SchetsControl s, char c) { }

        public override void Soort(SchetsControl s) { }

        public override void MuisVast(SchetsControl s, Point p)
        {
            s.gum(p);
        }
    }
    public class PromotieTool : GumTool
    {
        public override string ToString() { return "promo"; }

        public override void MuisDrag(SchetsControl s, Point p) { }

        public override void MuisVast(SchetsControl s, Point p)
        {
            s.Promotie(p);
        }
    }
}
