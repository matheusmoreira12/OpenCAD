using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public static class Utils
    {
        internal static double GetMetricPrefixValue(IMetricPrefix prefix) => prefix == null ? 1 : prefix.Multiplier;

        internal static double GetAbsoluteAmount<M>(Measurement<M> measurement) where M : IPhysicalQuantity, new()
            => measurement.Unit.StandardAmount * measurement.Amount;

        internal static double ConvertAmount<M>(Measurement<M> measurement, IUnit<M> outUnit)
             where M : IPhysicalQuantity, new() => GetAbsoluteAmount(measurement)
            / measurement.Unit.StandardAmount;

        private static IEnumerable<U> getAllSupportedMatchingFields<T, U>(System.Reflection.BindingFlags? bindingAttr = null)
        {
            var fields = typeof(T).GetFields(bindingAttr ?? 0);

            foreach (var field in fields)
            {
                object value = field.GetValue(null);

                if (field.FieldType.IsEquivalentTo(typeof(U)))
                    yield return (U)value;
            }
        }

        public static IEnumerable<IMetricPrefix> GetSupportedMetricPrefixes()
        {
            return getAllSupportedMatchingFields<MetricPrefixes, IMetricPrefix>(System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetField);
        }

        private static IEnumerable<IUnit<M>> getSupportedUnits<M>() where M : IPhysicalQuantity, new()
        {
            return getAllSupportedMatchingFields<M, IUnit<M>>(System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetField);
        }

        public static IEnumerable<IUnit<M>> GetSupportedUnits<M>() where M : IPhysicalQuantity, new()
        {
            var supportedUnits = getSupportedUnits<M>();
            var metricPrefixes = GetSupportedMetricPrefixes();

            foreach (var unit in supportedUnits)
            {
                yield return unit;

                if (unit.IsPrefixable)
                    foreach (var prefix in metricPrefixes)
                        yield return new PrefixedUnit<M>(unit, prefix);
            }
        }

        public static string ToString<M>(Measurement<M> measurement) where M : IPhysicalQuantity, new()
        {
            return $"{measurement.Amount}{measurement.Unit.ToString()}";
        }
    }
}