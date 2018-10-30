using System.Globalization;

namespace OpenCAD.OpenCADFormat
{
    public static class Conventions
    {
        public static readonly CultureInfo STANDARD_CULTURE = new CultureInfo(CultureInfo.InvariantCulture.Name);
        public static readonly NumberStyles STANDARD_NUMBER_STYLE = NumberStyles.Float;

        static Conventions()
        {
            STANDARD_CULTURE.NumberFormat.NumberDecimalDigits = 5;
        }
    }
}