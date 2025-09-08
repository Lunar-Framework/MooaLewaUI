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
                if (NodeRegistry.TryCreate(element.Name.LocalName, out var node))
                {
                    // node.Load(element); // 如果需要，可以从 XElement 初始化属性
                    nodes.Add(node);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"解析XML时出错: {ex.Message}");
        }

        return nodes;
    }
}