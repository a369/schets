using System;
using System.Collections.Generic;
using System.Drawing;


namespace SchetsEditor
{
    class Schets
    {
        private Bitmap bitmap;
        //
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
            else return new EllipsObject();

        }
        public void Voegtoe(int i, Point p1, Point p2, Brush b, char c)
        {
            Object huidigding;
            huidigding = this.ding(i);
            huidigding.Plek = p1;
            huidigding.Eind = p2;
            huidigding.Kwast = b;
            huidigding.C = c;
            lijst.Add(huidigding);
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
            get { return bitmap;}
        }
        //
        public void VeranderAfmeting(Size sz)
        {
            if (sz.Width > bitmap.Size.Width || sz.Height > bitmap.Size.Height)
            {
                Bitmap nieuw = new Bitmap( Math.Max(sz.Width, bitmap.Size.Width)
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
            foreach(Object ding in lijst)
            {
                ding.maak(gr);
            }
            //
        }
        public void Schoon()
        {
            Graphics gr = Graphics.FromImage(bitmap);
            gr.FillRectangle(Brushes.White, 0, 0, bitmap.Width, bitmap.Height);
        }
        public void Roteer()
        {
            bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
        }
    }
}
