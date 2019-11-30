using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    static class UnitMath
    {
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
    }
}
