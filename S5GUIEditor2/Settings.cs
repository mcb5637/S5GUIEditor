using System.Text.Json.Serialization;

namespace S5GUIEditor2;

public class Settings
{
    public string WorkspacePath { get; set; } = "";
}

[JsonSourceGenerationOptions(WriteIndented = true)]
[JsonSerializable(typeof(Settings))]
internal partial class SourceGenerationContext : JsonSerializerContext
{
}
