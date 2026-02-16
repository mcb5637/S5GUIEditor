using System.ComponentModel;
using System.Xml.Linq;

namespace S5GUIEditor2.Widgets;

internal class CSingleStringHandler : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    internal string StringTableKey
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(StringTableKey));
            OnPropertyChanged(nameof(ToRender));
        }
    } = "";

    internal string RawString
    {
        get;
        set
        {
            field = value;
            OnPropertyChanged(nameof(RawString));
            OnPropertyChanged(nameof(ToRender));
        }
    } = "";

    internal static CSingleStringHandler FromXml(XElement? e)
    {
        return new CSingleStringHandler()
        {
            StringTableKey = e?.Element("StringTableKey")?.Value ?? "",
            RawString = e?.Element("RawString")?.Value ?? "",
        };
    }

    public XElement[] ToXml()
    {
        return
        [
            new XElement("StringTableKey", StringTableKey),
            new XElement("RawString", RawString)
        ];
    }
    
    public string ToLua(string escapedname)
    {
        if (StringTableKey.Length > 0)
            return $"XGUIEng.SetTextKeyName({escapedname}, \"{StringTableKey}\")\n";
        else
            return $"XGUIEng.SetText({escapedname}, \"{RawString}\", 1)\n";
    }

    internal string ToRender => StringTableKey != "" ? StringTableKey : RawString;
}