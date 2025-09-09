using System.Collections.Generic;

namespace Lunar.Framework.MooaLewaUI.Base;

public class UIRoot : List<object> { }

public static class Utils
{

    public static string LoadFont(string fontName)
    {
        return $"TempLoadFont: {fontName}";
    }

    public static string LoadTexture(string textureName)
    {
        return $"TempLoadTexture: {textureName}";
    }
}

public class TextBlock
{
    public string Text { get; set; }
    public string Font { get; set; }
}

public class Sprite
{
    public string Source { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

public static class Resources
{
    public static string LoadFont(string path) => path;
    public static string LoadTexture(string path) => path;
}
