using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Media.Imaging;

namespace S5GUIEditor2;

internal class ImageCache
{
    internal required Settings? S { get; init; }
    
    private Dictionary<string, Bitmap?> Cache = new();

    private const string Data = "data/";
    
    private Bitmap? LoadImage(string path)
    {
        try
        {
            path = path.Replace('\\', '/');
            if (path.StartsWith(Data, StringComparison.InvariantCultureIgnoreCase))
                path = path.Remove(0, Data.Length);
            var fullPath = Path.Combine(S!.WorkspacePath, path);
            return new Bitmap(fullPath);
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