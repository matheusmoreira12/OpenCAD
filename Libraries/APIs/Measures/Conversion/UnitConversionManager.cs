using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures.Conversion
{
    internal static class UnitConversionManager
    {
        private static Dictionary<Unit, Scalar> allScaleZeroes { get; } = new Dictionary<Unit, Scalar> { };

        #region User Conversions
        private static HashSet<UnitConversion> userConversions { get; } = new HashSet<UnitConversion> { };

        public static void Add(UnitConversion conversion)
        {
            if (GetDirect(conversion.SourceUnit, conversion.TargetUnit) == null)
                userConversions.Add(conversion);
            else
                throw new InvalidOperationException("Cannot add unit conversion. The specified unit" +
                    " conversion already exists.");
        }

        private static UnitConversion getStrict(Unit sourceUnit, Unit targetUnit) =>
            userConversions.FirstOrDefault(c => c.SourceUnit == sourceUnit
                && c.TargetUnit == targetUnit);

        public static UnitConversion[] GetAll() => userConversions.ToArray();

        public static UnitConversion GetDirect(Unit sourceUnit, Unit targetUnit)
            => getStrict(sourceUnit, targetUnit) ?? getStrict(targetUnit, sourceUnit)?.Invert();

        public static IEnumerable<UnitConversion> GetDirectFrom(Unit sourceUnit) => userConversions
            .Where(c => c.SourceUnit == sourceUnit).Concat(userConversions
            .Where(c => c.TargetUnit == sourceUnit).Select(c => c.Invert()));

        public static IEnumerable<UnitConversion> GetDirectTo(Unit targetUnit) => userConversions
            .Where(c => c.TargetUnit == targetUnit).Concat(userConversions
            .Where(c => c.SourceUnit == targetUnit).Select(c => c.Invert()));
        #endregion

        #region Cached Conversions
        private static HashSet<UnitConversion> cachedConversions { get; } = new HashSet<UnitConversion> { };

        private static void addCached(UnitConversion conversion)
        {
            if (GetCachedDirect(conversion.SourceUnit, conversion.TargetUnit) == null)
                cachedConversions.Add(conversion);
            else
                throw new InvalidOperationException("Cannot add unit conversion to cache. The specified" +
                    " unit conversion is already cached.");
        }

        private static UnitConversion getCachedStrict(Unit sourceUnit, Unit targetUnit)
            => cachedConversions.FirstOrDefault(c => sourceUnit == targetUnit);

        public static UnitConversion GetCachedDirect(Unit sourceUnit, Unit targetUnit)
            => getCachedStrict(sourceUnit, targetUnit) ?? getCachedStrict(targetUnit, sourceUnit)?.Invert();

        public static void ClearCache() => cachedConversions.Clear();
        #endregion

        public static UnitConversion Get(Unit sourceUnit, Unit targetUnit)
        {
            var directConversion = GetDirect(sourceUnit, targetUnit);
            if (directConversion == null)
            {
                var cachedDirectConversion = GetCachedDirect(sourceUnit, targetUnit);
                if (cachedDirectConversion == null)
                {
                    var aggregateConversion = UnitConversionFinder.Find(sourceUnit, targetUnit);
                    if (aggregateConversion != null)
                        addCached(aggregateConversion);
                    return aggregateConversion;
                }
                else
                    return cachedDirectConversion;
            }
            else
                return directConversion;
        }

        public static void DefineScaleZero(Unit unit, Scalar zero)
        {
            if (zero.Unit == unit)
            {
                if (allScaleZeroes.ContainsKey(unit))
                    throw new InvalidOperationException("Cannot define scale zero. " +
                        "Scale zero has already been defined.");
                else
                    allScaleZeroes[unit] = zero;
            }
            else
                throw new InvalidOperationException("Cannot define scale zero." +
                    "Units must match.");
        }

        public static Scalar GetScaleZero(Unit unit)
        {
            Scalar zero;
            if (allScaleZeroes.TryGetValue(unit, out zero))
                return zero;
            else
                return Scalar.Zero;
        }
    }
}
