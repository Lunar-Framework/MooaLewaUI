using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator;

internal interface IMlXamlNode
{
    void Load(XElement element, List<Diagnostic> diagnostics);
}

internal class TextBlockNode : IMlXamlNode
{
    public string? Text { get; set; }
    public string? Font { get; set; }

    public void Load(XElement element, List<Diagnostic> diagnostics)
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

    public void Load(XElement element, List<Diagnostic> diagnostics)
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
                        var diagnostic = Diagnostic.Create(
                            new DiagnosticDescriptor("ML001", "Invalid attribute value",
                                "Could not parse attribute 'X' with value '{0}'. Defaulting to 0.", "MlXaml",
                                DiagnosticSeverity.Warning, true),
                            Location.None, // Ideally, map this to a line/column in the .mlxaml file
                            attribute.Value
                        );
                        diagnostics.Add(diagnostic);
                        xValue = 0;
                    }

                    X = xValue;
                    break;
                }
                case "Y":
                {
                    if (!int.TryParse(attribute.Value, out var yValue))
                    {
                        var diagnostic = Diagnostic.Create(
                            new DiagnosticDescriptor("ML001", "Invalid attribute value",
                                "Could not parse attribute 'Y' with value '{0}'. Defaulting to 0.", "MlXaml",
                                DiagnosticSeverity.Warning, true),
                            Location.None, // Ideally, map this to a line/column in the .mlxaml file
                            attribute.Value
                        );
                        diagnostics.Add(diagnostic);
                        yValue = 0;
                    }

                    Y = yValue;
                    break;
                }
            }
        }
    }
}