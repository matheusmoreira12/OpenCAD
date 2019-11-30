using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures.Math
{
    public static class MathExtension
    {
        #region Quantity Math
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
        #endregion

        #region Unit Math
        private static DerivedUnitExpressionMember power(DerivedUnitExpressionMember member, double exponent) =>
            new DerivedUnitExpressionMember(member.BaseUnit, member.Exponent * exponent);

        private static DerivedUnitExpression power(DerivedUnitExpression expression, double exponent) =>
            new DerivedUnitExpression(expression.Members.Select(m => power(m, exponent)).ToArray());

        private static DerivedUnitExpression multiply(DerivedUnitExpressionMember a, DerivedUnitExpressionMember b) =>
            new DerivedUnitExpression(a, b);

        private static DerivedUnitExpression multiply(DerivedUnitExpression a, DerivedUnitExpressionMember b) =>
            new DerivedUnitExpression(a.Members.Concat(new[] { b }).ToArray());

        private static DerivedUnitExpression multiply(DerivedUnitExpressionMember a, DerivedUnitExpression b) =>
            new DerivedUnitExpression(new[] { a }.Concat(b.Members).ToArray());

        private static DerivedUnitExpression multiply(DerivedUnitExpression a, DerivedUnitExpression b) =>
            new DerivedUnitExpression(a.Members.Concat(b.Members).ToArray());

        public static Unit Power(this Unit unit, double exponent)
        {
            if (unit is BaseUnit)
                return Power((BaseUnit)unit, exponent);
            else if (unit is DerivedUnit)
                return power((DerivedUnit)unit, exponent);

            throw new NotImplementedException();
        }

        private static DerivedUnit power(this BaseUnit unit, double exponent)
        {
            var member = new DerivedUnitExpressionMember(unit, exponent);
            var expression = new DerivedUnitExpression(member);
            return new DerivedUnit(expression);
        }

        private static DerivedUnit power(this DerivedUnit unit, double exponent) =>
            new DerivedUnit(power(unit.Expression, exponent));

        public static Unit Square(this Unit unit) => unit.Power(2);

        public static Unit Cube(this Unit unit) => unit.Power(3);

        public static Unit SquareRoot(this Unit unit) => unit.Power(1.0 / 2.0);

        public static Unit CubicRoot(this Unit unit) => unit.Power(1.0 / 3.0);

        public static Unit Invert(this Unit unit) => unit.Power(-1);

        public static Unit Multiply(this Unit a, Unit b)
        {
            if (a is BaseUnit)
            {
                if (b is BaseUnit)
                    return multiply((BaseUnit)a, (BaseUnit)b);
                else if (b is DerivedUnit)
                    return multiply((BaseUnit)a, (DerivedUnit)b);
            }
            else if (a is DerivedUnit)
            {
                if (b is BaseUnit)
                    return multiply((DerivedUnit)a, (BaseUnit)b);
                else if (b is DerivedUnit)
                    return multiply((DerivedUnit)a, (DerivedUnit)b);
            }

            throw new NotImplementedException();
        }

        private static DerivedUnit multiply(BaseUnit a, BaseUnit b)
        {
            var memberA = new DerivedUnitExpressionMember(a, 1);
            var memberB = new DerivedUnitExpressionMember(b, 1);
            return new DerivedUnit(multiply(memberA, memberB));
        }

        private static DerivedUnit multiply(BaseUnit a, DerivedUnit b)
        {
            var memberA = new DerivedUnitExpressionMember(a, 1);
            return new DerivedUnit(multiply(memberA, b.Expression));
        }

        private static DerivedUnit multiply(DerivedUnit a, BaseUnit b)
        {
            var memberB = new DerivedUnitExpressionMember(a, 1);
            return new DerivedUnit(multiply(a.Expression, memberB));
        }

        private static DerivedUnit multiply(DerivedUnit a, DerivedUnit b) =>
            new DerivedUnit(multiply(a.Expression, b.Expression));

        public static Unit Multiply(this Unit unit, MetricPrefix prefix)
        {
            if (unit is BaseUnit)
                return multiply((BaseUnit)unit, prefix);
            else if (unit is DerivedUnit)
                throw new InvalidOperationException("Cannot multiply a derived unit with a prefix.");

            throw new NotImplementedException();
        }

        private static DerivedUnit multiply(BaseUnit unit, MetricPrefix prefix) =>
            new DerivedUnit(new DerivedUnitExpression(new DerivedUnitExpressionMember(unit, prefix, 1)));

        public static Unit Divide(this Unit a, Unit b) => Multiply(a, Invert(b));
        #endregion

        #region Scalar Math
        public static Scalar Add(this Scalar a, Scalar b) => new Scalar(a.Amount + b.ConvertTo(a.Unit).Amount, a.Unit);

        public static Scalar Subtract(this Scalar a, Scalar b) => new Scalar(a.Amount - b.ConvertTo(a.Unit).Amount, a.Unit);

        public static Scalar Negate(this Scalar a) => new Scalar(-a.Amount, a.Unit);

        public static Scalar Invert(this Scalar a) => new Scalar(1.0 / a.Amount, !a.Unit);

        public static Scalar Multiply(this Scalar a, double b) => new Scalar(a.Amount * b, a.Unit);

        public static Scalar Multiply(this Scalar a, Scalar b) => new Scalar(a.Amount * b.Amount, a.Unit * b.Unit);

        public static Scalar Divide(this Scalar a, double b) => new Scalar(a.Amount / b, a.Unit);

        public static Scalar Divide(this Scalar a, Scalar b) => new Scalar(a.Amount / b.Amount, a.Unit / b.Unit);

        public static Scalar Power(this Scalar a, double b) => new Scalar(System.Math.Pow(a.Amount, b), a.Unit.Power(b));

        public static Scalar Square(this Scalar a) => Power(a, 2);

        public static Scalar Cube(this Scalar a) => Power(a, 3);

        public static Scalar SquareRoot(this Scalar a) => Power(a, 1.0 / 2.0);

        public static Scalar CubicRoot(this Scalar a) => Power(a, 1.0 / 3.0);
        #endregion
    }
}
