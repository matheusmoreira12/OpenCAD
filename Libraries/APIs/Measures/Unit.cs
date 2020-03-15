using System;
using System.Linq;

using MathAPI = OpenCAD.APIs.Math;
using OpenCAD.APIs.Math;

namespace OpenCAD.APIs.Measures
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public abstract class Unit : IEquatable<Unit>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        static Unit()
        {
            //Multiplication Operators
            Func<DerivedUnit, DerivedUnit, DerivedUnit> multiplyDerivedUnits = (a, b) =>
            {
                var expression = new DerivedUnitDimension(a.Dimension.Members
                    .Concat(b.Dimension.Members).ToArray());
                return new DerivedUnit(expression);
            };

            Func<BaseUnit, BaseUnit, DerivedUnit> multiplyBaseUnits = (a, b) =>
            {
                var derivedA = new DerivedUnit(a, 1);
                var derivedB = new DerivedUnit(b, 1);
                return multiplyDerivedUnits(derivedA, derivedB);
            };

            Func<BaseUnit, DerivedUnit, DerivedUnit> multiplyBaseUnitByDerivedUnits = (a, b) =>
            {
                var derivedA = new DerivedUnit(a, 1);
                return multiplyDerivedUnits(derivedA, b);
            };

            Func<DerivedUnit, BaseUnit, DerivedUnit> multiplyDerivedUnitByBaseUnits = (a, b) =>
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
                var members = a.Dimension.Members.Select(m => new DerivedUnitDimensionMember(m.BaseUnit, m.Prefix,
                    m.Exponent * b)).ToArray();
                var expression = new DerivedUnitDimension(members);
                return new DerivedUnit(expression);
            };

            MathOperationManager.RegisterMany(new MathOperation[] {
                new Multiplication<BaseUnit, BaseUnit, DerivedUnit>(multiplyBaseUnits),
                new Multiplication<DerivedUnit, DerivedUnit, DerivedUnit>(multiplyDerivedUnits),
                new Multiplication<BaseUnit, DerivedUnit, DerivedUnit>(multiplyBaseUnitByDerivedUnits),
                new Multiplication<DerivedUnit, BaseUnit, DerivedUnit>(multiplyDerivedUnitByBaseUnits),
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

        /// <summary>
        /// Parses a unit by its name, symbol or ui symbol.
        /// </summary>
        /// <param name="value">The unit name, symbol or ui symbol.</param>
        /// <returns>The matching unit.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Unit Parse(string value)
        {
            Unit result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        /// <summary>
        /// Tries parsing a unit by its name, symbol or ui symbol.
        /// </summary>
        /// <param name="value">The unit name, symbol or ui symbol.</param>
        /// <param name="result">The matching unit.</param>
        /// <returns>True, if there is a match. False, if there is no match.</returns>
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

        /// <summary>
        /// Gets this unit in its collapsed form.
        /// </summary>
        /// <returns></returns>
        public abstract Unit Collapse();

        /// <summary>
        /// Gets the name of this unit.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the physical quantity measured by this unit.
        /// </summary>
        public abstract Quantity Quantity { get; }

        /// <summary>
        /// Gets the symbol used internally by the application for representing this unit.
        /// </summary>
        public abstract string Symbol { get; }

        /// <summary>
        /// Gets the symbol shown to the user by the UI for representing this unit.
        /// </summary>
        public abstract string UISymbol { get; }

        /// <summary>
        /// Gets the metric system this unit belongs to.
        /// </summary>
        public MetricSystem MetricSystem { get; internal set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Unit))
                return false;
            else
                return ((IEquatable<Unit>)this).Equals((Unit)obj);
        }

        bool IEquatable<Unit>.Equals(Unit other)
        {
            Func<bool> quantityMatches = () => Utils.NullableEquals(Quantity, other?.Quantity);
            Func<bool> metricSystemMatches = () => Utils.NullableEquals(MetricSystem, other?.MetricSystem);
            return Name == other.Name && quantityMatches() && metricSystemMatches();
        }
    }
}