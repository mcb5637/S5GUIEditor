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
    public partial class GfxButtonWidgetOptions : WidgetOptions
    {
        GfxButtonWidget activeWidget;
        public GfxButtonWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            activeWidget = widget as GfxButtonWidget;
            tOIconMaterial.Initialize(activeWidget.IconMaterial, "Icon Material:");
        }
        
    }
}
