using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;

namespace S5GUIEditor2;

public class Settings
{
    public string WorkspacePath { get; set; } = "";
    public string? LastLoadedXml { get; set; }
    
    internal Func<IEnumerable<string>>? ExistingFileList { get; set; }

    private const string Data = "data/";
    
    internal string ToS5Path(string path)
    {
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(WorkspacePath))
            return path;
        path = Path.GetRelativePath(WorkspacePath, path);
        if (path.StartsWith('.'))
            path = path[2..];
        string with = (Data + path).Replace('/', '\\');
        path = path.Replace('/', '\\');
        var d = ExistingFileList?.Invoke().Where(x => !string.IsNullOrEmpty(x)).FirstOrDefault(x =>
            x.Equals(path, StringComparison.InvariantCultureIgnoreCase) ||
            x.Equals(with, StringComparison.InvariantCultureIgnoreCase));
        return d ?? with;
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
