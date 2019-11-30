using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Measures
{
    static class MetricSystemManager
    {
        static MetricSystemManager()
        {
            MetricSystems = new List<MetricSystem>();
        }

        public static readonly List<MetricSystem> MetricSystems;

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
