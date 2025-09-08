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
            
            // 遍历所有子元素
            foreach (var element in root.Elements())
            {
                // 使用注册表获取对应的节点类型
                var nodeType = NodeRegistry.GetNodeType(element.Name.LocalName);

                // 使用反射创建节点实例
                if (Activator.CreateInstance(nodeType) is IMlXamlNode node)
                {
                    // 从XElement加载数据到节点实例
                    // node.Load(element); // 假设IXamlNode有Load方法
                    nodes.Add(node);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"解析XML时出错: {ex.Message}");
            // 根据需要处理异常
        }
        
        return nodes;
    }
}