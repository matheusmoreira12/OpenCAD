using OpenCAD.OpenCADFormat.Measures.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    static class MetricSystemManager
    {
        static MetricSystemManager()
        {
            MetricSystems = new List<MetricSystem>();
        }

        public static readonly List<MetricSystem> MetricSystems;

        public static void Import(MetricSystemLibrary library)
        {
            foreach (var metricSystemNode in library.MetricSystems)
            {
                var quantities = importQuantities(metricSystemNode.Quantities).ToArray();
                var metricPrefixes = importPrefixes(metricSystemNode.Prefixes).ToArray();
                var units = importUnits(metricSystemNode.Units).ToArray();
                var metricSystem = new MetricSystem(metricSystemNode.Name, quantities, units, 
                    metricPrefixes);
                MetricSystems.Add(metricSystem);
            }
        }

        private static IEnumerable<Unit> importUnits(BaseUnitNode[] unitNodes)
        {
            foreach (var unitNode in unitNodes)
                yield return importUnits(unitNode);
        }

        private static Unit importUnits(UnitNode unitNode)
        {
            if (unitNode is BaseUnitNode)
            {
                var baseUnitNode = (BaseUnitNode)unitNode;
            }
            else if (unitNode is DerivedUnitNode)
            {
                var derivedUnitMode = (DerivedUnitNode)unitNode;
            }
            return null;
        }

        private static IEnumerable<MetricPrefix> importPrefixes(MetricPrefixNode[] metricPrefixNodes)
        {
            foreach (var metricPrefixNode in metricPrefixNodes)
                yield return importMetricPrefix(metricPrefixNode);
        }

        private static MetricPrefix importMetricPrefix(MetricPrefixNode metricPrefixNode) =>
            new MetricPrefix(metricPrefixNode.Symbol, metricPrefixNode.Multiplier,
                metricPrefixNode.Symbol, metricPrefixNode.UISymbol);

        private static IEnumerable<Quantity> importQuantities(QuantityNode[] quantityNodes)
        {
            foreach (var quantityNode in quantityNodes)
                yield return importQuantity(quantityNode);
        }

        private static Quantity importQuantity(QuantityNode quantityNode)
        {
            if (quantityNode is BaseQuantityNode)
            {
                var baseQuantityNode = (BaseQuantityNode)quantityNode;
                return new BaseQuantity(baseQuantityNode.Name, baseQuantityNode.Symbol, 
                    baseQuantityNode.UISymbol);
            }
            else if (quantityNode is DerivedQuantityNode)
            {
                var derivedQuantityNode = (DerivedQuantityNode)quantityNode;
                return new DerivedQuantity(derivedQuantityNode.Name,
                    derivedQuantityNode.Symbol, DerivedQuantityDimension.Parse(derivedQuantityNode.Dimension));
            }
            return null;
        }
    }
}
