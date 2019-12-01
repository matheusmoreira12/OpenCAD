using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.OpenCADFormat.Measures.Conversion
{
    class UnitConversionManager
    {
        public static List<UnitConversion> Conversions { get; } = new List<UnitConversion> { };

        private static UnitConversion getForStrict(Unit unitA, Unit unitB) =>
            Conversions.FirstOrDefault(c => c.UnitA == unitA && c.UnitB == unitB);

        public static UnitConversion GetForStrict(Unit unitA, Unit unitB) => getForStrict(unitA, unitB)
            ?? throw new KeyNotFoundException();

        public static UnitConversion GetFor(Unit unitA, Unit unitB) => getForStrict(unitA, unitB)
            ?? getForStrict(unitB, unitA)?.Invert() ?? throw new KeyNotFoundException();
    }
}
