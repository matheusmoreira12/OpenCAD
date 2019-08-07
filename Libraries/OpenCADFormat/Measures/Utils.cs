using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    static class Utils
    {
        public static double GetMetricPrefixValue(MetricPrefix prefix) => prefix == null ? 1 : prefix.Multiplier;

        public static double GetAbsoluteAmount(Scalar measurement)
            => measurement.Unit.StandardAmount * measurement.Amount;

        public static double ConvertAmount(Scalar measurement, Unit outUnit) => GetAbsoluteAmount(measurement)
            / outUnit.StandardAmount;

        public static IEnumerable<(Unit, MetricPrefix)> GetSupportedUnitsAndPrefixes()
        {
            foreach (var unit in Units.SupportedUnits)
            {
                yield return (unit, null);

                if (unit.IsMetric)
                    foreach (var prefix in MetricPrefixes.SupportedPrefixes)
                        yield return (unit, prefix);
            }
        }

        public static (Unit, MetricPrefix) ParseUnitAndPrefix(string value)
        {
            IEnumerable<(Unit, MetricPrefix)> supportedUnitsAndPrefixes = GetSupportedUnitsAndPrefixes();

            foreach (var unitAndPrefix in supportedUnitsAndPrefixes)
            {
                Unit unit; MetricPrefix prefix;

                (unit, prefix) = unitAndPrefix;

                if ($"{prefix?.Symbol}{unit.Symbol}" == value)
                    return unitAndPrefix;
            }

            throw new KeyNotFoundException("Unable to parse unit and prefix. The provided unit/prefix string" +
                " has no matches.");
        }

        internal static Quantity FindEquivalentQuantity(Unit unit)
        {
            ///TODO: implement equivalency between quantities and selection logic

            return null;
        }
    }
}