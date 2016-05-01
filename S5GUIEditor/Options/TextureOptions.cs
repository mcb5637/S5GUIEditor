using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace S5GUIEditor.Options
{
    public partial class TextureOptions : UserControl
    {
        Texture curTexture;
        public TextureOptions()
        {
            InitializeComponent();
        }

        public void Initialize(Texture texture, string textureName)
        {
            curTexture = texture;
            lTextureName.Text = textureName;
        }
        private void btnChoose_Click(object sender, EventArgs e)
        {
            TexturePicker tp = new TexturePicker(curTexture);
            tp.ShowDialog();
        }
    }
}
