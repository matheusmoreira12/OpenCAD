using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures.UnitConversion
{
    public class UnitConversionManager
    {
        public static List<UnitConversion> Conversions { get; } = new List<UnitConversion> { };

        private static UnitConversion getForStrict(Unit unitA, Unit unitB) =>
            Conversions.FirstOrDefault(c => c.UnitA == unitA && c.UnitB == unitB);

        public static UnitConversion GetForStrict(Unit unitA, Unit unitB) => getForStrict(unitA, unitB);

        public static UnitConversion GetFor(Unit unitA, Unit unitB) => getForStrict(unitA, unitB);
    }
}
