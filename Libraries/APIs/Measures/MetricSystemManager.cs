using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    static class MetricSystemManager
    {
        static MetricSystemManager()
        {
            metricSystems = new HashSet<MetricSystem>();
        }

        public static MetricSystem[] MetricSystems => metricSystems.ToArray();
        private static HashSet<MetricSystem> metricSystems { get; }

        internal static void AddMetricSystem(MetricSystem metricSystem)
            => metricSystems.Add(metricSystem);

        internal static void RemoveMetricSystem(MetricSystem metricSystem)
            => metricSystems.Add(metricSystem);

        public static IEnumerable<Unit> GetAllUnits()
        {
            foreach (var metricSystem in MetricSystems)
            {
                foreach (var unit in metricSystem.Units)
                    yield return unit;
            }
        }

        public static IEnumerable<Quantity> GetAllQuantities()
        {
            foreach (var metricSystem in MetricSystems)
            {
                foreach (var quantity in metricSystem.Quantities)
                    yield return quantity;
            }
        }

        public static IEnumerable<MetricPrefix> GetAllMetricPrefixes()
        {
            foreach (var metricSystem in MetricSystems)
            {
                foreach (var prefix in metricSystem.Prefixes)
                    yield return prefix;
            }
        }
    }
}
