using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace S5GUIEditor
{
    partial class Credits : Form
    {
        Image bg = Properties.Resources.CreditsBack_Normal;
        bool mouseOver = false;
        bool mouseDown = false;
        bool creditsBgDrawn = false;
        Rectangle btnRect;

        public Credits()
        {
            InitializeComponent();
            pbBack.Visible = false;
            btnRect = new Rectangle(pbBack.Location, pbBack.Size);
        }
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (!creditsBgDrawn)
                g.DrawImage(Properties.Resources.Credits, new Rectangle(0, 0, 388, 406));

            if (mouseOver)
            {
                if (mouseDown)
                    bg = Properties.Resources.CreditsBack_Pressed;
                else
                    bg = Properties.Resources.CreditsBack_Hover;
            }
            else
                bg = Properties.Resources.CreditsBack_Normal;
            g.DrawImage(bg, btnRect);
            creditsBgDrawn = true;
        }

        private void Credits_MouseMove(object sender, MouseEventArgs e)
        {
            bool isOver = e.X > pbBack.Location.X && e.X < pbBack.Location.X + 32 && e.Y > pbBack.Location.Y && e.Y < pbBack.Location.Y + 32;
            bool changed = isOver ^ mouseOver;
            mouseOver = isOver;
            if (changed)
                Invalidate();

        }

        private void Credits_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            if (mouseOver)
                Invalidate();
        }

        private void Credits_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
            if (mouseOver)
                this.Close();
        }
    }
}
