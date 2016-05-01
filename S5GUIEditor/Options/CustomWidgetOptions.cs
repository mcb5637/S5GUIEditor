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
    public partial class CustomWidgetOptions : WidgetOptions
    {
        CustomWidget activeWidget;
        int[] intUserVarDefaultValues;
        string[] stringUserVarDefaultValues;
        public CustomWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            activeWidget = widget as CustomWidget;
            tbCustomClassName.Text = activeWidget.CustomClassName;
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
        }

        private void tbCustomClassName_TextChanged(object sender, EventArgs e)
        {
            activeWidget.CustomClassName = tbCustomClassName.Text;
        }

        private void tbIntUserVarDefaultValue_TextChanged(object sender, EventArgs e)
        {
            int value = 0;
            int.TryParse(tbIntUserVarDefaultValue.Text, out value);
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
            tbIntUserVarDefaultValue.Text = intUserVarDefaultValues[cbIntUserVar.SelectedIndex].ToString();
        }

        private void tbStringUserVarDefaultValue_TextChanged(object sender, EventArgs e)
        {
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
            tbStringUserVarDefaultValue.Text = stringUserVarDefaultValues[cbStringUserVar.SelectedIndex];
        }
    }
}
