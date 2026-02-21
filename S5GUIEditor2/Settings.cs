using System;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace S5GUIEditor2;

public class Settings
{
    public string WorkspacePath { get; set; } = "";
    public string? LastLoadedXml { get; set; }

    private const string Data = "data/";
    
    internal string ToS5Path(string path)
    {
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(WorkspacePath))
            return path;
        path = Path.GetRelativePath(WorkspacePath, path);
        if (path.StartsWith('.'))
            path = path[2..];
        path = Data + path;
        path = path.Replace('/', '\\');
        return path;
    }

    internal string ResolveS5Path(string path)
    {
        path = path.Replace('\\', '/');
        if (path.StartsWith(Data, StringComparison.InvariantCultureIgnoreCase))
            path = path.Remove(0, Data.Length);
        path = Path.Combine(WorkspacePath, path);
        if (!OperatingSystem.IsWindows())
        {
            string dir = Path.GetDirectoryName(path)!;
            string? c = Directory.EnumerateFiles(dir).FirstOrDefault(
                x => path.Equals(x, StringComparison.InvariantCultureIgnoreCase)
            );
            if (c != null)
                return c;
        }
        return path;
    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Settings))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
