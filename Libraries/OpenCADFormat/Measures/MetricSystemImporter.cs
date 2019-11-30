using System;
using System.Collections.Generic;
using System.Linq;
using OpenCAD.OpenCADFormat.Measures.Serialization;

namespace OpenCAD.OpenCADFormat.Measures
{
    static class MetricSystemLibraryImporter
    {
        public static IEnumerable<MetricSystem> Import(MetricSystemLibrary library)
        {
            foreach (var metricSystemNode in library.MetricSystems)
            {
                var quantities = importQuantities(metricSystemNode.Quantities).ToArray();
                var metricPrefixes = importPrefixes(metricSystemNode.Prefixes).ToArray();
                var units = importUnits(metricSystemNode.Units).ToArray();
                yield return new MetricSystem(metricSystemNode.Name, quantities, units,
                    metricPrefixes);
            }
        }

        private static IEnumerable<Unit> importUnits(UnitNode[] unitNodes)
        {
            foreach (var baseUnit in importBaseUnits(unitNodes))
                yield return baseUnit;

            foreach (var derivedUnit in importDerivedUnits(unitNodes))
                yield return derivedUnit;
        }

        private static IEnumerable<BaseUnit> importBaseUnits(UnitNode[] unitNodes)
        {
            foreach (var unitNode in unitNodes)
                if (unitNode is BaseUnitNode)
                    yield return importBaseUnit((BaseUnitNode)unitNode);
        }

        private static BaseUnit importBaseUnit(BaseUnitNode unitNode)
        {
            var quantity = Quantity.Parse(unitNode.Quantity);

            return new BaseUnit(unitNode.Name, quantity, unitNode.Symbol, unitNode.UISymbol);
        }

        private static IEnumerable<DerivedUnit> importDerivedUnits(UnitNode[] unitNodes)
        {
            foreach (var unitNode in unitNodes)
                if (unitNode is DerivedUnitNode)
                    yield return importDerivedUnit((DerivedUnitNode)unitNode);
        }

        private static DerivedUnit importDerivedUnit(DerivedUnitNode unitNode)
        {
            throw new NotImplementedException();
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
