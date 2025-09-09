using System;
using System.Xml.Linq;
using Lunar.Core.Base.Interfaces;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator;

internal interface IMlXamlNode
{
    void Load(XElement element, ILogger logger);
}

internal class TextBlockNode : IMlXamlNode
{
    public string? Text { get; set; }
    public string? Font { get; set; }

    public void Load(XElement element, ILogger logger)
    {
        foreach (var attribute in element.Attributes())
        {
            switch (attribute.Name.LocalName)
            {
                case "Text":
                    Text = attribute.Value;
                    break;
                case "Font":
                    Font = attribute.Value;
                    break;
            }
        }
    }
}

internal class SpriteNode : IMlXamlNode
{
    public string? Source { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public void Load(XElement element, ILogger logger)
    {
        foreach (var attribute in element.Attributes())
        {
            switch (attribute.Name.LocalName)
            {
                case "Source":
                {
                    Source = attribute.Value;
                    break;
                }
                case "X":
                {
                    if (!int.TryParse(attribute.Value, out var xValue))
                    {
                        logger.LogWarning($"Warning: Could not parse attribute 'X' with value '{attribute.Value}'. Defaulting to 0.");
                        xValue = 0; 
                    }
                    X = xValue;
                    break;
                }
                case "Y":
                {
                    if (!int.TryParse(attribute.Value, out var yValue))
                    {
                        logger.LogWarning($"Warning: Could not parse attribute 'Y' with value '{attribute.Value}'. Defaulting to 0.");
                        yValue = 0; 
                    }
                    Y = yValue;
                    break;
                }
            }
        }
    }
}