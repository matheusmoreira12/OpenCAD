using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.APIs.Measures.Conversion
{
    static class UnitConversionFinder
    {
        private static Tree<UnitConversion> getConversionTree(Unit sourceUnit,
            Unit targetUnit, Recursion<Unit> recursion = null)
        {
            if (recursion == null) recursion = new Recursion<Unit>();

            var tree = new Tree<UnitConversion>();
            var sourceUnitConversions = UnitConversionManager.GetDirectFrom(sourceUnit);
            foreach (var conversion in sourceUnitConversions)
            {
                if (!recursion.Contains(conversion.TargetUnit))
                {
                    TreeItem<UnitConversion> subItem;
                    if (Utils.NullableEquals(conversion.TargetUnit, targetUnit))
                    {
                        subItem = new TreeItem<UnitConversion>(conversion);
                        tree.Children.Add(subItem);
                        break;
                    }
                    else
                    {
                        var newRecursion = recursion.Recurse(conversion.TargetUnit);
                        var subTree = getConversionTree(conversion.TargetUnit, targetUnit, newRecursion);
                        subItem = subTree.ToTreeItem(conversion);
                    }
                    tree.Children.Add(subItem);
                }
            }
            return tree;
        }

        private static bool compileConversionTreeRecursive(Tree<UnitConversion> conversionTree,
            Unit targetUnit, out double aggregateFactor)
        {
            foreach (var child in conversionTree.Children.ToArray())
            {
                var conversion = child.Value;
                double _aggregateFactor;
                if (conversion.TargetUnit == targetUnit)
                {
                    aggregateFactor = conversion.Factor;
                    return true;
                }
                else if (compileConversionTreeRecursive(child, targetUnit, out _aggregateFactor))
                {
                    aggregateFactor = _aggregateFactor * conversion.Factor;
                    return true;
                }
            }
            aggregateFactor = default;
            return false;
        }

        private static UnitConversion compileConversionTree(Tree<UnitConversion> conversionTree,
            Unit sourceUnit, Unit targetUnit)
        {
            double aggregateFactor;
            if (compileConversionTreeRecursive(conversionTree, targetUnit, out aggregateFactor))
            {
                var aggregateConversion = new UnitConversion(sourceUnit, targetUnit, aggregateFactor);
                return aggregateConversion;
            }
            else
                return null;
        }

        public static UnitConversion Find(Unit sourceUnit, Unit targetUnit)
        {
            var conversionTree = getConversionTree(sourceUnit, targetUnit);
            var aggregateConversion = compileConversionTree(conversionTree, sourceUnit, targetUnit);
            return aggregateConversion;
        }
    }
}
