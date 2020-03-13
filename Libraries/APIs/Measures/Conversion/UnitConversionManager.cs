using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures.UnitConversion
{
    public class UnitConversionManager
    {
        public static List<UnitConversion> Conversions { get; } = new List<UnitConversion> { };

        public static Dictionary<Unit, Scalar> ScaleZeroes { get; } = new Dictionary<Unit, Scalar> { };

        private static UnitConversion getForStrict(Unit unitA, Unit unitB) =>
            Conversions.FirstOrDefault(c => c.UnitA == unitA && c.UnitB == unitB);

        /// <summary>
        /// Gets an strict conversion from a unit to another unit;
        /// </summary>
        /// <param name="unitA">The unit to convert from.</param>
        /// <param name="unitB">The unit to convert to.</param>
        /// <returns></returns>
        public static UnitConversion GetForStrict(Unit unitA, Unit unitB) => getForStrict(unitA, unitB);

        public static UnitConversion GetFor(Unit unitA, Unit unitB) => getForStrict(unitA, unitB);

        public static UnitConversion GetForRecursive(Unit unitA, Unit unitB)
        {
            foreach (var conversion in Conversions)
            {
                if (conversion.UnitA == unitA)
                {
                    if (conversion.UnitB == unitB)
                        return conversion;
                    else
                        return GetForRecursive(conversion.UnitB, unitB);
                }
                else if (conversion.UnitB == unitA)
                {
                    if (conversion.UnitA == unitB)
                        return conversion.Invert();
                    else
                        return GetForRecursive(conversion.UnitA, unitB);
                }
            }
            return null;
        }

        public static Scalar? GetScaleZero(Unit unit)
        {
            Scalar zero = Scalar.Zero;
            if (ScaleZeroes.TryGetValue(unit, out zero))
                return zero;
            else
                return null;
        }
    }
}
