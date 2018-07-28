using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public static class Utils
    {
        internal static double GetMetricPrefixValue(IMetricPrefix prefix) => prefix == null ? 1 : prefix.Multiplier;

        internal static double GetAbsoluteAmount<M>(Measurement<M> measurement) where M : IPhysicalQuantity, new() =>
            GetMetricPrefixValue(measurement.PrefixedUnit.Prefix) * measurement.PrefixedUnit.Unit.Quantity.StandardAmount
                * measurement.Amount;

        internal static double ConvertAmount<M>(Measurement<M> measurement, PrefixedUnit<M> outPrefixedUnit)
             where M : IPhysicalQuantity, new() => GetAbsoluteAmount(measurement)
            / outPrefixedUnit.Unit.Quantity.StandardAmount / GetMetricPrefixValue(outPrefixedUnit.Prefix);

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

        public static IEnumerable<Unit<M>> GetSupportedUnits<M>() where M : IPhysicalQuantity, new()
        {
            return getAllSupportedMatchingFields<M, Unit<M>>(System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetField);
        }

        public static IEnumerable<IMetricPrefix> GetSupportedMetricPrefixes()
        {
            return getAllSupportedMatchingFields<MetricPrefixes, IMetricPrefix>(System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetField);
        }

        public static IEnumerable<PrefixedUnit<M>> GetSupportedPrefixedUnits<M>() where M : IPhysicalQuantity, new()
        {
            var supportedUnits = GetSupportedUnits<M>();
            var metricPrefixes = GetSupportedMetricPrefixes();

            foreach (var unit in supportedUnits)
            {
                yield return new PrefixedUnit<M>(unit, null);

                foreach (var prefix in metricPrefixes)
                    yield return new PrefixedUnit<M>(unit, prefix);
            }
        }

        public static string ToString<M>(Measurement<M> measurement) where M : IPhysicalQuantity, new()
        {
            return $"{measurement.Amount}{measurement.PrefixedUnit.ToString()}";
        }
    }
}