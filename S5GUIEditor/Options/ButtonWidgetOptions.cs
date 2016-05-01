using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using S5GUIEditor.Options;

namespace S5GUIEditor.Options
{
    public partial class ButtonWidgetOptions : WidgetOptions
    {
        ButtonWidget activeWidget;
        public ButtonWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            activeWidget = widget as ButtonWidget;
            s5StringOptions.Initialize(activeWidget.ShortCutString);
            updateWidgetOptions.Initialize(activeWidget.Update);
            tONormal.Initialize(activeWidget.ButtonNormal, "Normal:");
            tOHover.Initialize(activeWidget.ButtonHover, "Hover:");
            tOPressed.Initialize(activeWidget.ButtonPressed, "Pressed:");
            tODisabled.Initialize(activeWidget.ButtonDisabled, "Disabled:");
            tOHighLighted.Initialize(activeWidget.ButtonHighlighted, "Highlighted:");
            cbDisabled.Checked = activeWidget.Disabled;
            cbHighLighted.Checked = activeWidget.HighLighted;
            tbLuaCommand.Text = activeWidget.LuaCommand;
            tooltipOptions.Initialize(activeWidget.ToolTipHelper);
        }

        private void cbDisabled_CheckedChanged(object sender, EventArgs e)
        {
            activeWidget.Disabled = cbDisabled.Checked;
        }

        private void cbHighLighted_CheckedChanged(object sender, EventArgs e)
        {
            activeWidget.HighLighted = cbHighLighted.Checked;
        }

        private void tbLuaCommand_TextChanged(object sender, EventArgs e)
        {
            activeWidget.LuaCommand = tbLuaCommand.Text;
        }
    }
}
