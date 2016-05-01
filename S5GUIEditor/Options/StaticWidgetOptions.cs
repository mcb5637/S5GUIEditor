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
    public partial class StaticWidgetOptions : WidgetOptions
    {
        StaticWidget activeWidget;
        public StaticWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            activeWidget = widget as StaticWidget;
            tOBackground.Initialize(activeWidget.Background, "Background Texture:");
        }
    }
}
