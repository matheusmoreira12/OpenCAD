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
            Conversions.FirstOrDefault(c => c.SourceUnit == sourceUnit && c.TargetUnit == targetUnit);

        public static UnitConversion GetDirectConversion(Unit sourceUnit, Unit targetUnit)
            => getConversionStrict(sourceUnit, targetUnit) ?? getConversionStrict(targetUnit, sourceUnit)?.Invert();

        public static IEnumerable<UnitConversion> GetForSourceUnit(Unit sourceUnit)
        {
            var directConversions = Conversions.FindAll(c => c.SourceUnit == sourceUnit);
            var inverseConversions = Conversions.FindAll(c => c.TargetUnit == sourceUnit).Select(c => c.Invert());
            return directConversions.Concat(inverseConversions);
        }

        public static IEnumerable<UnitConversion> GetForTargetUnit(Unit targetUnit)
        {
            var directConversions = Conversions.FindAll(c => c.TargetUnit == targetUnit);
            var inverseConversions = Conversions.FindAll(c => c.SourceUnit == targetUnit).Select(c => c.Invert());
            return directConversions.Concat(inverseConversions);
        }

        private static int maxRecursion => (int)System.Math.Pow(Conversions.Count, Conversions.Count);

        public static UnitConversionTree getConversionTree(Unit sourceUnit, Unit targetUnit,
            IList<UnitConversion> topRecursion = null)
        {
            var recursion = new List<UnitConversion>(topRecursion ?? new UnitConversion[0]);

            UnitConversionTree tree = new UnitConversionTree();
            var sourceConversions = GetForSourceUnit(sourceUnit);
            foreach (var sourceConversion in sourceConversions)
            {
                if (!recursion.Contains(sourceConversion))
                {
                    recursion.Add(sourceConversion);

                    var branch = new UnitConversionBranch(sourceConversion);
                    tree.Branches.Add(branch);

                    var cascadedTree = getConversionTree(sourceConversion.TargetUnit,
                        targetUnit, recursion);
                    branch.Branches.AddRange(cascadedTree.Branches);
                }
            }
            return tree;
        }

        private static IEnumerable<UnitConversionBranch> getValidConversionBranches(UnitConversionTree conversionTree, Unit targetUnit)
        {
            foreach (var branch in conversionTree.Branches)
            {
                var traversedBranches = branch.TraverseBranches().ToArray();
                bool isBranchSuccessful = traversedBranches.Length > 0 && traversedBranches
                    .Last().Item.TargetUnit == targetUnit;
                if (isBranchSuccessful)
                    yield return branch;
            }
        }

        private static UnitConversion compileComplexConversion(Unit sourceUnit, Unit targetUnit)
        {
            var conversionTree = getConversionTree(sourceUnit, targetUnit);
            var validConversionBranches = getValidConversionBranches(conversionTree, targetUnit).ToArray();
            var shortestValidConversionBranchInfo = validConversionBranches
                .Select(cb => (cb, tb: cb.TraverseBranches()))
                .OrderBy(bi => bi.tb.Count())
                .FirstOrDefault();
            bool hasConversion = !(shortestValidConversionBranchInfo.cb is null);
            if (hasConversion)
            {
                double aggregateConversionFactor = 1;
                aggregateConversionFactor *= shortestValidConversionBranchInfo.cb.Item.Factor;
                foreach (var traversedBranch in shortestValidConversionBranchInfo.tb)
                    aggregateConversionFactor *= traversedBranch.Item.Factor;
                return new UnitConversion(sourceUnit, targetUnit, aggregateConversionFactor);
            }
            else
                return null;
        }

        public static UnitConversion GetBestConversion(Unit sourceUnit, Unit targetUnit)
        {
            var directConversion = GetDirectConversion(sourceUnit, targetUnit);
            if (directConversion is null)
                return compileComplexConversion(sourceUnit, targetUnit);
            else
                return directConversion;
        }

        public static Scalar? GetScaleZero(Unit unit)
        {
            Scalar zero = Scalar.Zero;
            if (ScaleZeroes.TryGetValue(unit, out zero))
                return zero;
            else
                return null;
        }
    }
}
