using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Colors
{
    /// <summary>
    /// Represents a color value.
    /// </summary>
    //[DataStrings.AnyFunction("ColorRGB", "ColorRGBA", "ColorHSL",  "ColorHSLA", "ColorHSV", "ColorHSVA", "ColorCMYK", "ColorCMYKA")]
    public abstract class Color
    {
        public static Color Parse(string text)
        {
            return null;
        }

        public abstract override string ToString();
    }

    /// <summary>
    /// Represents a RGB color value.
    /// </summary>
    //[DataStrings.Function("ColorRGB")]
    public class ColorRGB : Color
    {
        //[DataStrings.IntegerLiteral()]
        public byte R { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte G { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte B { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte A { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorRGB() : this(0, 0, 0) { }
        public ColorRGB(byte r, byte g, byte b, byte a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }

    /// <summary>
    /// Represents an HSL color value.
    /// </summary>
    //[DataStrings.Function("ColorHSL")]
    public class ColorHSL : Color
    {
        //[DataStrings.FloatLiteral()]
        public double H { get; private set; }
        //[DataStrings.FloatLiteral()]
        public double S { get; private set; }
        //[DataStrings.FloatLiteral()]
        public double L { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte A { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorHSL() : this(0, 0, 0) { }
        public ColorHSL(double h, double s, double l, byte a = 255)
        {
            H = h;
            S = s;
            L = l;
            A = a;
        }
    }

    /// <summary>
    /// Represents an HSV color value.
    /// </summary>
    //[DataStrings.Function("ColorHSV")]
    public class ColorHSV : Color
    {
        //[DataStrings.FloatLiteral()]
        public double H { get; private set; }
        //[DataStrings.FloatLiteral()]
        public double S { get; private set; }
        //[DataStrings.FloatLiteral()]
        public double V { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte A { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorHSV() : this(0, 0, 0) { }
        public ColorHSV(double h, double s, double v, byte a = 255)
        {
            H = h;
            S = s;
            V = v;
            A = a;
        }
    }

    /// <summary>
    /// Represents a CMYK color value;
    /// </summary>
    //[DataStrings.Function("ColorCMYK")]
    public class ColorCMYK : Color
    {
        //[DataStrings.FloatLiteral()]
        public double C { get; private set; }
        //[DataStrings.FloatLiteral()]
        public double M { get; private set; }
        //[DataStrings.FloatLiteral()]
        public double Y { get; private set; }
        //[DataStrings.FloatLiteral()]
        public double K { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte A { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorCMYK() : this(0, 0, 0, 1) { }
        public ColorCMYK(double c, double m, double y, double k, byte a = 255)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
            A = a;
        }
    }
}
