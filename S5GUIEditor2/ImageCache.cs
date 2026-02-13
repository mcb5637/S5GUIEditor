using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;

namespace S5GUIEditor2;

internal class ImageCache : IValueConverter
{
    internal required Settings? S { get; init; }
    
    private Dictionary<string, Bitmap> Cache = new();

    private const string Data = "data/";
    
    private Bitmap LoadImage(string path)
    {
        path = path.Replace('\\', '/');
        if (path.StartsWith(Data, StringComparison.InvariantCultureIgnoreCase))
            path = path.Remove(0, Data.Length);
        var fullPath = Path.Combine(S!.WorkspacePath, path);
        return new Bitmap(fullPath);
    }

    internal Bitmap Get(string path)
    {
        if (Cache.TryGetValue(path, out Bitmap? r))
            return r;
        Bitmap l = LoadImage(path);
        Cache[path] = l;
        return l;
    }
    
    public static ImageCache Instance { get; internal set; } = null!;
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (S == null)
            return Instance.Convert(value, targetType, parameter, culture);
        if (value is string path && targetType.IsAssignableFrom(typeof(Bitmap)))
        {
            return Get(path);
        }
        return new BindingNotification(new InvalidCastException(), BindingErrorType.Error);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}