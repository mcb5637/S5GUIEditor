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
    public partial class UpdateWidgetOptions : UserControl
    {
        WidgetUpdate curWidgetUpdate;
        public UpdateWidgetOptions()
        {
            InitializeComponent();
        }

        public void Initialize(WidgetUpdate update)
        {
            curWidgetUpdate = update;
            tbLuaUpdateCommand.Text = curWidgetUpdate.LuaUpdateCommand;
            cbUpdateManualFlag.Checked = curWidgetUpdate.UpdateManualFlag;
        }

        private void tbLuaUpdateCommand_TextChanged(object sender, EventArgs e)
        {
            curWidgetUpdate.LuaUpdateCommand = tbLuaUpdateCommand.Text;
        }

        private void cbUpdateManualFlag_CheckedChanged(object sender, EventArgs e)
        {
            curWidgetUpdate.UpdateManualFlag = cbUpdateManualFlag.Checked;
        }
    }
}
