using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SchetsEditor
{
    class Object
    {
        public Graphics gr;
        public Point Plek;
        public Point Eind;
        public Brush Kwast;
        public char C;
       
    }
    class TweepuntObject : Object
    {
        public Rectangle RechthoekO()
        {
            return new Rectangle(new Point(Math.Min(Plek.X,Eind.X), Math.Min(Plek.Y,Eind.Y))
                                , new Size (Math.Abs(Plek.X-Eind.X), Math.Abs(Plek.Y-Eind.Y)));
        }
        public Pen MaakPen()
        {
            Pen pen = new Pen(Kwast, 3);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            return pen;
        }
        
        public virtual void maak(Graphics gr) { }
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
    }
    class VolEllipsObject : TweepuntObject
    {
        
        public override void maak(Graphics gr)
        {
            gr.FillEllipse(Kwast, RechthoekO());
        }
    }
    class EllipsObject : VolEllipsObject
    {
        
        public override void maak(Graphics gr)
        {
            gr.DrawEllipse(MaakPen(), RechthoekO());
        }
    }
    class LijnObject : TweepuntObject
    {
       
        public override void maak(Graphics gr)
        {
            gr.DrawLine(MaakPen(), Plek, Eind);
        }
    }
}
