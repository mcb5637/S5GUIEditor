using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AvengersUtd.ColorChooserTest;

namespace S5GUIEditor
{
    public partial class TexturePicker : Form
    {
        Texture loadedTexture;
        Texture pickedTexture;
        Size pickedTextureSize = new Size(0, 0);
        Size loadedTextureSize;
        TextureBrush bg;
        public TexturePicker(Texture texture)
        {
            InitializeComponent();
            bg = new TextureBrush(panel1.BackgroundImage, WrapMode.Tile);
            pickedTexture = texture;
            SetCurrentTexture(texture.TexturePath);
        }

        private void SetCurrentTexture(string texturePath)
        {
            loadedTexture = new Texture(texturePath, new RectangleF(0, 0, 1, 1), Color.White);
            if (loadedTexture.TextureImage != null)
            {
                loadedTextureSize = loadedTexture.TextureImage.Size;
                float w = pickedTexture.TextureImage.Width * pickedTexture.TexturePosAndSize.Width;
                float h = pickedTexture.TextureImage.Height * pickedTexture.TexturePosAndSize.Height;
                pickedTextureSize = new Size((int)w, (int)h);
                float x = pickedTexture.TexturePosAndSize.X * pickedTexture.TextureImage.Width;
                float y = pickedTexture.TexturePosAndSize.Y * pickedTexture.TextureImage.Height;
                float xGrid = x / w;
                float yGrid = y / h;
                tbWGrid.Text = w.ToString();
                tbHGrid.Text = h.ToString();
                tbXGrid.Text = Math.Round(xGrid, 3).ToString();
                tbYGrid.Text = Math.Round(yGrid, 3).ToString();
                tbXCustom.Text = x.ToString();
                tbYCustom.Text = y.ToString();
                gpSize.Enabled = true;
                tcCoords.Enabled = true;
                tbTexturePath.Text = texturePath;
            }
            else
            {
                loadedTextureSize = new Size(pbTexture.Width, pbTexture.Height);
                pickedTextureSize = new Size(pbPickedTexture.Width, pbPickedTexture.Height);
                loadedTexture.RGBA = Color.Transparent;
                gpSize.Enabled = false;
                tcCoords.Enabled = false;
                tbTexturePath.Text = "";
            }
        }

