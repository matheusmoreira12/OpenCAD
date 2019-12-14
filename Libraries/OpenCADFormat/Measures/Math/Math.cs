using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface ISummable<TValue, TResult>
    {
        TResult Sum(TValue value);
    }

    public static class ISummableExtension
    {
        public static TResult SumImplicit<TValue, TResult>(this ISummable<TValue, TResult> a, TValue b)
        {
            throw new NotImplementedException();
        }
    }

    public interface IMultipliable<TValue, TResult>
    {
        TResult Multiply(TValue value);
    }

    public static class IMultipliableExtension
    {
        public static TResult MultiplyImplicit<TValue, TResult>(this IMultipliable<TValue, TResult> a, TValue b)
        {
            throw new NotImplementedException();
        }
    }

    public interface IExponentiable<TExponent, TResult>
    {
        TResult Exponentiate(TExponent exponent);
    }

    public static class IExponentiableExtension
    {
        public static TResult ExponentiateImplicit<TExponent, TResult>(this IExponentiable<TExponent, TResult> value, 
            TExponent exponent)
        {
            throw new NotImplementedException();
        }
    }

    public static class Math
    {
        #region Quantity Math
        public static DerivedQuantityDimensionMember Power(DerivedQuantityDimensionMember member, double exponent) =>
            new DerivedQuantityDimensionMember(member.BaseQuantity, member.Exponent * exponent);

        public static DerivedQuantityDimension Power(DerivedQuantityDimension dimension, double exponent) =>
            new DerivedQuantityDimension(dimension.Members.Select(m => Power(m, exponent)).ToArray());

        public static DerivedQuantityDimension Multiply(DerivedQuantityDimensionMember a, DerivedQuantityDimensionMember b) =>
            new DerivedQuantityDimension(a, b);

        public static DerivedQuantityDimension Multiply(DerivedQuantityDimensionMember a, DerivedQuantityDimension b) =>
            new DerivedQuantityDimension(new[] { a }.Concat(b.Members).ToArray());

        public static DerivedQuantityDimension Multiply(DerivedQuantityDimension a, DerivedQuantityDimensionMember b) =>
            new DerivedQuantityDimension(a.Members.Concat(new[] { b }).ToArray());

        public static DerivedQuantityDimension Multiply(DerivedQuantityDimension a, DerivedQuantityDimension b) =>
            new DerivedQuantityDimension(a.Members.Concat(b.Members).ToArray());

        public static Quantity Power(Quantity quantity, double exponent)
        {
            if (quantity is BaseQuantity)
                return power((BaseQuantity)quantity, exponent);
            else if (quantity is DerivedQuantity)
                return power((DerivedQuantity)quantity, exponent);

            throw new NotImplementedException();
        }

        private static Quantity power(BaseQuantity quantity, double exponent)
        {
            var member = new DerivedQuantityDimensionMember(quantity, exponent);
            var dimension = new DerivedQuantityDimension(member);
            return new DerivedQuantity(dimension);
        }

        private static Quantity power(DerivedQuantity quantity, double exponent) =>
            new DerivedQuantity(Power(quantity.Dimension, exponent));

        public static Quantity Square(Quantity quantity) => Power(quantity, 2);

        public static Quantity Cube(Quantity quantity) => Power(quantity, 3);

        public static Quantity SquareRoot(Quantity quantity) => Power(quantity, 1.0 / 2.0);

        public static Quantity CubicRoot(Quantity quantity) => Power(quantity, 1.0 / 3.0);

        public static Quantity Invert(Quantity quantity) => Power(quantity, -1);

        public static Quantity Multiply(Quantity a, Quantity b)
        {
            if (a is null)
                return b;

            if (b is null)
                return a;

            if (a is BaseQuantity)
            {
                if (b is BaseQuantity)
                    return multiply((BaseQuantity)a, (BaseQuantity)b);
                else if (b is DerivedQuantity)
                    return multiply((DerivedQuantity)a, (BaseQuantity)b);
            }
            else if (b is BaseQuantity)
            {
                if (b is BaseQuantity)
                    return multiply((BaseQuantity)a, (DerivedQuantity)b);
                else if (b is DerivedQuantity)
                    return multiply((DerivedQuantity)a, (DerivedQuantity)b);
            }

            throw new NotImplementedException();
        }

        private static Quantity multiply(BaseQuantity a, BaseQuantity b)
        {
            var memberA = new DerivedQuantityDimensionMember(a, 1);
            var memberB = new DerivedQuantityDimensionMember(b, 1);
            return new DerivedQuantity(Multiply(memberA, memberB));
        }

        private static Quantity multiply(BaseQuantity a, DerivedQuantity b)
        {
            var memberA = new DerivedQuantityDimensionMember(a, 1);
            return new DerivedQuantity(Multiply(b.Dimension, memberA));
        }

        private static Quantity multiply(DerivedQuantity a, BaseQuantity b)
        {
            var memberB = new DerivedQuantityDimensionMember(b, 1);
            return new DerivedQuantity(Multiply(a.Dimension, memberB));
        }

        private static Quantity multiply(DerivedQuantity a, DerivedQuantity b) =>
            new DerivedQuantity(Multiply(a.Dimension, b.Dimension));

        public static Quantity Divide(Quantity a, Quantity b) => Multiply(a, Invert(b));
        #endregion

        #region Unit Math
        public static DerivedUnitExpressionMember Power(DerivedUnitExpressionMember member, double exponent) =>
            new DerivedUnitExpressionMember(member.BaseUnit, member.Prefix, member.Exponent * exponent);

        public static DerivedUnitExpression Power(DerivedUnitExpression expression, double exponent) =>
            new DerivedUnitExpression(expression.Members.Select(m => Power(m, exponent)).ToArray());

        public static DerivedUnitExpression Multiply(DerivedUnitExpressionMember a, DerivedUnitExpressionMember b) =>
            new DerivedUnitExpression(a, b);

        public static DerivedUnitExpression Multiply(DerivedUnitExpression a, DerivedUnitExpressionMember b) =>
            new DerivedUnitExpression(a.Members.Concat(new[] { b }).ToArray());

        public static DerivedUnitExpression Multiply(DerivedUnitExpressionMember a, DerivedUnitExpression b) =>
            new DerivedUnitExpression(new[] { a }.Concat(b.Members).ToArray());

        public static DerivedUnitExpression Multiply(DerivedUnitExpression a, DerivedUnitExpression b) =>
            new DerivedUnitExpression(a.Members.Concat(b.Members).ToArray());

        public static Unit Power(Unit unit, double exponent)
        {
            if (unit is BaseUnit)
                return power((BaseUnit)unit, exponent);
            else if (unit is DerivedUnit)
                return power((DerivedUnit)unit, exponent);

            throw new NotImplementedException();
        }

        private static DerivedUnit power(BaseUnit unit, double exponent)
        {
            var member = new DerivedUnitExpressionMember(unit, exponent);
            var expression = new DerivedUnitExpression(member);
            return new DerivedUnit(expression);
        }

        private static DerivedUnit power(DerivedUnit unit, double exponent) =>
            new DerivedUnit(Power(unit.Expression, exponent));

        public static Unit Square(Unit unit) => Power(unit, 2);

        public static Unit Cube(Unit unit) => Power(unit, 3);

        public static Unit SquareRoot(Unit unit) => Power(unit, 1.0 / 2.0);

        public static Unit CubicRoot(Unit unit) => Power(unit, 1.0 / 3.0);

        public static Unit Invert(Unit unit) => Power(unit, -1);

        public static Unit Multiply(Unit a, Unit b)
        {
            if (a is null)
                return b;

            if (b is null)
                return a;

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
            return new DerivedUnit(Multiply(memberA, memberB));
        }

        private static DerivedUnit multiply(BaseUnit a, DerivedUnit b)
        {
            var memberA = new DerivedUnitExpressionMember(a, 1);
            return new DerivedUnit(Multiply(memberA, b.Expression));
        }

        private static DerivedUnit multiply(DerivedUnit a, BaseUnit b)
        {
            var memberB = new DerivedUnitExpressionMember(b, 1);
            return new DerivedUnit(Multiply(a.Expression, memberB));
        }

        private static DerivedUnit multiply(DerivedUnit a, DerivedUnit b) =>
            new DerivedUnit(Multiply(a.Expression, b.Expression));

        public static Unit Multiply(Unit unit, MetricPrefix prefix)
        {
            if (unit is BaseUnit)
                return multiply((BaseUnit)unit, prefix);
            else if (unit is DerivedUnit)
                throw new InvalidOperationException("Cannot multiply a derived unit by a prefix.");

            throw new NotImplementedException();
        }

        private static DerivedUnit multiply(BaseUnit unit, MetricPrefix prefix) =>
            new DerivedUnit(new DerivedUnitExpression(new DerivedUnitExpressionMember(unit, prefix, 1)));

        public static Unit Multiply(MetricPrefix prefix, Unit unit) =>
            Multiply(unit, prefix);

        public static Unit Divide(Unit a, Unit b) => Multiply(a, Invert(b));
        #endregion

        #region Scalar Math
        public static Scalar Add(Scalar a, Scalar b) => new Scalar(a.Amount + b.ConvertTo(a.Unit).Amount, a.Unit);

        public static Scalar Subtract(Scalar a, Scalar b) => new Scalar(a.Amount - b.ConvertTo(a.Unit).Amount, a.Unit);

        public static Scalar Negate(Scalar a) => new Scalar(-a.Amount, a.Unit);

        public static Scalar Invert(Scalar a) => new Scalar(1 / a.Amount, Invert(a.Unit));

        public static Scalar Multiply(Scalar a, double b) => new Scalar(a.Amount * b, a.Unit);

        public static Scalar Multiply(Scalar a, Scalar b) => new Scalar(a.Amount * b.Amount, Multiply(a.Unit, b.Unit));

        public static Scalar Divide(Scalar a, double b) => new Scalar(a.Amount / b, a.Unit);

        public static Scalar Divide(Scalar a, Scalar b) => new Scalar(a.Amount / b.Amount, Divide(a.Unit, b.Unit));

        public static Scalar Power(Scalar a, double b) => new Scalar(System.Math.Pow(a.Amount, b), Power(a.Unit, b));

        public static Scalar Square(Scalar a) => Power(a, 2);

        public static Scalar Cube(Scalar a) => Power(a, 3);

        public static Scalar SquareRoot(Scalar a) => Power(a, 1.0 / 2.0);

        public static Scalar CubicRoot(Scalar a) => Power(a, 1.0 / 3.0);
        #endregion
    }
}
