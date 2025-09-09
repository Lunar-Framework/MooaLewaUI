using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Lunar.Core.Base.Interfaces;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator;

public class MlXamlParser
{
    internal static List<IMlXamlNode> Parse(string xmlString, ILogger logger)
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

                node.Load(element, logger);
                
                nodes.Add(node);
            }
        }
        catch (Exception ex)
        {
            logger.LogError($"An error occurred while parsing XML: {ex.Message}");
        }

        return nodes;
    }
}