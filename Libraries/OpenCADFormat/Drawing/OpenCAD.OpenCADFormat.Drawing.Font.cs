using OpenCAD.OpenCADFormat.Measures;
using System;
using System.Xml.Serialization;
using Quantities = OpenCAD.OpenCADFormat.Measures.Quantities;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public enum TextDecoration { None = 0, Underline = 1, Overline = 2, StrikeThrough = 4 }

    [Serializable]
    public enum TextAlignment { Leading, Middle, Trailing }

    [Serializable]
    public enum TextBaselineShift { Baseline, Subscript, Superscript }

    [Serializable]
    public enum FontWeight { Lighter, Light, Normal, Bold, Bolder }

    [Serializable]
    public enum FontStyle { Normal, Italic, Oblique, BackSlanted }

    [Serializable]
    public class Font
    {
        [XmlIgnore()]
        public const string DefaultFamily = "Arial, Verdana";
        [XmlIgnore()]
        public static readonly Measurement DefaultHeight = new Measurement(20, Units.Length.Mil);

        [XmlIgnore()]
        public static Font Default = new Font(DefaultFamily, DefaultHeight);


        public Font(string family, Measurement height,
            FontWeight weight = FontWeight.Normal, FontStyle style = FontStyle.Normal)
        {
            Family = family ?? throw new ArgumentNullException("family");
            Height = height;
            Weight = weight;
            Style = style;
        }

        public Font() { }

        [XmlAttribute]
        public string Family { get; private set; } = DefaultFamily;
        [XmlAttribute]
        public FontWeight Weight { get; private set; } = FontWeight.Normal;
        [XmlAttribute]
        public FontStyle Style { get; private set; } = FontStyle.Normal;
        [XmlAttribute]
        public Measurement Height { get; private set; } = DefaultHeight;
    }
}