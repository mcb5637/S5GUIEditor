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
    public partial class BaseWidgetOptions : WidgetOptions
    {
        Widget activeWidget;
        public BaseWidgetOptions()
        {
            InitializeComponent();
        }

        public override void LoadOptions(Widget widget)
        {
            activeWidget = widget;
            tbWidgetName.Text = widget.Name;
            tbGroup.Text = widget.Group;
            tBXPos.Text = widget.PositionAndSize.X.ToString();
            tbYPos.Text = widget.PositionAndSize.Y.ToString();
            tbWidth.Text = widget.PositionAndSize.Width.ToString();
            tbHeight.Text = widget.PositionAndSize.Height.ToString();
            tbZPriority.Text = widget.ZPriority.ToString();
            cbFTHMEF.Checked = widget.ForceToHandleMouseEventsFlag;
            cbFTNBFF.Checked = widget.ForceToNeverBeFoundFlag;
            cbIsShown.Checked = widget.IsShown;
        }

        private void tbWidgetName_TextChanged(object sender, EventArgs e)
        {
            activeWidget.Name = tbWidgetName.Text;
            activeWidget.TreeNode.Text = tbWidgetName.Text;
        }

        private void tBXPos_TextChanged(object sender, EventArgs e)
        {
            int x = 0;
            int.TryParse(tBXPos.Text, out x);
            activeWidget.PositionAndSize = new RectangleF(new PointF(x, activeWidget.PositionAndSize.Y), activeWidget.PositionAndSize.Size);
        }

        private void tbYPos_TextChanged(object sender, EventArgs e)
        {
            int y = 0;
            int.TryParse(tbYPos.Text, out y);
            activeWidget.PositionAndSize = new RectangleF(new PointF(activeWidget.PositionAndSize.X, y), activeWidget.PositionAndSize.Size);
        }

        private void tbWidth_TextChanged(object sender, EventArgs e)
        {
            int w = 0;
            int.TryParse(tbWidth.Text, out w);
            activeWidget.PositionAndSize = new RectangleF(activeWidget.PositionAndSize.Location, new SizeF(w, activeWidget.PositionAndSize.Height));
        }

        private void tbHeight_TextChanged(object sender, EventArgs e)
        {
            int h = 0;
            int.TryParse(tbHeight.Text, out h);
            activeWidget.PositionAndSize = new RectangleF(activeWidget.PositionAndSize.Location, new SizeF(activeWidget.PositionAndSize.Width, h));
        }

        private void cbIsShown_CheckedChanged(object sender, EventArgs e)
        {
            activeWidget.IsShown = cbIsShown.Checked;
        }

        private void cbFTHMEF_CheckedChanged(object sender, EventArgs e)
        {
            activeWidget.ForceToHandleMouseEventsFlag = cbFTHMEF.Checked;
        }

        private void cbFTNBFF_CheckedChanged(object sender, EventArgs e)
        {
            activeWidget.ForceToNeverBeFoundFlag = cbFTNBFF.Checked;
        }

        private void tbGroup_TextChanged(object sender, EventArgs e)
        {
            activeWidget.Group = tbGroup.Text;
        }

        private void tbZPriority_TextChanged(object sender, EventArgs e)
        {
            float zpriority = 0;
            float.TryParse(tbZPriority.Text, out zpriority);
            activeWidget.ZPriority = zpriority;
        }
    }
}
