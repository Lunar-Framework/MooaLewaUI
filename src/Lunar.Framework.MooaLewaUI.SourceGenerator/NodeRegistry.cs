using System;
using System.Collections.Generic;

namespace Lunar.Framework.MooaLewaUI.SourceGenerator;

internal static class NodeRegistry
{
    private static readonly Dictionary<string, Func<IMlXamlNode>> Factories
        = new();

    static NodeRegistry()
    {
        Register("TextBlock", () => new TextBlockNode());
        Register("Sprite", () => new SpriteNode());
    }

    private static void Register(string nodeName, Func<IMlXamlNode> factory)
    {
        Factories[nodeName] = factory;
    }

    public static bool TryCreate(string nodeName, out IMlXamlNode node)
    {
        if (Factories.TryGetValue(nodeName, out var factory))
        {
            node = factory();
            return true;
        }

        node = null!;
        return false;
    }
}