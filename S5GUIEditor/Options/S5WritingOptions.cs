using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AvengersUtd.ColorChooserTest;

namespace S5GUIEditor.Options
{
    public partial class S5WritingOptions : UserControl
    {
        S5Writing curS5Writing = new S5Writing("", new S5String("",""),0,Color.White);
        public S5WritingOptions()
        {
            InitializeComponent();
        }

        public void Initialize(S5Writing writing, string name)
        {
            gbName.Text = name;
            curS5Writing = writing;
            tbFontPath.Text = curS5Writing.FontPath;
            tbStringFrameDistance.Text = curS5Writing.StringFrameDistance.ToString();
            s5soText.Initialize(curS5Writing.Text);

        }
        private void btnChooseFont_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = GlobalSettings.DataPath + "menu\\fonts\\";
            ofd.Filter = ".met|*.met|All|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string filename = ofd.FileName;
                if (filename.Length < GlobalSettings.DataPath.Length || filename.Substring(0, GlobalSettings.DataPath.Length) != GlobalSettings.DataPath)
                {
                    MessageBox.Show("Invalid path");
                    return;
                }
                filename = filename.Substring(GlobalSettings.DataPath.Length);
                curS5Writing.FontPath = filename;
                UpdateGraphics();
            }
        }

        private void  UpdateGraphics()
        {
            pbColor.Invalidate();
            pbPreview.Invalidate();
        }

        private void btnSetWhite_Click(object sender, EventArgs e)
        {
            curS5Writing.RGBA = Color.White;
            UpdateGraphics();
        }

        private void btnSetBlack_Click(object sender, EventArgs e)
        {
            curS5Writing.RGBA = Color.Black;
            UpdateGraphics();
        }

        private void tbStringFrameDistance_TextChanged(object sender, EventArgs e)
        {
            float sfd = 0;
            float.TryParse(tbStringFrameDistance.Text, out sfd);
            curS5Writing.StringFrameDistance = sfd;
            UpdateGraphics();
        }

        private void pbPreview_Paint(object sender, PaintEventArgs e)
        {
            curS5Writing.DrawString(e.Graphics, new RectangleF(0, 0, pbPreview.Width, pbPreview.Height), 1);
        }

        private void pbColor_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(curS5Writing.RGBA), 0, 0, pbColor.Width, pbColor.Height);
        }

        private void pbColor_Click(object sender, EventArgs e)
        {
            ColorChooser cd = new ColorChooser() { Color = curS5Writing.RGBA };
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                curS5Writing.RGBA = cd.Color;
                UpdateGraphics();
            }
        }

        private void s5soText_ContentChanged(object sender, EventArgs e)
        {
            UpdateGraphics();
        }

        private void btnTransperent_Click(object sender, EventArgs e)
        {
            curS5Writing.RGBA = Color.Transparent;
            UpdateGraphics();
        }
    }
}
