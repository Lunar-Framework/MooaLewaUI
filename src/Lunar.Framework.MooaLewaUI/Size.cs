using System;
using System.Globalization;

namespace Lunar.Framework.MooaLewaUI;

public readonly struct Size : IEquatable<Size>
{
    public static readonly Size Infinity = new(double.PositiveInfinity, double.PositiveInfinity);

    public Size(double width, double height)
    {
        Width = width;
        Height = height;
    }

    public double AspectRatio => Width / Height;

    public double Width { get; }

    public double Height { get; }

    public static bool operator ==(Size left, Size right)
    {
        return left.Equals(right);
    }

    public static bool operator !=(Size left, Size right)
    {
        return !(left == right);
    }

    public static Size operator *(Size size, Vector scale)
    {
        return new Size(size.Width * scale.X, size.Height * scale.Y);
    }

    public static Size operator /(Size size, Vector scale)
    {
        return new Size(size.Width / scale.X, size.Height / scale.Y);
    }

    public static Vector operator /(Size left, Size right)
    {
        return new Vector(left.Width / right.Width, left.Height / right.Height);
    }

    public static Size operator *(Size size, double scale)
    {
        return new Size(size.Width * scale, size.Height * scale);
    }

    public static Size operator /(Size size, double scale)
    {
        return new Size(size.Width / scale, size.Height / scale);
    }

    public static Size operator +(Size size, Size toAdd)
    {
        return new Size(size.Width + toAdd.Width, size.Height + toAdd.Height);
    }

    public static Size operator -(Size size, Size toSubtract)
    {
        return new Size(size.Width - toSubtract.Width, size.Height - toSubtract.Height);
    }
    
    /// <summary>
    ///     Constrains the size.
    /// </summary>
    /// <param name="constraint">The size to constrain to.</param>
    /// <returns>The constrained size.</returns>
    public Size Constrain(Size constraint)
    {
        return new Size(
            Math.Min(Width, constraint.Width),
            Math.Min(Height, constraint.Height));
    }

    public Size Deflate(Thickness thickness)
    {
        var width = Width - thickness.Left - thickness.Right;
        if (width < 0)
        {
            width = 0;
        }

        var height = Height - thickness.Top - thickness.Bottom;
        if (height < 0)
        {
            height = 0;
        }

        return new Size(width, height);
    }

    public bool Equals(Size other)
    {
        // ReSharper disable CompareOfFloatsByEqualityOperator
        return Width == other.Width &&
               Height == other.Height;
        // ReSharper enable CompareOfFloatsByEqualityOperator
    }

    public bool NearlyEquals(Size other)
    {
        return MathUtilities.AreClose(Width, other.Width) &&
               MathUtilities.AreClose(Height, other.Height);
    }

    public override bool Equals(object? obj)
    {
        return obj is Size other && Equals(other);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 23 + Width.GetHashCode();
            hash = hash * 23 + Height.GetHashCode();
            return hash;
        }
    }

    public Size Inflate(Thickness thickness)
    {
        return new Size(
            Width + thickness.Left + thickness.Right,
            Height + thickness.Top + thickness.Bottom);
    }

    /// <summary>
    ///     Returns a new <see cref="Size" /> with the same height and the specified width.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <returns>The new <see cref="Size" />.</returns>
    public Size WithWidth(double width)
    {
        return new Size(width, Height);
    }

    /// <summary>
    ///     Returns a new <see cref="Size" /> with the same width and the specified height.
    /// </summary>
    /// <param name="height">The height.</param>
    /// <returns>The new <see cref="Size" />.</returns>
    public Size WithHeight(double height)
    {
        return new Size(Width, height);
    }

    /// <summary>
    ///     Returns the string representation of the size.
    /// </summary>
    /// <returns>The string representation of the size.</returns>
    public override string ToString()
    {
        return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", Width, Height);
    }

    /// <summary>
    ///     Deconstructs the size into its Width and Height values.
    /// </summary>
    /// <param name="width">The width.</param>
    /// <param name="height">The height.</param>
    public void Deconstruct(out double width, out double height)
    {
        width = Width;
        height = Height;
    }
}