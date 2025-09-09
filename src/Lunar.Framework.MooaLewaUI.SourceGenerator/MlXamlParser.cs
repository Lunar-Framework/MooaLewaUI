using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator;

internal class MlXamlParser
{
    public static List<IMlXamlNode> Parse(string xmlString, out List<Diagnostic> diagnostics)
    {
        diagnostics = new List<Diagnostic>();
        var nodes = new List<IMlXamlNode>();
        try
        {
            var root = XElement.Parse(xmlString);

            foreach (var element in root.Elements())
            {
                if (!NodeRegistry.TryCreate(element.Name.LocalName, out var node))
                {
                    continue;
                }

                node.Load(element, diagnostics);

                nodes.Add(node);
            }
        }
        catch (Exception ex)
        {
            diagnostics.Add(Diagnostic.Create(
                new DiagnosticDescriptor("ML002", "XML Parsing Error", "An error occurred while parsing XML: {0}",
                    "MlXaml", DiagnosticSeverity.Error, true),
                Location.None,
                ex.Message
            ));
        }

        return nodes;
    }
}