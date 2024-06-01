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
    public partial class CustomWidgetOptions : WidgetOptions
    {
        CustomWidget activeWidget;
        int[] intUserVarDefaultValues;
        string[] stringUserVarDefaultValues;
        bool updating = false;
        public CustomWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            updating = true;
            activeWidget = widget as CustomWidget;
            tbCustomClassName.Text = activeWidget.CustomClassName;
            tbCustomClassName.Items.Clear();
            tbCustomClassName.Items.AddRange(CustomWidget.KnownWidgetTypes.Keys.ToArray());
            intUserVarDefaultValues = new int[] {
                activeWidget.IntegerUserVariable0DefaultValue,
                activeWidget.IntegerUserVariable1DefaultValue,
                activeWidget.IntegerUserVariable2DefaultValue,
                activeWidget.IntegerUserVariable3DefaultValue,
                activeWidget.IntegerUserVariable4DefaultValue,
                activeWidget.IntegerUserVariable5DefaultValue
            };
            stringUserVarDefaultValues = new string[] {

                activeWidget.StringUserVariable0DefaultValue,
                activeWidget.StringUserVariable1DefaultValue
            };
            cbIntUserVar.SelectedIndex = 0;
            cbStringUserVar.SelectedIndex = 0;
            tbIntUserVarDefaultValue.Text = activeWidget.IntegerUserVariable0DefaultValue.ToString();
            tbStringUserVarDefaultValue.Text = activeWidget.StringUserVariable0DefaultValue;
            updating = false;
            UpdateCustomClass();
        }

        private void TbCustomClassName_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updating)
                return;
            MessageBox.Show("t");
            activeWidget.CustomClassName = tbCustomClassName.Text;
            UpdateCustomClass();
        }

        private void tbIntUserVarDefaultValue_TextChanged(object sender, EventArgs e)
        {
            if (updating)
                return;
            int.TryParse(tbIntUserVarDefaultValue.Text, out int value);
            int nr = int.Parse(cbIntUserVar.Text);
            switch (nr)
            {
                case 0:
                    activeWidget.IntegerUserVariable0DefaultValue = value;
                    break;
                case 1:
                    activeWidget.IntegerUserVariable1DefaultValue = value;
                    break;
                case 2:
                    activeWidget.IntegerUserVariable2DefaultValue = value;
                    break;
                case 3:
                    activeWidget.IntegerUserVariable3DefaultValue = value;
                    break;
                case 4:
                    activeWidget.IntegerUserVariable4DefaultValue = value;
                    break;
                case 5:
                    activeWidget.IntegerUserVariable5DefaultValue = value;
                    break;
            }
            intUserVarDefaultValues[nr] = value;
        }

        private void cbIntUserVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updating)
                return;
            tbIntUserVarDefaultValue.Text = intUserVarDefaultValues[cbIntUserVar.SelectedIndex].ToString();
            UpdateCustomClass();
        }

        private void tbStringUserVarDefaultValue_TextChanged(object sender, EventArgs e)
        {
            if (updating)
                return;
            string value = tbStringUserVarDefaultValue.Text;
            int nr = int.Parse(cbStringUserVar.Text);
            switch (nr)
            {
                case 0:
                    activeWidget.StringUserVariable0DefaultValue = value;
                    break;
                case 1:
                    activeWidget.StringUserVariable1DefaultValue = value;
                    break;
            }
            stringUserVarDefaultValues[nr] = value;
        }

        private void cbStringUserVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (updating)
                return;
            tbStringUserVarDefaultValue.Text = stringUserVarDefaultValues[cbStringUserVar.SelectedIndex];
            UpdateCustomClass();
        }

        private void UpdateCustomClass()
        {
            var o = CustomWidget.TryGet(activeWidget.CustomClassName);
            lbIntUserVarEx.Text = o != null ? o.IntVar(int.Parse(cbIntUserVar.Text)) : "";
            lbStringUserVarEx.Text = o != null ? o.StringVar(int.Parse(cbStringUserVar.Text)) : "";
        }

        private void TbCustomClassName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string s = tbCustomClassName.Text;

                if (!tbCustomClassName.Items.Contains(s))
                {
                    tbCustomClassName.Items.Add(s);
                    tbCustomClassName.SelectedItem = s;
                }

                e.Handled = true;
            }
        }

        private void BtnEncodeColor_Click(object sender, EventArgs e)
        {
            ColorChooser ch = new ColorChooser();
            if (int.TryParse(tbIntUserVarDefaultValue.Text, out int argb))
                ch.Color = Color.FromArgb(argb);
            else
                ch.Color = Color.Black;
            if (ch.ShowDialog() == DialogResult.OK)
            {
                tbIntUserVarDefaultValue.Text = (ch.Color.ToArgb()).ToString();
            }
        }
    }
}
