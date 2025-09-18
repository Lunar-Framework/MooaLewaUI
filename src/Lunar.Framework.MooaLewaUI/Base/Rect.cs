using System;

namespace Lunar.Framework.MooaLewaUI.Base;

public readonly struct Rect : IEquatable<Rect>
{
    public bool Equals(Rect other)
    {
        return X.Equals(other.X) && Y.Equals(other.Y) && Width.Equals(other.Width) &&
               Height.Equals(other.Height);
    }

    public override bool Equals(object obj)
    {
        return obj is Rect other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = X.GetHashCode();
            hashCode = (hashCode * 397) ^ Y.GetHashCode();
            hashCode = (hashCode * 397) ^ Width.GetHashCode();
            hashCode = (hashCode * 397) ^ Height.GetHashCode();
            return hashCode;
        }
    }

    public Rect(double x, double y, double width, double height)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    public Rect(Size size)
    {
        X = 0;
        Y = 0;
        Width = size.Width;
        Height = size.Height;
    }

    public Rect(Point position, Size size)
    {
        X = position.X;
        Y = position.Y;
        Width = size.Width;
        Height = size.Height;
    }

    public Rect(Point topLeft, Point bottomRight)
    {
        X = topLeft.X;
        Y = topLeft.Y;
        Width = bottomRight.X - topLeft.X;
        Height = bottomRight.Y - topLeft.Y;
    }


    /// <summary>
    ///     Gets the X position.
    /// </summary>
    public double X { get; }

    /// <summary>
    ///     Gets the Y position.
    /// </summary>
    public double Y { get; }

    /// <summary>
    ///     Gets the width.
    /// </summary>
    public double Width { get; }

    /// <summary>
    ///     Gets the height.
    /// </summary>
    public double Height { get; }

    /// <summary>
    ///     Gets the position of the rectangle.
    /// </summary>
    public Point Position => new(X, Y);

    /// <summary>
    ///     Gets the size of the rectangle.
    /// </summary>
    public Size Size => new(Width, Height);

    /// <summary>
    ///     Gets the right position of the rectangle.
    /// </summary>
    public double Right => X + Width;

    /// <summary>
    ///     Gets the bottom position of the rectangle.
    /// </summary>
    public double Bottom => Y + Height;

    /// <summary>
    ///     Gets the left position.
    /// </summary>
    public double Left => X;

    /// <summary>
    ///     Gets the top position.
    /// </summary>
    public double Top => Y;

    /// <summary>
    ///     Gets the top left point of the rectangle.
    /// </summary>
    public Point TopLeft => new(X, Y);

    /// <summary>
    ///     Gets the top right point of the rectangle.
    /// </summary>
    public Point TopRight => new(Right, Y);

    /// <summary>
    ///     Gets the bottom left point of the rectangle.
    /// </summary>
    public Point BottomLeft => new(X, Bottom);

    /// <summary>
    ///     Gets the bottom right point of the rectangle.
    /// </summary>
    public Point BottomRight => new(Right, Bottom);

    /// <summary>
    ///     Gets the center point of the rectangle.
    /// </summary>
    public Point Center => new(X + Width / 2, Y + Height / 2);
}