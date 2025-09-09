using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Lunar.Framework.MooaLewaUI.MlXaml.Compiler;

public class MlXamlParser
{
    public static List<IMlXamlNode> Parse(string xmlString)
    {
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
                
                node.Load(element);
                
                nodes.Add(node);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while parsing XML: {ex.Message}");
        }

        return nodes;
    }
}