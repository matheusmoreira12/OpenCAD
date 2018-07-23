using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{

    public static class Utils
    {
        internal static double GetMetricPrefixValue(IMetricPrefix prefix)
        {
            return prefix == null ? 1 : prefix.Multiplier;
        }

        internal static double GetAbsoluteAmount<M>(IMeasurement<M> measurement) where M : IPhysicalQuantity, new()
        {
            return GetMetricPrefixValue(measurement.PrefixedUnit.Prefix) * measurement.PrefixedUnit.Unit.Quantity.StandardAmount
                * measurement.Amount;
        }

        internal static double ConvertAmount<M>(IMeasurement<M> measurement, IPrefixedUnit<M> outPrefixedUnit)
             where M : IPhysicalQuantity, new()
        {
            return GetAbsoluteAmount(measurement) / outPrefixedUnit.Unit.Quantity.StandardAmount /
                GetMetricPrefixValue(outPrefixedUnit.Prefix);
        }

        private static IEnumerable<U> getAllSupportedMatchingFields<T, U>(System.Reflection.BindingFlags? bindingAttr = null)
        {
            var fields = typeof(T).GetFields(bindingAttr ?? 0);

            foreach (var field in fields)
            {
                object value = field.GetValue(null);

                if (field.FieldType.IsEquivalentTo(typeof (U)))
                    yield return (U)value;
            }
        }

        public static IEnumerable<IUnit<M>> GetSupportedUnits<M>() where M : IPhysicalQuantity, new()
        {
            return getAllSupportedMatchingFields<M, IUnit<M>>(System.Reflection.BindingFlags.Static | 
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetField);
        }

        public static IEnumerable<IMetricPrefix> GetSupportedMetricPrefixes()
        {
            return getAllSupportedMatchingFields<MetricPrefixes, IMetricPrefix>(System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.GetField);
        }

        public static IEnumerable<IPrefixedUnit<M>> GetSupportedPrefixedUnits<M>() where M : IPhysicalQuantity, new()
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

        public static string ToString<M>(IMeasurement<M> measurement) where M : IPhysicalQuantity, new()
        {
            return $"{measurement.Amount}{measurement.PrefixedUnit.ToString()}";
        }
    }
}