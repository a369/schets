using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchetsEditor
{
    public class Hoofdscherm : Form
    {
        bool openb = false;
        MenuStrip menuStrip;
        public Hoofdscherm()
        {
            this.ClientSize = new Size(800, 650);
            menuStrip = new MenuStrip();
            this.Controls.Add(menuStrip);
            this.maakFileMenu();
            this.maakHelpMenu();
            this.Text = "Schets editor";
            this.IsMdiContainer = true;
            this.MainMenuStrip = menuStrip;
        }
        private void maakFileMenu()
        {
            ToolStripDropDownItem menu;
            menu = new ToolStripMenuItem("File");
            menu.DropDownItems.Add("Nieuw", null, this.nieuw);
            menu.DropDownItems.Add("Exit", null, this.afsluiten);
            menu.DropDownItems.Add("Open", null, this.open);
            menuStrip.Items.Add(menu);
        }
        private void maakHelpMenu()
        {
            ToolStripDropDownItem menu;
            menu = new ToolStripMenuItem("Help");
            menu.DropDownItems.Add("Over \"Schets\"", null, this.about);
            menuStrip.Items.Add(menu);
        }
        private void about(object o, EventArgs ea)
        {
            MessageBox.Show("Schets versie 2.0\n(c) UU Informatica 2010 \nTomas & Adriaan"
                           , "Over \"Schets\""
                           , MessageBoxButtons.OK
                           , MessageBoxIcon.Information
                           );
        }

        private void nieuw(object sender, EventArgs e)
        {
            SchetsWin s = new SchetsWin();
            s.MdiParent = this;
            s.Show();
            if (openb)
                s.Open();
            openb = false;
        }
        private void open(object sender, EventArgs e)
        {
            openb = true;
            nieuw(sender, e);

        }
        private void afsluiten(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
