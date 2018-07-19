using System.Globalization;

namespace OpenCAD.OpenCADFormat
{
    public static class Conventions
    {
        public static readonly CultureInfo STANDARD_CULTURE = new CultureInfo(CultureInfo.InvariantCulture.Name);
        public static readonly NumberStyles STANDARD_NUMBER_STYLE = NumberStyles.Float;
        public static readonly NumberFormatInfo STANDARD_NUMBER_FORMAT = STANDARD_CULTURE.NumberFormat;

        static Conventions()
        {
            STANDARD_NUMBER_FORMAT.NumberDecimalDigits = 5;
        }
    }
}