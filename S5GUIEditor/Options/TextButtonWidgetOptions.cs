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
    public partial class TextButtonWidgetOptions : WidgetOptions
    {
        TextButtonWidget activeWidget;
        public TextButtonWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            activeWidget = widget as TextButtonWidget;
            s5WritingOptions.Initialize(activeWidget.ButtonText, "Button Text:");
        }
    }
}
