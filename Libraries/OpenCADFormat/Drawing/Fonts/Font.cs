using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing.Fonts
{
    public class Font
    {
        public static readonly Font Default = new Font("Arial, Verdana, sans-serif", Scalar.Parse("200mil"));

        public Font(string family, Scalar height, FontWeight weight = FontWeight.Regular, FontStyle style = FontStyle.Normal)
        {
            Family = family;
            Height = height;
            Weight = weight;
            Style = style;
        }

        public Font ChangeWeight(FontWeight newWeight) => new Font(Family, Height, newWeight, Style);

        public Font ChangeStyle(FontStyle newStyle) => new Font(Family, Height, Weight, newStyle);

        public Font ChangeHeight(Scalar newHeight) => new Font(Family, newHeight, Weight, Style);

        public Font ChangeFamily(string newFamily) => new Font(newFamily, Height, Weight, Style);

        public readonly string Family;

        public readonly FontWeight Weight;

        public readonly FontStyle Style;

        public readonly Scalar Height;
    }
}