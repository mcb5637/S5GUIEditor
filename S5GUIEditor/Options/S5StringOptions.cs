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
    public partial class S5StringOptions : UserControl
    {
        S5String curString;
        public S5StringOptions()
        {
            InitializeComponent(); 
        }

        public event EventHandler ContentChanged;

        public void Initialize(S5String s5String)
        {
            curString = s5String;
            tbRawString.Text = curString.RawString;
            tbStringTableKey.Text = curString.StringTableKey;
        }

        private void tbStringTableKey_TextChanged(object sender, EventArgs e)
        {
            curString.StringTableKey = tbStringTableKey.Text;
            if (this.ContentChanged != null)
                this.ContentChanged(this, new EventArgs());
        }

        private void tbRawString_TextChanged(object sender, EventArgs e)
        {
            curString.RawString = tbRawString.Text;
            if (this.ContentChanged != null)
                this.ContentChanged(this, new EventArgs());
        }
    }
}