        private void btnSearchFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = GlobalSettings.DataPath + "graphics\\textures\\gui\\";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filename = ofd.FileName;
                if (filename.Length < GlobalSettings.DataPath.Length || filename.Substring(0, GlobalSettings.DataPath.Length) != GlobalSettings.DataPath)
                {
                    MessageBox.Show("Invalid path");
                    return;
                }
                filename = filename.Substring(GlobalSettings.DataPath.Length);
                pickedTexture.TexturePath = filename;
                SetCurrentTexture(pickedTexture.TexturePath);
                UpdateGraphics();
            }
        }

        private void pbTexture_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(bg, new Rectangle(panel1.HorizontalScroll.Value, panel1.VerticalScroll.Value, panel1.Width, panel1.Height));
            if (loadedTexture != null)
                loadedTexture.DrawTexture(g, new RectangleF(0, 0, loadedTextureSize.Width, loadedTextureSize.Height));
        }

        private void pbPickedTexture_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(bg, new Rectangle(panel2.HorizontalScroll.Value, panel2.VerticalScroll.Value, panel2.Width, panel2.Height));
            if (pickedTexture != null)
                pickedTexture.DrawTexture(g, new RectangleF(0, 0, pickedTextureSize.Width, pickedTextureSize.Height));
        }

        private void UpdateGraphics()
        {
            if (pickedTexture.TextureImage != null)
            {
                pbTexture.Width = panel1.Width > loadedTexture.TextureImage.Width + 1 ? panel1.Width : loadedTexture.TextureImage.Width + 1;
                pbTexture.Height = panel1.Height > loadedTexture.TextureImage.Height + 1 ? panel1.Height : loadedTexture.TextureImage.Height + 1;
                pbPickedTexture.Width = panel2.Width > pickedTextureSize.Width + 1 ? panel2.Width : pickedTextureSize.Width + 1;
                pbPickedTexture.Height = panel2.Height > pickedTextureSize.Height + 1 ? panel2.Height : pickedTextureSize.Height + 1;
            }
            else
            {
                pbTexture.Width = panel1.Width;
                pbTexture.Height = panel1.Height;
                pbPickedTexture.Width = panel2.Width;
                pbPickedTexture.Height = panel2.Height;
            }
            pbTexture.Invalidate();
            pbPickedTexture.Invalidate();
            pColor.Invalidate();
        }

        private void tbW_TextChanged(object sender, EventArgs e)
        {
            int w = 0;
            int.TryParse((sender as TextBox).Text, out w);
            pickedTextureSize.Width = w;
            pickedTexture.TexturePosAndSize = new RectangleF(pickedTexture.TexturePosAndSize.X, pickedTexture.TexturePosAndSize.Y, w / (float)pickedTexture.TextureImage.Width, pickedTexture.TexturePosAndSize.Height);
            if (tcCoords.SelectedIndex == 0)
                SetGrid();
            UpdateGraphics();
        }

        private void tbH_TextChanged(object sender, EventArgs e)
        {
            int h = 0;
            int.TryParse((sender as TextBox).Text, out h);
            pickedTextureSize.Height = h;
            pickedTexture.TexturePosAndSize = new RectangleF(pickedTexture.TexturePosAndSize.X, pickedTexture.TexturePosAndSize.Y, pickedTexture.TexturePosAndSize.Width, h / (float)pickedTexture.TextureImage.Height);
            if (tcCoords.SelectedIndex == 0)
                SetGrid();
            UpdateGraphics();
        }

        private void tbXGrid_TextChanged(object sender, EventArgs e)
        {
            SetGrid();
            UpdateGraphics();
        }

        private void tbYGrid_TextChanged(object sender, EventArgs e)
        {
            SetGrid();
            UpdateGraphics();
        }

        private void SetGrid()
        {
            int y = 0;
            int.TryParse(tbYGrid.Text, out y);
            y *= pickedTextureSize.Height;
            pickedTexture.TexturePosAndSize = new RectangleF(pickedTexture.TexturePosAndSize.X, y / (float)pickedTexture.TextureImage.Height, pickedTexture.TexturePosAndSize.Width, pickedTexture.TexturePosAndSize.Height);
            int x = 0;
            int.TryParse(tbXGrid.Text, out x);
            x *= pickedTextureSize.Width;
            pickedTexture.TexturePosAndSize = new RectangleF(x / (float)pickedTexture.TextureImage.Width, pickedTexture.TexturePosAndSize.Y, pickedTexture.TexturePosAndSize.Width, pickedTexture.TexturePosAndSize.Height);
        }

        private void tbXCustom_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            int.TryParse(tbXCustom.Text, out x);
            pickedTexture.TexturePosAndSize = new RectangleF(x / (float)pickedTexture.TextureImage.Width, pickedTexture.TexturePosAndSize.Y, pickedTexture.TexturePosAndSize.Width, pickedTexture.TexturePosAndSize.Height);
            UpdateGraphics();
        }

        private void tbYCustom_TextChanged(object sender, EventArgs e)
        {
            int y = 0;
            int.TryParse(tbYCustom.Text, out y);
            pickedTexture.TexturePosAndSize = new RectangleF(pickedTexture.TexturePosAndSize.X, y / (float)pickedTexture.TextureImage.Height, pickedTexture.TexturePosAndSize.Width, pickedTexture.TexturePosAndSize.Height);
            UpdateGraphics();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            pickedTexture.TexturePosAndSize = loadedTexture.TexturePosAndSize;
            pickedTextureSize = new Size((int)loadedTexture.TextureImage.Width, (int)loadedTexture.TextureImage.Height);
            tbWGrid.Text = pickedTextureSize.Width.ToString();
            tbHGrid.Text = pickedTextureSize.Height.ToString();
            tbXCustom.Text = "0";
            tbYCustom.Text = "0";
            tbXGrid.Text = "0";
            tbYGrid.Text = "0";
            UpdateGraphics();
        }

        private void panel1_Scroll(object sender, ScrollEventArgs e)
        {
            pbTexture.Invalidate();
        }

        private void panel3_Click(object sender, EventArgs e)
        {
            ColorChooser cd = new ColorChooser() { Color = pickedTexture.RGBA };
            if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pickedTexture.RGBA = cd.Color;
                UpdateGraphics();
            }
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            if (pickedTexture != null)
                g.FillRectangle(new SolidBrush(pickedTexture.RGBA), 0, 0, pColor.Width, pColor.Height);
        }

        private void btnNoTexture_Click(object sender, EventArgs e)
        {
            pickedTexture.TexturePath = "";
            SetCurrentTexture(pickedTexture.TexturePath);
            UpdateGraphics();
        }

        private void btnSetWhite_Click(object sender, EventArgs e)
        {
            pickedTexture.RGBA = Color.White;
            UpdateGraphics();
        }

        private void btnSetBlack_Click(object sender, EventArgs e)
        {
            pickedTexture.RGBA = Color.Black;
            UpdateGraphics();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTransperent_Click(object sender, EventArgs e)
        {
            pickedTexture.RGBA = Color.Transparent;
            UpdateGraphics();
        }
    }
}
