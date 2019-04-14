
using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public struct Size
    {
        public static readonly Size Zero = new Size(Measurement.Zero, Measurement.Zero);

        public static Size Add(Size a, Size b) => new Size(a.Width + b.Width, a.Height + b.Height);
        public static Size Subtract(Size a, Size b) => new Size(a.Width - b.Width, a.Height - b.Height);
        public static Size Divide(Size a, double b) => new Size(a.Width / b, a.Height / b);

        public static Size operator +(Size a, Size b) => Add(a, b);
        public static Size operator -(Size a, Size b) => Subtract(a, b);
        public static Size operator /(Size a, double b) => Divide(a, b);

        public Size(Measurement width, Measurement height)
        {
            Validation.Expect(Quantities.Length, width);
            Validation.Expect(Quantities.Length, height);

            Width = width;
            Height = height;
        }

        public Size ConvertTo(Unit unit) => new Size(Width.ConvertToUnit(unit),
            Height.ConvertToUnit(unit));

        public Measurement Width { get; set; }
        public Measurement Height { get; set; }
    }
}
