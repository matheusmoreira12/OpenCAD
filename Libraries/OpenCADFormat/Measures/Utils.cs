using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public static class Utils
    {
        public static double GetMetricPrefixValue(MetricPrefix prefix) => prefix == null ? 1 : prefix.Multiplier;

        public static double GetAbsoluteAmount(Scalar measurement)
            => measurement.Unit.StandardAmount * measurement.Amount;

        public static double ConvertAmount(Scalar measurement, Unit outUnit) => GetAbsoluteAmount(measurement)
            / outUnit.StandardAmount;

        public static IEnumerable<Unit> GetSupportedUnits()
        {
            foreach (var unit in Units.SupportedUnits)
            {
                yield return unit;

                foreach (var prefix in MetricPrefixes.SupportedPrefixes)
                    yield return new PrefixedUnit(unit, prefix);
            }
        }

        internal static Quantity FindEquivalentQuantity(Unit unit)
        {
            ///TODO: implement equivalency between quantities and selection logic

            return null;
        }
    }
}