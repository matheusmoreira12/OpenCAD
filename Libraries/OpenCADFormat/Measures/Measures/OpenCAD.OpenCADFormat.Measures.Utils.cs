using System.Collections.Generic;
using System.Linq;

using OpenCAD.OpenCADFormat.DataTypes;

namespace OpenCAD.OpenCADFormat.Measures
{

    public static class Utils
    {
        internal static BigFloat GetMetricPrefixValue(IMetricPrefix prefix)
        {
            return prefix?.Multiplier;
        }

        internal static BigFloat GetAbsoluteAmount<M>(IMeasurement<M> measurement) where M : IPhysicalQuantity, new()
        {
            return GetMetricPrefixValue(measurement.PrefixedUnit.Prefix) * measurement.PrefixedUnit.Unit.Quantity.StandardAmount
                * measurement.Amount;
        }

        internal static BigFloat ConvertAmount<M>(IMeasurement<M> measurement, IPrefixedUnit<M> outPrefixedUnit)
             where M : IPhysicalQuantity, new()
        {
            return GetAbsoluteAmount(measurement) / outPrefixedUnit.Unit.Quantity.StandardAmount /
                GetMetricPrefixValue(outPrefixedUnit.Prefix);
        }

        private static IEnumerable<U> getAllSuppotedMatchingFields<T, U>(System.Reflection.BindingFlags bindingAttr)
        {
            var staticFields = typeof(T).GetFields(bindingAttr);

            foreach (var field in staticFields)
            {
                object value = field.GetValue(null);

                if (field is U)
                    yield return (U)value;
            }
        }

        public static IEnumerable<IUnit<M>> GetSupportedUnits<M>() where M : IPhysicalQuantity, new()
        {
            return getAllSuppotedMatchingFields<M, IUnit<M>>(System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.GetField);
        }

        public static IEnumerable<IMetricPrefix> GetSupportedMetricPrefixes()
        {
            return getAllSuppotedMatchingFields<MetricPrefix, IMetricPrefix>(System.Reflection.BindingFlags.Static |
                System.Reflection.BindingFlags.GetField);
        }

        private static IEnumerable<string> combineAllPrefixesAndUnits<M>() where M : IPhysicalQuantity, new()
        {
            IUnit<M>[] supportedUnits = GetSupportedUnits<M>().ToArray();
            IMetricPrefix[] supportedPrefixes = GetSupportedMetricPrefixes().ToArray();

            foreach (var unit in supportedUnits)
            {
                yield return $"{unit.Symbol}";

                foreach (var prefix in supportedPrefixes)
                    yield return $"{prefix.Symbol}{unit.Symbol}";
            }
        }

        public static string ToString<M>(IMeasurement<M> measurement) where M : IPhysicalQuantity, new()
        {
            return $"{measurement.Amount}{measurement.PrefixedUnit.ToString()}";
        }
    }
}