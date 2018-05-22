using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Colors
{
    static class ColorConverter
    {
        private static class Utils
        {
            public static T GetMinimum<T>(params T[] numbers) where T : IComparable
            {
                return numbers.Min();
            }

            public static T GetMaximum<T>(params T[] numbers) where T : IComparable
            {
                return numbers.Max();
            }
        }

        private static (byte r, byte g, byte b) convertHSLtoRGB(float h, float s, float l)
        {
            double hue = h, saturation = s, lightness = l;

            hue %= 360;

            double c = (1 - Math.Abs(2 * lightness - 1)) * saturation,
                x = c * (1 - Math.Abs((hue / 60) % 2 - 1)),
                m = lightness - c / 2;

            // Matrix of all the values, organized in 1/6th of rotation divisions
            var matrix = new[] { (c, x, 0.0), (x, c, 0), (0, c, x), (0, x, c), (x, 0, c), (c, 0, x) };

            // Select the division according to the angle
            int division = (int)(hue / 60);

            // Assign the RGB values
            double red, green, blue;
            (red, green, blue) = matrix[division];

            // Dessaturate color
            red += m; green += m; blue += m;

            return ((byte)(red * 255.0), (byte)(green * 255.0), (byte)(blue * 255.0));
        }

        private static (float h, float s, float l) convertRGBtoHSL(byte r, byte g, byte b)
        {
            double red = r / 255.0, green = g / 255.0, blue = b / 255.0;

            // Calculate saturation and lightness 
            double cmax = Utils.GetMaximum(red, green, blue),
                cmin = Utils.GetMinimum(red, green, blue),
                delta = cmax - cmin,
                lightness = (cmax + cmin) / 2,
                saturation = delta / (1 - Math.Abs(2 * lightness - 1));

            // Calculate hue
            double hue = 0;
            if (delta > 0)
            {
                if (cmax == red)
                    hue = 60 * (((green - blue) / delta) % 6);
                else if (cmax == green)
                    hue = 60 * ((blue - red) / delta + 2);
                else
                    hue = 60 * ((red - green) / delta + 4);
            }

            return ((float)hue, (float)saturation, (float)lightness);
        }

        private static (byte r, byte g, byte b) convertHSVtoRGB(float h, float s, float l)
        {
            double hue = h, saturation = s, value = l;

            hue %= 360;

            double c = value * saturation,
                x = c * (1 - Math.Abs((hue / 60.0f) % 2 - 1)),
                m = value - c;

            // Matrix of all the values, organized in 1/6th of rotation divisions
            var matrix = new[] { ( c, x, 0.0 ), ( x, c, 0 ), ( 0, c, x ), ( 0, x, c ), ( x, 0, c ), ( c, 0, x ) };

            // Select the division according to the angle
            int division = (int)(hue / 60);

            // Assign the RGB values
            double red, green, blue;
            (red, green, blue) = matrix[division];

            //Dessaturate color
            red += m; green += m; blue += m;

            return ((byte)(red * 255.0), (byte)(green * 255.0), (byte)(blue * 255.0));
        }

        private static (float h, float s, float v) convertRGBtoHSV(byte r, byte g, byte b)
        {
            double red = r / 255.0, green = g / 255.0, blue = b / 255.0;

            // Calculate saturation and lightness 
            double cmax = Utils.GetMaximum(red, green, blue),
                cmin = Utils.GetMinimum(red, green, blue),
                delta = cmax - cmin,
                value = cmax,
                saturation = cmax == 0 ? 0 : (delta / cmax);

            // Calculate hue
            double hue = 0;
            if (delta > 0)
            {
                if (cmax == red)
                    hue = 60 * ((green - blue) / delta % 6);
                else if (cmax == green)
                    hue = 60 * ((blue - red) / delta + 2);
                else
                    hue = 60 * ((red - green) / delta + 4);
            }

            return ((float)hue, (float)saturation, (float)value);
        }

        private static (byte r, byte g, byte b) convertCMYKToRGB(float c, float m, float y, float k)
        {
            double cyan = c, magenta = m, yellow = y, black = k;

            // Calculate RGB color values
            double red = (1 - cyan) * (1 - black), green = (1 - magenta) * (1 - black), blue = (1 - yellow) * (1 - black);

            return ((byte)(red * 255.0), (byte)(green * 255.0), (byte)(blue * 255.0));
        }

        private static (float c, float m, float y, float k) ConvertRGBToCMYK(byte r, byte g, byte b)
        {
            // Convert RGB values to double
            double red = r / 255.0, green = g / 255.0, blue = b / 255.0;

            // Get initial C, M, and Y values
            double cyan = 1 - red, magenta = 1 - green, yellow = 1 - blue,
                black = Utils.GetMinimum(cyan, magenta, yellow);

            if (black == 1)
                cyan = magenta = yellow = 0;
            else
            {
                // Remove black from C, M, and Y values
                cyan = (cyan - black) / (1 - black);
                magenta = (magenta - black) / (1 - black);
                yellow = (yellow - black) / (1 - black);
            }

            return ((float)cyan, (float)magenta, (float)yellow, (float)black);
        }

        /// <summary>
        /// Converts this RGBA color to RGB plus alpha channel.
        /// </summary>
        /// <param name="source">The RGBA color.</param>
        /// <param name="a">The alpha channel value.</param>
        /// <returns></returns>
        public static ColorRGB ToColorRGB(this ColorRGBA source, out float a)
        {
            a = source.A;
            return new ColorRGB(source.R, source.G, source.B);
        }

        /// <summary>
        /// Converts this RGB color plus alpha channel to an RGBA color.
        /// </summary>
        /// <param name="source">The RGB color.</param>
        /// <param name="a">The alpha channel value.</param>
        /// <returns></returns>
        public static ColorRGBA ToColorRGBA(this ColorRGB source, float a = 1)
        {
            return new ColorRGBA(source.R, source.G, source.B, a);
        }

        /// <summary>
        /// Converts this HSLA color to HSL plus alpha channel.
        /// </summary>
        /// <param name="source">The HSLA color.</param>
        /// <param name="a">The alpha channel value.</param>
        /// <returns></returns>
        public static ColorHSL ToColorHSLA(this ColorHSLA source, out float a)
        {
            a = source.A;
            return new ColorHSL(source.H, source.S, source.L);
        }

        /// <summary>
        /// Converts this HSL color plus alpha channel to an HSLA color.
        /// </summary>
        /// <param name="source">The HSL color.</param>
        /// <param name="a">The alpha channel value.</param>
        /// <returns></returns>
        public static ColorHSLA ToColorHSLA(this ColorHSL source, float a = 1)
        {
            return new ColorHSLA(source.H, source.S, source.L, a);
        }

        /// <summary>
        /// Converts this HSVA color to HSV plus alpha channel.
        /// </summary>
        /// <param name="source">The HSVA color.</param>
        /// <param name="a">The alpha channel value.</param>
        /// <returns></returns>
        public static ColorHSV ToColorHSV(this ColorHSVA source, out float a)
        {
            a = source.A;
            return new ColorHSV(source.H, source.S, source.V);
        }

        /// <summary>
        /// Converts this HSV color plus alpha channel to an HSVA color.
        /// </summary>
        /// <param name="source">The HSL color.</param>
        /// <param name="a">The alpha channel value.</param>
        /// <returns></returns>
        public static ColorHSVA ToColorHSVA(this ColorHSV source, float a = 1)
        {
            return new ColorHSVA(source.H, source.S, source.V, a);
        }

        /// <summary>
        /// Converts this CMYKA color to CMYK plus alpha channel.
        /// </summary>
        /// <param name="source">The CMYKA color.</param>
        /// <param name="a">The alpha channel value.</param>
        /// <returns></returns>
        public static ColorCMYK ToColorCMYK(this ColorCMYKA source, out float a)
        {
            a = source.A;
            return new ColorCMYK(source.C, source.M, source.Y, source.K);
        }

        /// <summary>
        /// Converts this CMY color plus alpha channel to an CMYKA color.
        /// </summary>
        /// <param name="source">The CMYK color.</param>
        /// <param name="a">The alpha channel value.</param>
        /// <returns></returns>
        public static ColorCMYKA ToColorCMYKA(this ColorCMYK source, float a)
        {
            return new ColorCMYKA(source.C, source.M, source.Y, source.K, a);
        }

        #region System Color Conversion Extension Methods
        /// <summary>
        /// Converts this RGB color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The RGB color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorRGB source)
        {
            return System.Windows.Media.Color.FromRgb(source.R, source.G, source.B);
        }

        /// <summary>
        /// Converts an operating system color to an OpenCAD RGB color.
        /// </summary>
        /// <param name="source">The system color to be converted.</param>
        /// <returns></returns>
        public static ColorRGB ToOpenCADColorRGB(this System.Windows.Media.Color source)
        {
            return new ColorRGB(source.R, source.G, source.B);
        }

        /// <summary>
        /// Converts this RGBA color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The RGBA color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorRGBA source)
        {
            return System.Windows.Media.Color.FromArgb((byte)(source.A * 255), source.R, source.G, source.B);
        }

        /// <summary>
        /// Converts an operating system color to an OpenCAD RGBA color.
        /// </summary>
        /// <param name="source">The system color to be converted.</param>
        /// <returns></returns>
        public static ColorRGBA ToOpenCADColorRGBA(this System.Windows.Media.Color source)
        {
            return new ColorRGBA(source.R, source.G, source.B, source.A);
        }

        /// <summary>
        /// Converts this HSL color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The HSL color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorHSL source)
        {
            var rgbValues = convertHSLtoRGB(source.H, source.S, source.L);

            return System.Windows.Media.Color.FromRgb(rgbValues.Item1, rgbValues.Item2, rgbValues.Item3);
        }

        /// <summary>
        /// Converts an operating system color to an OpenCAD HSL color.
        /// </summary>
        /// <param name="source">The system color to be converted.</param>
        /// <returns></returns>
        public static ColorHSL ToOpenCADColorHSL(this System.Windows.Media.Color source)
        {
            var values = convertRGBtoHSL(source.R, source.G, source.B);

            return new ColorHSL(values.h, values.s, values.l);
        }

        /// <summary>
        /// Converts this HSLA color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The HSLA color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorHSLA source)
        {
            var values = convertHSLtoRGB(source.H, source.S, source.L);

            return System.Windows.Media.Color.FromArgb((byte)(source.A * 255), values.r, values.g, values.b);
        }

        /// <summary>
        /// Converts an operating system color to an OpenCAD HSLA color.
        /// </summary>
        /// <param name="source">The system color to be converted.</param>
        /// <returns></returns>
        public static ColorHSLA ToOpenCADColorHSLA(this System.Windows.Media.Color source)
        {
            var hslValues = convertRGBtoHSL(source.R, source.G, source.B);

            return new ColorHSLA(hslValues.h, hslValues.s, hslValues.l, source.A);
        }

        /// <summary>
        /// Converts this HSV color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The HSV color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorHSV source)
        {
            var values = convertHSVtoRGB(source.H, source.S, source.V);

            return System.Windows.Media.Color.FromRgb(values.r, values.g, values.b);
        }

        /// <summary>
        /// Converts an operating system color to an OpenCAD HSV color.
        /// </summary>
        /// <param name="source">The system color to be converted.</param>
        /// <returns></returns>
        public static ColorHSV ToOpenCADColorHSV(this System.Windows.Media.Color source)
        {
            var hsvValues = convertRGBtoHSV(source.R, source.G, source.B);

            return new ColorHSV(hsvValues.h, hsvValues.s, hsvValues.v);
        }

        /// <summary>
        /// Converts this HSVA color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The HSVA color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorHSVA source)
        {
            var values = convertHSVtoRGB(source.H, source.S, source.V);

            return System.Windows.Media.Color.FromArgb((byte)(source.A * 255), values.r, values.g, values.b);
        }

        /// <summary>
        /// Converts an operating system color to an OpenCAD HSVA color.
        /// </summary>
        /// <param name="source">The system color to be converted.</param>
        /// <returns></returns>
        public static ColorHSVA ToOpenCADColorHSVA(this System.Windows.Media.Color source)
        {
            var hsvValues = convertRGBtoHSV(source.R, source.G, source.B);

            return new ColorHSVA(hsvValues.h, hsvValues.s, hsvValues.v, source.A);
        }

        /// <summary>
        /// Converts this CMYK color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The CMYK color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorCMYK source)
        {
            var values = convertCMYKToRGB(source.C, source.M, source.Y, source.K);

            return System.Windows.Media.Color.FromRgb(values.r, values.g, values.b);
        }

        /// <summary>
        /// Converts an operating system color to an OpenCAD CMYK color.
        /// </summary>
        /// <param name="source">The system color to be converted.</param>
        /// <returns></returns>
        public static ColorCMYK ToOpenCADColorCMYK(this System.Windows.Media.Color source)
        {
            var values = ConvertRGBToCMYK(source.R, source.G, source.B);

            return new ColorCMYK(values.c, values.m, values.y, values.k);
        }

        /// <summary>
        /// Converts this CMYKA color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The CMYKA color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorCMYKA source)
        {
            var values = convertCMYKToRGB(source.C, source.M, source.Y, source.K);

            return System.Windows.Media.Color.FromArgb((byte)(source.A * 255), values.r, values.g, values.b);
        }

        /// <summary>
        /// Converts an operating system color to an OpenCAD CMYKA color.
        /// </summary>
        /// <param name="source">The system color to be converted.</param>
        /// <returns></returns>
        public static ColorCMYKA ToOpenCADColorCMYKA(this System.Windows.Media.Color source)
        {
            var values = ConvertRGBToCMYK(source.R, source.G, source.B);

            return new ColorCMYKA(values.c, values.m, values.y, values.k, source.A);
        }
        #endregion
    }
}
