using System.Collections.Generic;
using Lunar.Framework.MooaLewaUI.Base;
using Lunar.Framework.MooaLewaUI.Layout;

namespace Lunar.Framework.MooaLewaUI.Controls;

public abstract class UIElement
{
    public Thickness Margin { get; set; } // 外边距
    public HorizontalAlignment HAlign { get; set; } // Left/Center/Right/Stretch
    public VerticalAlignment VAlign { get; set; } // Top/Center/Bottom/Stretch
    public Size? DesiredSize { get; set; } // 希望大小（可选）

    internal Rect LayoutRect { get; private set; } // 最终计算出的矩形（只读）

    public List<UIElement> Children { get; } = new();
    //public abstract void Render(IRenderContext ctx);
}