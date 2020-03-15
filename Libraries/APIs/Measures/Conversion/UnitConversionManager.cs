using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures.UnitConversion
{
    public class UnitConversionManager
    {
        public static List<UnitConversion> Conversions { get; } = new List<UnitConversion> { };

        public static Dictionary<Unit, Scalar> ScaleZeroes { get; } = new Dictionary<Unit, Scalar> { };

        private static UnitConversion getConversionStrict(Unit sourceUnit, Unit targetUnit) =>
            Conversions.FirstOrDefault(c => Utils.NullableEquals(c.SourceUnit, sourceUnit)
                && Utils.NullableEquals(c.TargetUnit, targetUnit));

        public static UnitConversion GetDirectOnly(Unit sourceUnit, Unit targetUnit)
            => getConversionStrict(sourceUnit, targetUnit)
            ?? getConversionStrict(targetUnit, sourceUnit)?.Invert();

        public static IEnumerable<UnitConversion> GetFrom(Unit sourceUnit) => Conversions
            .FindAll(c => Utils.NullableEquals(c.SourceUnit, sourceUnit)).Concat(Conversions
            .FindAll(c => Utils.NullableEquals(c.TargetUnit, sourceUnit)).Select(c => c.Invert()));

        public static IEnumerable<UnitConversion> GetTo(Unit targetUnit) => Conversions
            .FindAll(c => Utils.NullableEquals(c.TargetUnit, targetUnit)).Concat(Conversions
            .FindAll(c => Utils.NullableEquals(c.SourceUnit, targetUnit)).Select(c => c.Invert()));

        private static int maxRecursion => (int)System.Math.Pow(Conversions.Count, Conversions.Count);

        public static Tree<UnitConversion> getConversionTree(Unit sourceUnit,
            Unit targetUnit, UnitConversion[] recursion = null)
        {
            var tree = new Tree<UnitConversion>();
            if (Utils.VerifyStackOverflow())
                return tree;

            var sourceUnitConversions = GetFrom(sourceUnit);
            foreach (var conversion in sourceUnitConversions)
            {
                TreeItem<UnitConversion> subItem;
                if (conversion.TargetUnit == targetUnit)
                {
                    subItem = new TreeItem<UnitConversion>(conversion);
                    tree.Children.Add(subItem);
                    break;
                }
                else
                {
                    var subTree = getConversionTree(sourceUnit, targetUnit);
                    subItem = subTree.ToTreeItem(conversion);
                }
                tree.Children.Add(subItem);
            }
            return tree;
        }

        private static UnitConversion compileComplexConversion(Unit sourceUnit, Unit targetUnit)
        {
            var conversionTree = mountConversionTree(sourceUnit, targetUnit);
            double aggregateConversionFactor = 1;

        }

        public static UnitConversion Get(Unit sourceUnit, Unit targetUnit)
        {
            var directConversion = GetDirectOnly(sourceUnit, targetUnit);
            if (directConversion is null)
                return compileComplexConversion(sourceUnit, targetUnit);
            else
                return directConversion;
        }

        public static void DefineScaleZero(Unit unit, Scalar zero)
        {
            ScaleZeroes[unit] = zero;
        }

        public static Scalar GetScaleZero(Unit unit)
        {
            Scalar zero = Scalar.Zero;
            if (ScaleZeroes.TryGetValue(unit, out zero))
                return zero;
            else
                return Scalar.Zero;
        }
    }
}
