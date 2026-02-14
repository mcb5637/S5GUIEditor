using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Media.Imaging;

namespace S5GUIEditor2;

internal class ImageCache
{
    internal required Settings? S { get; init; }
    
    private Dictionary<string, Bitmap?> Cache = new();

    private Bitmap? LoadImage(string path)
    {
        if (string.IsNullOrEmpty(path))
            return null;
        try
        {
            return new Bitmap(S!.ResolveS5Path(path));
        }
        catch (IOException)
        {
            return null;
        }
    }

    internal Bitmap? Get(string path)
    {
        if (Cache.TryGetValue(path, out Bitmap? r))
            return r;
        Bitmap? l = LoadImage(path);
        Cache[path] = l;
        return l;
    }
}