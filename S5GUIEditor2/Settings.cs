using System;
using System.IO;
using System.Text.Json.Serialization;

namespace S5GUIEditor2;

public class Settings
{
    public string WorkspacePath { get; set; } = "";

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
        return Path.Combine(WorkspacePath, path);
    }
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Settings))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
