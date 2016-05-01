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
    public partial class TooltipOptions : UserControl
    {
        Tooltip curTooltip;
        public TooltipOptions()
        {
            InitializeComponent();
        }

        public void Initialize(Tooltip tooltip)
        {
            curTooltip = tooltip;
            cbControlTargetWidgetDisplayState.Checked = curTooltip.ControlTargetWidgetDisplayState;
            cbEnabled.Checked = curTooltip.EnabledFlag;
            tbLuaCommand.Text = curTooltip.LuaUpdateCommand;
            tbTargetWidget.Text = curTooltip.TargetWidget;
            s5soText.Initialize(curTooltip.Text);
        }
        private void cbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            curTooltip.EnabledFlag = cbEnabled.Checked;
        }

        private void cbControlTargetWidgetDisplayState_CheckedChanged(object sender, EventArgs e)
        {
            curTooltip.ControlTargetWidgetDisplayState = cbControlTargetWidgetDisplayState.Checked;
        }

        private void tbTargetWidget_TextChanged(object sender, EventArgs e)
        {
            curTooltip.TargetWidget = tbTargetWidget.Text;
        }

        private void tbLuaCommand_TextChanged(object sender, EventArgs e)
        {
            curTooltip.LuaUpdateCommand = tbLuaCommand.Text;
        }
    }
}
