using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public static class QuantityMath
    {
        public static DerivedQuantityDimensionMember Power(this DerivedQuantityDimensionMember member, double exponent) =>
            new DerivedQuantityDimensionMember(member.BaseQuantity, member.Exponent * exponent);

        public static DerivedQuantityDimensionMember Square(this DerivedQuantityDimensionMember member) => Power(member, 2);

        public static DerivedQuantityDimensionMember Cube(this DerivedQuantityDimensionMember member) => Power(member, 3);

        public static DerivedQuantityDimensionMember SquareRoot(this DerivedQuantityDimensionMember member) => Power(member, 1.0 / 2.0);

        public static DerivedQuantityDimensionMember CubicRoot(this DerivedQuantityDimensionMember member) => Power(member, 1.0 / 3.0);

        public static DerivedQuantityDimensionMember Invert(this DerivedQuantityDimensionMember member) => Power(member, -1);

        public static DerivedQuantityDimension Multiply(this DerivedQuantityDimensionMember memberA,
            DerivedQuantityDimensionMember memberB) => new DerivedQuantityDimension(memberA, memberB);

        public static DerivedQuantityDimension Divide(this DerivedQuantityDimensionMember memberA,
            DerivedQuantityDimensionMember memberB) => Multiply(memberA, Invert(memberB));


        public static DerivedQuantityDimension Power(this DerivedQuantityDimension dimension, double exponent) =>
            new DerivedQuantityDimension(dimension.Members.Select(m => m.Power(exponent)).ToArray());

        public static DerivedQuantityDimension Square(this DerivedQuantityDimension dimension) => Power(dimension, 2);

        public static DerivedQuantityDimension Cube(this DerivedQuantityDimension dimension) => Power(dimension, 3);

        public static DerivedQuantityDimension SquareRoot(this DerivedQuantityDimension dimension) => Power(dimension, 1.0 / 2.0);

        public static DerivedQuantityDimension CubicRoot(this DerivedQuantityDimension dimension) => Power(dimension, 1.0 / 3.0);

        public static DerivedQuantityDimension Invert(this DerivedQuantityDimension dimension) => Power(dimension, -1);

        public static DerivedQuantityDimension Multiply(this DerivedQuantityDimension dimensionA, DerivedQuantityDimension dimensionB)
        {
            var members = new List<DerivedQuantityDimensionMember>(dimensionA.Members);
            members.AddRange(dimensionB.Members);

            return new DerivedQuantityDimension(members.ToArray());
        }

        public static DerivedQuantityDimension Divide(this DerivedQuantityDimension dimensionA, DerivedQuantityDimension dimensionB) =>
            Multiply(dimensionA, Invert(dimensionB));

        public static DerivedQuantityDimension Solve(this DerivedQuantityDimension dimension)
        {
            var membersSolver = dimension.Members
                .GroupBy(m => m.BaseQuantity)
                .Select(g => new DerivedQuantityDimensionMember(g.Key, g.Sum(o => o.Exponent)))
                .Where(p => p.Exponent != 0);

            return new DerivedQuantityDimension(membersSolver.ToArray());
        }
    }
}
