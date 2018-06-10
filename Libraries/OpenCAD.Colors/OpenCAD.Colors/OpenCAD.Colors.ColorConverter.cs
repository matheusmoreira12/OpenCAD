using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Colors
{
    static class Utils
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

    public static class ColorConverter
    {
        private static (byte r, byte g, byte b) convertHSLtoRGB(double h, double s, double l)
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

        private static (double h, double s, double l) convertRGBtoHSL(byte r, byte g, byte b)
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

            return (hue, saturation, lightness);
        }

        private static (byte r, byte g, byte b) convertHSVtoRGB(double h, double s, double l)
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

        private static (double h, double s, double v) convertRGBtoHSV(byte r, byte g, byte b)
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

            return ((double)hue, (double)saturation, (double)value);
        }

        private static (byte r, byte g, byte b) convertCMYKtoRGB(double c, double m, double y, double k)
        {
            double cyan = c, magenta = m, yellow = y, black = k;

            // Calculate RGB color values
            double red = (1 - cyan) * (1 - black), green = (1 - magenta) * (1 - black), blue = (1 - yellow) * (1 - black);

            return ((byte)(red * 255.0), (byte)(green * 255.0), (byte)(blue * 255.0));
        }

        private static (double c, double m, double y, double k) convertRGBtoCMYK(byte r, byte g, byte b)
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

            return ((double)cyan, (double)magenta, (double)yellow, (double)black);
        }

        public static ColorRGB ToColorRGB(this ColorHSL source)
        {
            byte r, g, b;
            (r, g, b) = convertHSLtoRGB(source.H, source.S, source.L);

            return new ColorRGB(r, g, b, source.A);
        }

        public static ColorRGB ToColorRGB(this ColorHSV source)
        {
            byte r, g, b;
            (r, g, b) = convertHSVtoRGB(source.H, source.S, source.V);

            return new ColorRGB(r, g, b, source.A);
        }

        public static ColorRGB ToColorRGB(this ColorCMYK source)
        {
            byte r, g, b;
            (r, g, b) = convertCMYKtoRGB(source.C, source.M, source.Y, source.K);

            return new ColorRGB(r, g, b, source.A);
        }

        public static ColorHSL ToColorHSL(this ColorRGB source)
        {
            double h, s, l;
            (h, s, l) = convertRGBtoHSL(source.R, source.G, source.B);

            return new ColorHSL(h, s, l, source.A);
        }

        public static ColorHSL ToColorHSV(this ColorRGB source)
        {
            double h, s, v;
            (h, s, v) = convertRGBtoHSV(source.R, source.G, source.B);

            return new ColorHSL(h, s, v, source.A);
        }

        #region System Color Conversion Extension Methods
        /// <summary>
        /// Converts this RGB color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The RGB color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorRGB source)
        {
            return System.Windows.Media.Color.FromArgb(source.A, source.R, source.G, source.B);
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
        /// Converts this HSL color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The HSL color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorHSL source)
        {
            var rgbValues = convertHSLtoRGB(source.H, source.S, source.L);

            return System.Windows.Media.Color.FromArgb(source.A, rgbValues.Item1, rgbValues.Item2, rgbValues.Item3);
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
        /// Converts this HSV color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The HSV color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorHSV source)
        {
            var values = convertHSVtoRGB(source.H, source.S, source.V);

            return System.Windows.Media.Color.FromArgb(source.A, values.r, values.g, values.b);
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
        /// Converts this CMYK color to a color compatible with the operating system.
        /// </summary>
        /// <param name="source">The CMYK color.</param>
        /// <returns></returns>
        public static System.Windows.Media.Color ToSystemColor(this ColorCMYK source)
        {
            var values = convertCMYKtoRGB(source.C, source.M, source.Y, source.K);

            return System.Windows.Media.Color.FromArgb(source.A, values.r, values.g, values.b);
        }

        /// <summary>
        /// Converts an operating system color to an OpenCAD CMYK color.
        /// </summary>
        /// <param name="source">The system color to be converted.</param>
        /// <returns></returns>
        public static ColorCMYK ToOpenCADColorCMYK(this System.Windows.Media.Color source)
        {
            var values = convertRGBtoCMYK(source.R, source.G, source.B);

            return new ColorCMYK(values.c, values.m, values.y, values.k);
        }
        #endregion
    }
}
