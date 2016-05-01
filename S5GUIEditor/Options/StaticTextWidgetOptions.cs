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
    public partial class StaticTextWidgetOptions : WidgetOptions
    {
        StaticTextWidget activeWidget;
        public StaticTextWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            activeWidget = widget as StaticTextWidget;
            tOBackground.Initialize(activeWidget.Background, "Background Texture:");
            updateWidgetOptions.Initialize(activeWidget.Update);
            s5woText.Initialize(activeWidget.Text, "Text:");
            tbFirstLineToPrint.Text = activeWidget.FirstLineToPrint.ToString();
            tbLineDistanceFactor.Text = activeWidget.LineDistanceFactor.ToString();
            tbNumberOfLinesToPrint.Text = activeWidget.NumberOfLinesToPrint.ToString();
        }

        private void tbFirstLineToPrint_TextChanged(object sender, EventArgs e)
        {
            int fltp = 0;
            int.TryParse(tbFirstLineToPrint.Text, out fltp);
            activeWidget.FirstLineToPrint = fltp;
        }

        private void tbNumberOfLinesToPrint_TextChanged(object sender, EventArgs e)
        {
            int noltp = 0;
            int.TryParse(tbNumberOfLinesToPrint.Text, out noltp);
            activeWidget.NumberOfLinesToPrint = noltp;
        }

        private void tbLineDistanceFactor_TextChanged(object sender, EventArgs e)
        {
            float ldf = 0;
            float.TryParse(tbLineDistanceFactor.Text, out ldf);
            activeWidget.LineDistanceFactor = ldf;
        }
    }
}
