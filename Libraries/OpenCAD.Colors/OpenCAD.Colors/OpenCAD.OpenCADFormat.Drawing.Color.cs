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
    //[DataStrings.AnyFunction("ColorRGB", "ColorRGBA", "ColorHSL", "ColorHSLA", "ColorHSV", "ColorHSVA", "ColorCMYK", "ColorCMYKA")]
    abstract class Color
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
    class ColorRGB : Color
    {
        //[DataStrings.IntegerLiteral()]
        public byte R { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte G { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte B { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorRGB() : this(0, 0, 0) { }
        public ColorRGB(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    /// <summary>
    /// Represents a RGB color value with alpha channel.
    /// </summary>
    //[DataStrings.Function("ColorRGBA")]
    class ColorRGBA : Color
    {
        //[DataStrings.IntegerLiteral()]
        public byte R { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte G { get; private set; }
        //[DataStrings.IntegerLiteral()]
        public byte B { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float A { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorRGBA() : this(0, 0, 0, 1) { }
        public ColorRGBA(byte r, byte g, byte b, float a)
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
    class ColorHSL : Color
    {
        //[DataStrings.FloatLiteral()]
        public float H { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float S { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float L { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorHSL() : this(0, 0, 0) { }
        public ColorHSL(float h, float s, float l)
        {
            H = h;
            S = s;
            L = l;
        }
    }

    /// <summary>
    /// Represents an HSL color value with alpha channel.
    /// </summary>
    //[DataStrings.Function("ColorHSLA")]
    class ColorHSLA : Color
    {
        //[DataStrings.FloatLiteral()]
        public float H { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float S { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float L { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float A { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorHSLA() : this(0, 0, 0, 1) { }
        public ColorHSLA(float h, float s, float l, float a)
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
    class ColorHSV : Color
    {
        //[DataStrings.FloatLiteral()]
        public float H { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float S { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float V { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorHSV() : this(0, 0, 0) { }
        public ColorHSV(float h, float s, float v)
        {
            H = h;
            S = s;
            V = v;
        }
    }

    /// <summary>
    /// Represents an HSV color value with alpha channel.
    /// </summary>
    //[DataStrings.Function("ColorHSVA")]
    class ColorHSVA : Color
    {
        //[DataStrings.FloatLiteral()]
        public float H { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float S { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float V { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float A { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorHSVA() : this(0, 0, 0, 1) { }
        public ColorHSVA(float h, float s, float v, float a)
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
    class ColorCMYK : Color
    {
        //[DataStrings.FloatLiteral()]
        public float C { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float M { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float Y { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float K { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorCMYK() : this(0, 0, 0, 1) { }
        public ColorCMYK(float c, float m, float y, float k)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
        }
    }

    /// <summary>
    /// Represents a CMYK color value with alpha channel;
    /// </summary>
    //[DataStrings.Function("ColorCMYKA")]
    class ColorCMYKA : Color
    {
        //[DataStrings.FloatLiteral()]
        public float C { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float M { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float Y { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float K { get; private set; }
        //[DataStrings.FloatLiteral()]
        public float A { get; private set; }

        public override string ToString()
        {
            return null;
        }

        public ColorCMYKA() : this(0, 0, 0, 1, 1) { }
        public ColorCMYKA(float c, float m, float y, float k, float a)
        {
            C = c;
            M = m;
            Y = y;
            K = k;
            A = a;
        }
    }
}
