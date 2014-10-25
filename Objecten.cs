using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchetsEditor
{
    class Objecten
    {
        protected Graphics gr;
        protected Point p1;
        protected Point p2;
        protected Brush kwast;
        protected char c;
    }
    class TweepuntObject : Objecten
    {
        public Rectangle RechthoekO()
        {
            return new Rectangle(new Point(Math.Min(p1.X,p2.X), Math.Min(p1.Y,p2.Y))
                                , new Size (Math.Abs(p1.X-p2.X), Math.Abs(p1.Y-p2.Y)));
        }
        public Pen MaakPen()
        {
            Pen pen = new Pen(kwast, 3);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            return pen;
        }
        protected void Set(Point P1, Point P2, Brush B, char C)
        {
            p1 = P1;
            p2 = P2;
            kwast = B;
            c = C;
        }
    }
    class VolRechthoekObject : TweepuntObject
    {
        public void VolRecthoek(Point P1, Point P2, Brush B, char C)
        {
            Set(P1, P2, B, C);
        }
        public override void maak(Graphics gr)
        {
            gr.FillRectangle(kwast, RechthoekO());
        }
    }
    class RechthoekObject : VolRechthoekObject
    {
        public void Recthoek(Point P1, Point P2, Brush B, char C)
        {
            Set(P1, P2, B, C);
        }
        public override void maak(Graphics gr)
        {
            gr.DrawRectangle(MaakPen(), RechthoekO());
        }
    }
    class VolEllipsObject : TweepuntObject
    {
        public void VolEllips(Point P1, Point P2, Brush B, char C)
        {
            Set(P1, P2, B, C);
        }
        public override void maak(Graphics gr)
        {
            gr.FillEllipse(kwast, RechthoekO());
        }
    }
    class EllipsObject : VolEllipsObject
    {
        public void Ellips(Point P1, Point P2, Brush B, char C)
        {
            Set(P1, P2, B, C);
        }
        public override void maak(Graphics gr)
        {
            gr.DrawEllipse(MaakPen(), RechthoekO());
        }
    }
    class LijnObject : TweepuntObject
    {
        public void Lijn(Point P1, Point P2, Brush B, char C)
        {
            Set(P1, P2, B, C);
            Maak();
        }
        public override void maak(Graphics gr)
        {
            gr.DrawLine(MaakPen(), p1, p2);
        }
    }
}
