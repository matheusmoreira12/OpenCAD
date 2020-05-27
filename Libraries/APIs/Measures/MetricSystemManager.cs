using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    internal static class MetricSystemManager
    {
        private static HashSet<MetricSystem> allMetricSystems { get; }
            = new HashSet<MetricSystem>();

        internal static MetricSystem[] GetAllMetricSystems()
            => allMetricSystems.ToArray();

        internal static void AddMetricSystem(MetricSystem metricSystem)
            => allMetricSystems.Add(metricSystem);

        internal static void RemoveMetricSystem(MetricSystem metricSystem)
            => allMetricSystems.Remove(metricSystem);

        internal static IEnumerable<Unit> GetAllUnits()
        {
            foreach (var metricSystem in GetAllMetricSystems())
            {
                foreach (var unit in metricSystem.Units)
                    yield return unit;
            }
        }

        internal static IEnumerable<Quantity> GetAllQuantities()
        {
            foreach (var metricSystem in GetAllMetricSystems())
            {
                foreach (var quantity in metricSystem.Quantities)
                    yield return quantity;
            }
        }

        internal static IEnumerable<MetricPrefix> GetAllMetricPrefixes()
        {
            foreach (var metricSystem in GetAllMetricSystems())
            {
                foreach (var prefix in metricSystem.Prefixes)
                    yield return prefix;
            }
        }
    }
}
