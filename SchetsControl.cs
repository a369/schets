using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SchetsEditor
{
    public class SchetsControl : UserControl
    {
        private Schets schets;
        private Color penkleur = Color.Black;
        private int i;
        private Point p1;
        private Point p2 = new Point(0, 0);
        private SolidBrush b;
        private char c = '-';

        public int soort
        {
            set { i = value; }
        }
        public Point start
        {
            set { p1 = value; }
        }
        public Point eind
        {
            set { p2 = value; }
        }
        public SolidBrush kwast
        {
            set { b = value; }
        }
        public char letter
        {
            set { c = value; }
        }
        public void maak()
        {
            schets.Voegtoe(i, p1, p2, b, c);
            Invalidate();
        }
        public bool opgeslagen
        {
            get { return schets.opgeslagen; }
        }
        public Color PenKleur
        {
            get { return penkleur; }
        }
        public SchetsControl()
        {
            this.BorderStyle = BorderStyle.Fixed3D;
            this.schets = new Schets();
            this.Paint += this.teken;
            this.Resize += this.veranderAfmeting;
            this.veranderAfmeting(null, null);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }
        private void teken(object o, PaintEventArgs pea)
        {
            schets.Teken(pea.Graphics);
        }
        //

        public Bitmap Bitmap
        {
            get { return schets.Bitmap; }
        }
        //
        private void veranderAfmeting(object o, EventArgs ea)
        {
            schets.VeranderAfmeting(this.ClientSize);
            this.Invalidate();
        }
        public Graphics MaakBitmapGraphics()
        {
            Graphics g = schets.BitmapGraphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            return g;
        }
        public void Schoon(object o, EventArgs ea)
        {
            schets.Schoon();
            this.Invalidate();
        }
        public void gum(Point p)
        {
            schets.gum(p);
            Invalidate();
        }
        public void Promotie(Point p)
        {
            schets.Promotie(p);
            Invalidate();
        }

        public string Opslaan()
        {
            return schets.Opslaan();
        }
        public void Open(string s)
        {
            schets.Open(s);
            this.Invalidate();
        }
        public void Undo(object o, EventArgs ea)
        {
            schets.Undo();
            Invalidate();
        }
        public void Colors(object o, EventArgs ea)
        {
            ColorDialog kleur = new ColorDialog();

            if (kleur.ShowDialog() == DialogResult.OK)
                penkleur = kleur.Color;
        }
        public void Roteer(object o, EventArgs ea)
        {
            schets.Roteer();
            this.veranderAfmeting(o, ea);
        }
    }
}
