using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Drawing.Drawing2D;

namespace S5GUIEditor
{
    public static class GlobalSettings
    {
        public static string DataPath;
        public static string LastLoadPath;
        public static Font TextFont = new Font(FontFamily.GenericSansSerif, 12);
        public static Brush BgBrush = new SolidBrush(Color.FromArgb(25, 200, 200, 200));
        private static int uniqueInt = 0;
        public static InterpolationMode InterpolationMode = InterpolationMode.NearestNeighbor;
        public static int GetUniqueInt()
        {
            uniqueInt++;
            return uniqueInt;
        }
    }
}
