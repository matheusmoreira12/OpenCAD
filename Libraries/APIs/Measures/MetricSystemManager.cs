using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    static class MetricSystemManager
    {
        private static HashSet<MetricSystem> allMetricSystems { get; } = new HashSet<MetricSystem>();

        public static MetricSystem[] GetAll() => allMetricSystems.ToArray();

        internal static void AddMetricSystem(MetricSystem metricSystem)
            => allMetricSystems.Add(metricSystem);

        internal static void RemoveMetricSystem(MetricSystem metricSystem)
            => allMetricSystems.Add(metricSystem);

        public static IEnumerable<Unit> GetAllUnits()
        {
            foreach (var metricSystem in GetAll())
            {
                foreach (var unit in metricSystem.Units)
                    yield return unit;
            }
        }

        public static IEnumerable<Quantity> GetAllQuantities()
        {
            foreach (var metricSystem in GetAll())
            {
                foreach (var quantity in metricSystem.Quantities)
                    yield return quantity;
            }
        }

        public static IEnumerable<MetricPrefix> GetAllMetricPrefixes()
        {
            foreach (var metricSystem in GetAll())
            {
                foreach (var prefix in metricSystem.Prefixes)
                    yield return prefix;
            }
        }
    }
}
