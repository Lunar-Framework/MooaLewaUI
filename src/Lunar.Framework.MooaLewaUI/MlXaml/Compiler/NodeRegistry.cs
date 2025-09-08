using System;
using System.Collections.Generic;

namespace Lunar.Framework.MooaLewaUI.MlXaml.Compiler;

public static class NodeRegistry
{
    // 存储XML标签名到其对应的C#节点类型的映射
    private static readonly Dictionary<string, Type> NodeTypes = new();

    static NodeRegistry()
    {
        // 注册已知的节点类型
        NodeTypes.Add("TextBlock", typeof(TextBlockNode));
        NodeTypes.Add("Sprite", typeof(SpriteNode));
        // 当你添加新节点时，只需要在这里注册即可
        // _nodeTypes.Add("Button", typeof(ButtonNode));
    }

    // 根据XML标签名获取对应的C#节点类型
    public static Type GetNodeType(string tagName)
    {
        NodeTypes.TryGetValue(tagName, out var nodeType);
        
        return nodeType ?? throw new InvalidOperationException();
    }
}