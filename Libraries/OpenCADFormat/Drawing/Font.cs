using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public enum TextDecoration { None = 0, Underline = 1, Overline = 2, StrikeThrough = 4 }

    public enum TextAlignment { Leading, Middle, Trailing }

    public enum TextBaselineShift { Baseline, Subscript, Superscript }

    public enum FontWeight { Regular, Light, SemiBold, Bold, Black }

    public enum FontStyle { Normal, Italic, Oblique }

    public class Font
    {
        public static readonly Font Default = new Font("Arial, Verdana", new Measurement(200, Units.Length.Mil));

        public Font(string family, Measurement height,
            FontWeight weight = FontWeight.Regular, FontStyle style = FontStyle.Normal)
        {
            Family = family;
            Height = height;
            Weight = weight;
            Style = style;
        }

        public string Family { get; private set; }
        public FontWeight Weight { get; private set; }
        public FontStyle Style { get; private set; }
        public Measurement Height { get; private set; }
    }
}