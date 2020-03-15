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

        public static IEnumerable<UnitConversion> GetFrom(Unit sourceUnit)
        {
            var directConversions = Conversions.FindAll(c => Utils.NullableEquals(c.SourceUnit, 
                sourceUnit));
            var inverseConversions = Conversions.FindAll(c => Utils.NullableEquals(c.TargetUnit, 
                sourceUnit)).Select(c => c.Invert());
            return directConversions.Concat(inverseConversions);
        }

        public static IEnumerable<UnitConversion> GetTo(Unit targetUnit)
        {
            var directConversions = Conversions.FindAll(c => Utils.NullableEquals(c.TargetUnit, 
                targetUnit));
            var inverseConversions = Conversions.FindAll(c => Utils.NullableEquals(c.SourceUnit, 
                targetUnit)).Select(c => c.Invert());
            return directConversions.Concat(inverseConversions);
        }

        private static int maxRecursion => (int)System.Math.Pow(Conversions.Count, Conversions.Count);

        public static UnitConversionTree getConversionTree(Unit sourceUnit, Unit targetUnit,
            int recursionLevel = 0)
        {
            UnitConversionTree tree = new UnitConversionTree();
            var sourceConversions = GetFrom(sourceUnit);
            foreach (var sourceConversion in sourceConversions)
            {
                if (recursionLevel < 1000)
                {
                    var branch = new UnitConversionTreeItem(sourceConversion);
                    tree.Children.Add(branch);

                    var cascadedTree = getConversionTree(sourceConversion.TargetUnit,
                        targetUnit, recursionLevel);
                    branch.Children.AddRange(cascadedTree.Children);
                }
            }
            return tree;
        }

        private static IEnumerable<UnitConversionTreeItem[]> getValidConversionBranches(UnitConversionTree conversionTree, Unit targetUnit)
        {
            foreach (var branch in conversionTree.Children)
            {
                var traversedBranches = branch.TraverseBranches().ToArray();
                UnitConversion finalConversion = traversedBranches.Last().Value;
                bool isBranchSuccessful = traversedBranches.Length > 0
                    && Utils.NullableEquals(finalConversion.TargetUnit, targetUnit);
                if (isBranchSuccessful)
                    yield return traversedBranches;
            }
        }

        private static UnitConversion compileComplexConversion(Unit sourceUnit, Unit targetUnit)
        {
            var conversionTree = getConversionTree(sourceUnit, targetUnit);
            var validTraversions = getValidConversionBranches(conversionTree, targetUnit).ToArray();
            var shortestTraversion = validTraversions.GetLowest(bt => bt.Length);
            bool hasConversion = !(shortestTraversion is null);
            if (hasConversion)
            {
                double conversionFactor = shortestTraversion
                    .Select(b => b.Value.Factor)
                    .Aggregate(1.0, (a, b) => a * b);
                return new UnitConversion(sourceUnit, targetUnit, conversionFactor);
            }
            else
                return null;
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
