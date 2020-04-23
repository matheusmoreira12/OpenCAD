using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures.Conversion
{
    internal static class UnitConversionManager
    {
        private static List<UnitConversion> allConversions { get; } = new List<UnitConversion> { };

        private static Dictionary<Unit, Scalar> allScaleZeroes { get; } = new Dictionary<Unit, Scalar> { };

        private static UnitConversion getStrict(Unit sourceUnit, Unit targetUnit) =>
            allConversions.FirstOrDefault(c => Utils.NullableEquals(c.SourceUnit, sourceUnit)
                && Utils.NullableEquals(c.TargetUnit, targetUnit));

        public static void Add(UnitConversion conversion)
        {
            if (GetDirect(conversion.SourceUnit, conversion.TargetUnit) == null)
                allConversions.Add(conversion);
            else
                throw new InvalidOperationException("Cannot add unit conversion. The specified unit" +
                    " conversion already exists.");
        }

        public static UnitConversion[] GetAll() => allConversions.ToArray();

        public static UnitConversion GetDirect(Unit sourceUnit, Unit targetUnit)
            => getStrict(sourceUnit, targetUnit)
            ?? getStrict(targetUnit, sourceUnit)?.Invert();

        public static IEnumerable<UnitConversion> GetDirectFrom(Unit sourceUnit) => allConversions
            .FindAll(c => Utils.NullableEquals(c.SourceUnit, sourceUnit)).Concat(allConversions
            .FindAll(c => Utils.NullableEquals(c.TargetUnit, sourceUnit)).Select(c => c.Invert()));

        public static IEnumerable<UnitConversion> GetDirectTo(Unit targetUnit) => allConversions
            .FindAll(c => Utils.NullableEquals(c.TargetUnit, targetUnit)).Concat(allConversions
            .FindAll(c => Utils.NullableEquals(c.SourceUnit, targetUnit)).Select(c => c.Invert()));

        public static Tree<UnitConversion> getConversionTree(Unit sourceUnit,
            Unit targetUnit, Recursion<Unit> recursion = null)
        {
            if (recursion == null) recursion = new Recursion<Unit>();

            var tree = new Tree<UnitConversion>();
            var sourceUnitConversions = GetDirectFrom(sourceUnit);
            foreach (var conversion in sourceUnitConversions)
            {
                if (!recursion.Contains(conversion.TargetUnit))
                {
                    TreeItem<UnitConversion> subItem = null;
                    if (Utils.NullableEquals(conversion.TargetUnit, targetUnit))
                    {
                        subItem = new TreeItem<UnitConversion>(conversion);
                        tree.AddChild(subItem);
                        break;
                    }
                    else
                    {
                        var newRecursion = recursion.Recurse(conversion.TargetUnit);
                        var subTree = getConversionTree(conversion.TargetUnit, targetUnit, newRecursion);
                        subItem = subTree.ToTreeItem(conversion);
                    }
                    tree.AddChild(subItem);
                }
            }
            return tree;
        }

        private static bool compileConversionTreeRecursive(Tree<UnitConversion> conversionTree,
            Unit targetUnit, out double aggregateFactor)
        {
            foreach (var child in conversionTree.Children)
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
                return UnitConversion.Define(sourceUnit, targetUnit, aggregateFactor);
            else
                return null;
        }

        public static UnitConversion Get(Unit sourceUnit, Unit targetUnit)
        {
            var directConversion = GetDirect(sourceUnit, targetUnit);
            if (directConversion is null)
            {
                var conversionTree = getConversionTree(sourceUnit, targetUnit);
                return compileConversionTree(conversionTree, sourceUnit, targetUnit);
            }
            else
                return directConversion;
        }

        public static void DefineScaleZero(Unit unit, Scalar zero)
        {
            if (zero.Unit == unit)
            {
                if (allScaleZeroes.ContainsKey(unit))
                    throw new InvalidOperationException("Cannot define scale zero. " +
                        "Scale zero has already been defined.");
                else
                    allScaleZeroes[unit] = zero;
            }
            else
                throw new InvalidOperationException("Cannot define scale zero." +
                    "Units must match.");
        }

        public static Scalar GetScaleZero(Unit unit)
        {
            Scalar zero;
            if (allScaleZeroes.TryGetValue(unit, out zero))
                return zero;
            else
                return Scalar.Zero;
        }
    }
}
