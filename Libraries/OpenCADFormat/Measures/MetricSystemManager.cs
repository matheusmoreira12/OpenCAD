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
    }
}
