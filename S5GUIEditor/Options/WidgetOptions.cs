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
    public partial class WidgetOptions : UserControl
    {
        public WidgetOptions()
        {
            InitializeComponent();
        }

        public virtual void LoadOptions(Widget widget) { }
    }
}
