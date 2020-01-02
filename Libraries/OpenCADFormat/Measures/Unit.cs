using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
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
                new MathOperation<BaseUnit, BaseUnit, DerivedUnit>(multiplyBaseUnits,
                    MathOperationType.Multiplication),
                new MathOperation<DerivedUnit, DerivedUnit, DerivedUnit>(multiplyDerivedUnits,
                    MathOperationType.Multiplication),
                new MathOperation<BaseUnit, DerivedUnit, DerivedUnit>(multiplyBaseByDerivedUnits,
                    MathOperationType.Multiplication),
                new MathOperation<DerivedUnit, BaseUnit, DerivedUnit>(multiplyDerivedByBaseUnits,
                    MathOperationType.Multiplication),
                new MathOperation<BaseUnit, MetricPrefix, DerivedUnit>(multiplyBaseUnitByPrefix,
                    MathOperationType.Multiplication),
                new MathOperation<MetricPrefix, BaseUnit, DerivedUnit>(multiplyPrefixByBaseUnit,
                    MathOperationType.Multiplication),
                new MathOperation<BaseUnit, double, DerivedUnit>(exponentiateBaseUnit,
                    MathOperationType.Exponentiation),
                new MathOperation<DerivedUnit, double, DerivedUnit>(exponentiateDerivedUnit,
                    MathOperationType.Exponentiation)
            });
        }

        public static Unit operator *(Unit a, Unit b) => (Unit)Math.Multiply(a, b);

        public static Unit operator *(MetricPrefix a, Unit b) => (Unit)Math.Multiply(b, a);

        public static Unit operator *(Unit a, MetricPrefix b) => (Unit)Math.Multiply(a, b);

        public static Unit operator /(Unit a, Unit b) => (Unit)Math.Divide(a, b);

        public static Unit operator !(Unit a) => (Unit)Math.Invert<Unit, Unit>(a);

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