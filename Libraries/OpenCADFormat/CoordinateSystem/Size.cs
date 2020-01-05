using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public struct Size
    {
        public static readonly Size Zero = new Size(Scalar.Zero, Scalar.Zero);

        public static Size Add(Size a, Size b) => new Size(a.Width + b.Width, a.Height + b.Height);
        public static Size Subtract(Size a, Size b) => new Size(a.Width - b.Width, a.Height - b.Height);
        public static Size Divide(Size a, double b) => new Size(a.Width / (Scalar)b, a.Height / (Scalar)b);

        public static Size operator +(Size a, Size b) => Add(a, b);
        public static Size operator -(Size a, Size b) => Subtract(a, b);
        public static Size operator /(Size a, double b) => Divide(a, b);

        public Size(Scalar width, Scalar height)
        {
            Width = width;
            Height = height;
        }

        public Size ConvertTo(Unit unit) => new Size(Width.ConvertTo(unit), Height.ConvertTo(unit));

        public Size ConvertToPixels() => new Size(Width.ConvertTo(Unit.Parse("PixelX")), Height.ConvertTo(Unit.Parse("PixelY")));

        public Scalar Width { get; }
        public Scalar Height { get; }
    }
}
