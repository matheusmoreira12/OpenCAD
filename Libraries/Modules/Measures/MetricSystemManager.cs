using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    internal static class MetricSystemManager
    {
        #region Quantities
        private static HashSet<Quantity> allQuantities { get; } = new HashSet<Quantity>();

        internal static Quantity[] GetAllQuantities() => allQuantities.ToArray();

        internal static void AddQuantity(Quantity quantity) => allQuantities.Add(quantity);

        internal static void RemoveQuantity(Quantity quantity) => allQuantities.Remove(quantity);
        #endregion

        #region Metric Systems
        private static HashSet<MetricSystem> allMetricSystems { get; } = new HashSet<MetricSystem>();

        internal static MetricSystem[] GetAllMetricSystems() => allMetricSystems.ToArray();

        internal static void AddMetricSystem(MetricSystem metricSystem) => allMetricSystems.Add(metricSystem);

        internal static void RemoveMetricSystem(MetricSystem metricSystem) => allMetricSystems.Remove(metricSystem);
        #endregion

        internal static IEnumerable<Unit> GetAllUnits()
        {
            foreach (var metricSystem in GetAllMetricSystems())
            {
                foreach (var unit in metricSystem.Units)
                    yield return unit;
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
