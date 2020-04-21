﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures.Conversion
{
    internal static class UnitConversionManager
    {
        private static List<UnitConversion> allConversions { get; } = new List<UnitConversion> { };

        private static Dictionary<Unit, Scalar> allScaleZeroes { get; } = new Dictionary<Unit, Scalar> { };

        private static UnitConversion getConversionStrict(Unit sourceUnit, Unit targetUnit) =>
            allConversions.FirstOrDefault(c => Utils.NullableEquals(c.SourceUnit, sourceUnit)
                && Utils.NullableEquals(c.TargetUnit, targetUnit));

        public static void Add(UnitConversion conversion)
        {
            if (Get(conversion.SourceUnit, conversion.TargetUnit) == null)
                allConversions.Add(conversion);
        }

        public static UnitConversion[] GetAll() => allConversions.ToArray();

        public static UnitConversion GetDirect(Unit sourceUnit, Unit targetUnit)
            => getConversionStrict(sourceUnit, targetUnit)
            ?? getConversionStrict(targetUnit, sourceUnit)?.Invert();

        public static IEnumerable<UnitConversion> GetDirectFrom(Unit sourceUnit) => allConversions
            .FindAll(c => Utils.NullableEquals(c.SourceUnit, sourceUnit)).Concat(allConversions
            .FindAll(c => Utils.NullableEquals(c.TargetUnit, sourceUnit)).Select(c => c.Invert()));

        public static IEnumerable<UnitConversion> GetDirectTo(Unit targetUnit) => allConversions
            .FindAll(c => Utils.NullableEquals(c.TargetUnit, targetUnit)).Concat(allConversions
            .FindAll(c => Utils.NullableEquals(c.SourceUnit, targetUnit)).Select(c => c.Invert()));

        public static Tree<UnitConversion> getConversionTree(Unit sourceUnit,
            Unit targetUnit, IEnumerable<Unit> recursion = null)
        {
            if (recursion == null) recursion = new Unit[0];

            var tree = new Tree<UnitConversion>();
            var sourceUnitConversions = GetDirectFrom(sourceUnit);
            foreach (var conversion in sourceUnitConversions)
            {
                TreeItem<UnitConversion> subItem = null;
                if (conversion.TargetUnit == targetUnit)
                {
                    subItem = new TreeItem<UnitConversion>(conversion);
                    tree.AddChild(subItem);
                    break;
                }
                else if (recursion.Contains(conversion.TargetUnit))
                {
                    var newRecursion = recursion.Concat(new[] { conversion.TargetUnit });
                    var subTree = getConversionTree(sourceUnit, targetUnit, newRecursion);
                    subItem = subTree.ToTreeItem(conversion);
                }

                if (subItem != null) tree.AddChild(subItem);
            }
            return tree;
        }

        private static UnitConversion compileComplexConversion(Unit sourceUnit, Unit targetUnit)
        {
            var conversionTree = getConversionTree(sourceUnit, targetUnit);
            double aggregateConversionFactor = 1;

            Console.WriteLine(conversionTree.ToString());

            return null;
        }

        public static UnitConversion Get(Unit sourceUnit, Unit targetUnit)
        {
            var directConversion = GetDirect(sourceUnit, targetUnit);
            if (directConversion is null)
                return compileComplexConversion(sourceUnit, targetUnit);
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
