using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures.UnitConversion
{
    public class UnitConversionManager
    {
        public static List<UnitConversion> Conversions { get; } = new List<UnitConversion> { };

        public static Dictionary<Unit, Scalar> ScaleZeroes { get; } = new Dictionary<Unit, Scalar> { };

        private static UnitConversion getForStrict(Unit sourceUnit, Unit targetUnit) =>
            Conversions.FirstOrDefault(c => c.SourceUnit == sourceUnit && c.TargetUnit == targetUnit);

        public static UnitConversion GetDirectConversion(Unit sourceUnit, Unit targetUnit)
            => getForStrict(sourceUnit, targetUnit) ?? getForStrict(targetUnit, sourceUnit)?.Invert();

        public static IEnumerable<UnitConversion> GetForSourceUnit(Unit sourceUnit)
        {
            var directConversions = Conversions.FindAll(c => c.SourceUnit == sourceUnit);
            var inverseConversions = Conversions.FindAll(c => c.TargetUnit == sourceUnit).Select(c => c.Invert());
            return directConversions.Concat(inverseConversions);
        }

        public static IEnumerable<UnitConversion> GetForTargetUnit(Unit targetUnit)
        {
            var directConversions = Conversions.FindAll(c => c.TargetUnit == targetUnit);
            var inverseConversions = Conversions.FindAll(c => c.SourceUnit == targetUnit).Select(c => c.Invert());
            return directConversions.Concat(inverseConversions);
        }

        private static IEnumerable<UnitConversion> getCascade(Unit sourceUnit, Unit targetUnit, List<UnitConversion> topRecursion)
        {
            var recursion = new List<UnitConversion>(topRecursion);
            var sourceUnitConversions = GetForSourceUnit(sourceUnit);
            foreach (var sourceUnitConversion in sourceUnitConversions)
            {
                if (!recursion.Contains(sourceUnitConversion))
                {
                    recursion.Add(sourceUnitConversion);
                    if (sourceUnitConversion.TargetUnit == targetUnit)
                        yield return sourceUnitConversion;
                    else
                    {
                        var conversions = getCascade(sourceUnitConversion.TargetUnit, targetUnit, recursion);
                        foreach (var conversion in conversions)
                            yield return conversion;
                    }
                }
            }
        }

        public static UnitConversion Get(Unit sourceUnit, Unit targetUnit)
        {
            var directConversion = GetDirectConversion(sourceUnit, targetUnit);
            if (directConversion is null)
            {
                var cascadedConversions = getCascade(sourceUnit, targetUnit, new List<UnitConversion> { }).ToArray();
                var aggregateConversionFactor = cascadedConversions.Select(c => c.Factor)
                    .Aggregate(1.0, (a, b) => a * b);
                if (cascadedConversions.Length > 0 && 
                    cascadedConversions.Last().TargetUnit == targetUnit)
                    return new UnitConversion(sourceUnit, targetUnit, aggregateConversionFactor);
                else
                    return null;
            }
            else
                return directConversion;
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
