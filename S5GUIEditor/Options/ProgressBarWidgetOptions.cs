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
    public partial class ProgressBarWidgetOptions : WidgetOptions
    {
        ProgressBarWidget activeWidget;
        public ProgressBarWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            activeWidget = widget as ProgressBarWidget;
            toBackground.Initialize(activeWidget.Background, "Background Texture:");
            updateWidgetOptions.Initialize(activeWidget.Update);
            tbProgressBarLimit.Text = activeWidget.ProgressBarLimit.ToString();
            tbProgressBarValue.Text = activeWidget.ProgressBarValue.ToString();
        }
        private void tbProgressBarValue_TextChanged(object sender, EventArgs e)
        {
            float pbv = 0;
            float.TryParse(tbProgressBarValue.Text, out pbv);
            if (pbv > activeWidget.ProgressBarLimit)
                pbv = activeWidget.ProgressBarLimit;
            activeWidget.ProgressBarValue = pbv;
        }

        private void tbProgressBarLimit_TextChanged(object sender, EventArgs e)
        {
            float pbl = 100;
            float.TryParse(tbProgressBarLimit.Text, out pbl);
            activeWidget.ProgressBarLimit = pbl;
        }
    }
}
