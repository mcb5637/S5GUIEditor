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
    public partial class PureTooltipWidgetOptions : WidgetOptions
    {
        PureTooltipWidget activeWidget;
        public PureTooltipWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            activeWidget = widget as PureTooltipWidget;
            tooltipOptions.Initialize(activeWidget.Tooltip);
        }
    }
}
