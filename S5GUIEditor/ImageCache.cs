using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.IO;
using DevIL;

namespace S5GUIEditor
{
    public static class ImageCache
    {
        static private Dictionary<string, Image> cache = new Dictionary<string, Image>();

        public static void Clear()
        {
            cache = new Dictionary<string, Image>();
        }

        public static Image LoadImage(string path)
        {
            path = Path.GetFullPath(path);
            if (cache.ContainsKey(path))
                return cache[path];
            else if (File.Exists(path))
            {
                Image im = DevIL.DevIL.LoadBitmap(path);
                if (im == null)
                    return null;

                cache.Add(path, im);
                return im;
            }
            return null;
        }
    }

    public static class FontCache
    {
        static Dictionary<string, BitmapFont> cache = new Dictionary<string, BitmapFont>();

        public static void Clear()
        {
            cache = new Dictionary<string, BitmapFont>();
        }

        public static BitmapFont LoadFont(string path)
        {
            path = Path.GetFullPath(path);
            if (cache.ContainsKey(path))
                return cache[path];
            else if (File.Exists(path))
            {
                BitmapFont bm = BitmapFont.CreateFromMetrics(path);
                if (bm == null)
                    return null;

                cache.Add(path, bm);
                return bm;
            }
            else
                return null;
        }
    }
}
