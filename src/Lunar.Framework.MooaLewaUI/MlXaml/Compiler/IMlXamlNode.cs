namespace Lunar.Framework.MooaLewaUI.MlXaml.Compiler;

public interface IMlXamlNode
{
    // 定义通用的节点属性和方法，例如：
    // void Load(XElement element); // 从XElement加载数据
    // void Render(); // 渲染节点
}

public class TextBlockNode : IMlXamlNode
{
    // TextBlock 特有的属性
    public string Text { get; set; }
    public string Font { get; set; }

    // 实现 Load 方法，从XElement加载数据
    // public void Load(XElement element) { ... }
}

public class SpriteNode : IMlXamlNode
{
    // Sprite 特有的属性
    public string Source { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}

// 未来可以添加更多的节点
// public class ButtonNode : IMlXamlNode { ... }