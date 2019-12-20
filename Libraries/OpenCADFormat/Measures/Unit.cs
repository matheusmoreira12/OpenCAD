using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Unit : IMultipliable<Unit, Unit>, IMultipliable<MetricPrefix, Unit>, IExponentiable<Unit>
    {
        public static Unit operator *(Unit a, Unit b) => Math.Multiply<Unit, Unit>(a, b);

        public static Unit operator *(MetricPrefix a, Unit b) => Math.Multiply<MetricPrefix, Unit>(b, a);

        public static Unit operator *(Unit a, MetricPrefix b) => Math.Multiply<MetricPrefix, Unit>(a, b);

        public static Unit operator /(Unit a, Unit b) => Math.Divide<Unit, Unit>(a, b);

        public static Unit operator !(Unit a) => Math.Invert(a);

        public static Unit operator ^(Unit a, double b) => Math.Power(a, b);

        public static Unit Parse(string value)
        {
            Unit result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static bool TryParse(string value, out Unit result) =>
            tryParseBySymbol(value, out result) || tryParseByUISymbol(value, out result) || tryParseByName(value, out result);

        private static bool tryParseBySymbol(string symbol, out Unit result)
        {
            result = MetricSystemManager.GetAllUnits().FirstOrDefault(u => u.Symbol == symbol);
            if (result == default)
                return false;
            return true;
        }

        private static bool tryParseByUISymbol(string uiSymbol, out Unit result)
        {
            result = MetricSystemManager.GetAllUnits().FirstOrDefault(u => u.UISymbol == uiSymbol);
            if (result == default)
                return false;
            return true;
        }

        private static bool tryParseByName(string name, out Unit result)
        {
            result = MetricSystemManager.GetAllUnits().FirstOrDefault(u => u.Name == name);
            if (result == default)
                return false;
            return true;
        }

        public abstract Unit Collapse();

        public abstract string Name { get; }

        public abstract Quantity Quantity { get; }

        public abstract string Symbol { get; }

        public abstract string UISymbol { get; }

        public MetricSystem MetricSystem { get; internal set; }

        Unit IMultipliable<Unit, Unit>.Multiply(Unit value)
        {
            DerivedUnit derivedThis = null;
            if (this is BaseUnit)
                derivedThis = new DerivedUnit((BaseUnit)this, 1);
            else if (this is DerivedUnit)
                derivedThis = (DerivedUnit)this;

            DerivedUnit derivedValue = null;
            if (value is BaseUnit)
                derivedValue = new DerivedUnit((BaseUnit)value, 1);
            else if (value is DerivedUnit)
                derivedValue = (DerivedUnit)value;

            if (derivedThis is null || derivedValue is null)
                throw new NotSupportedException();

            return multiply(derivedThis, derivedValue);
        }

        private Unit multiply(DerivedUnit a, DerivedUnit b)
        {
            var members = a.Expression.Members.Concat(b.Expression.Members).ToArray();
            var expression = new DerivedUnitExpression(members);
            return new DerivedUnit(expression);
        }

        Unit IMultipliable<MetricPrefix, Unit>.Multiply(MetricPrefix value)
        {
            if (this is BaseUnit)
                return new DerivedUnit((BaseUnit)this, value, 1);

            throw new NotSupportedException();
        }

        Unit IExponentiable<Unit>.Exponentiate(double exponent)
        {
            DerivedUnit derivedThis = null;
            if (this is BaseUnit)
                derivedThis = new DerivedUnit((BaseUnit)this, 1);
            else if (this is DerivedUnit)
                derivedThis = (DerivedUnit)this;

            if (derivedThis is null)
                throw new NotSupportedException();

            return exponentiate(derivedThis, exponent);
        }

        private Unit exponentiate(DerivedUnit derivedUnit, double exponent)
        {
            var members = derivedUnit.Expression.Members.Select(m => new DerivedUnitExpressionMember(m.BaseUnit,
                m.Prefix, m.Exponent * exponent)).ToArray();
            var expression = new DerivedUnitExpression(members);
            return new DerivedUnit(expression);
        }
    }
}