using System;
using System.Collections.Generic;

namespace Lunar.Framework.MooaLewaUI;

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

public readonly struct Thickness : IEquatable<Thickness>
{
    public Thickness(double uniformLength)
    {
        Left = Top = Right = Bottom = uniformLength;
    }

    public Thickness(double horizontal, double vertical)
    {
        Left = Right = horizontal;
        Top = Bottom = vertical;
    }

    public Thickness(double left, double top, double right, double bottom)
    {
        Left = left;
        Top = top;
        Right = right;
        Bottom = bottom;
    }

    public double Left { get; }

    public double Top { get; }

    public double Right { get; }

    public double Bottom { get; }

    public bool IsUniform => Left.Equals(Right) && Top.Equals(Bottom) && Right.Equals(Bottom);

    public bool Equals(Thickness other)
    {
        return Left.Equals(other.Left) && Top.Equals(other.Top) && Right.Equals(other.Right) &&
               Bottom.Equals(other.Bottom);
    }

    public override bool Equals(object obj)
    {
        return obj is Thickness other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = Left.GetHashCode();
            hashCode = (hashCode * 397) ^ Top.GetHashCode();
            hashCode = (hashCode * 397) ^ Right.GetHashCode();
            hashCode = (hashCode * 397) ^ Bottom.GetHashCode();
            return hashCode;
        }
    }
}

public enum HorizontalAlignment
{
    Stretch,
    Left,
    Center,
    Right
}

public enum VerticalAlignment
{
    Stretch,
    Top,
    Center,
    Bottom
}