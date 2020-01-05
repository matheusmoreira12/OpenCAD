using System;
using System.Linq;

using MathAPI = OpenCAD.APIs.Math;
using OpenCAD.APIs.Math;

namespace OpenCAD.APIs.Measures
{
    public abstract class Unit
    {
        static Unit()
        {
            //Multiplication Operators
            Func<DerivedUnit, DerivedUnit, DerivedUnit> multiplyDerivedUnits = (a, b) =>
            {
                var expression = new DerivedUnitExpression(a.Expression.Members
                    .Concat(b.Expression.Members).ToArray());
                return new DerivedUnit(expression);
            };

            Func<BaseUnit, BaseUnit, DerivedUnit> multiplyBaseUnits = (a, b) =>
            {
                var derivedA = new DerivedUnit(a, 1);
                var derivedB = new DerivedUnit(b, 1);
                return multiplyDerivedUnits(derivedA, derivedB);
            };

            Func<BaseUnit, DerivedUnit, DerivedUnit> multiplyBaseByDerivedUnits = (a, b) =>
            {
                var derivedA = new DerivedUnit(a, 1);
                return multiplyDerivedUnits(derivedA, b);
            };

            Func<DerivedUnit, BaseUnit, DerivedUnit> multiplyDerivedByBaseUnits = (a, b) =>
            {
                var derivedB = new DerivedUnit(b, 1);
                return multiplyDerivedUnits(a, derivedB);
            };

            Func<BaseUnit, MetricPrefix, DerivedUnit> multiplyBaseUnitByPrefix = (a, b) => new DerivedUnit(a, b, 1);

            Func<MetricPrefix, BaseUnit, DerivedUnit> multiplyPrefixByBaseUnit = (a, b) =>
                multiplyBaseUnitByPrefix(b, a);

            //Exponentiation Operators
            Func<BaseUnit, double, DerivedUnit> exponentiateBaseUnit = (a, b) => new DerivedUnit(a, b);

            Func<DerivedUnit, double, DerivedUnit> exponentiateDerivedUnit = (a, b) =>
            {
                var members = a.Expression.Members.Select(m => new DerivedUnitExpressionMember(m.BaseUnit, m.Prefix,
                    m.Exponent * b)).ToArray();
                var expression = new DerivedUnitExpression(members);
                return new DerivedUnit(expression);
            };

            MathOperationManager.RegisterMany(new MathOperation[] {
                new Multiplication<BaseUnit, BaseUnit, DerivedUnit>(multiplyBaseUnits),
                new Multiplication<DerivedUnit, DerivedUnit, DerivedUnit>(multiplyDerivedUnits),
                new Multiplication<BaseUnit, DerivedUnit, DerivedUnit>(multiplyBaseByDerivedUnits),
                new Multiplication<DerivedUnit, BaseUnit, DerivedUnit>(multiplyDerivedByBaseUnits),
                new Multiplication<BaseUnit, MetricPrefix, DerivedUnit>(multiplyBaseUnitByPrefix),
                new Multiplication<MetricPrefix, BaseUnit, DerivedUnit>(multiplyPrefixByBaseUnit),
                new Exponentiation<BaseUnit, double, DerivedUnit>(exponentiateBaseUnit),
                new Exponentiation<DerivedUnit, double, DerivedUnit>(exponentiateDerivedUnit)
            });
        }

        public static Unit operator *(Unit a, Unit b) => (Unit)MathAPI::Math.Multiply(a, b);

        public static Unit operator *(MetricPrefix a, Unit b) => (Unit)MathAPI::Math.Multiply(b, a);

        public static Unit operator *(Unit a, MetricPrefix b) => (Unit)MathAPI::Math.Multiply(a, b);

        public static Unit operator /(Unit a, Unit b) => (Unit)MathAPI::Math.Divide(a, b);

        public static Unit operator !(Unit a) => (Unit)MathAPI::Math.Invert<Unit, Unit>(a);

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
    }
}