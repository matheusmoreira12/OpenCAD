using System;
using System.Linq;

using MathAPI = OpenCAD.APIs.Math;
using OpenCAD.APIs.Math;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures
{
    public abstract class Unit : IDisposable
    {
        #region Metric System Management
        /// <summary>
        /// Gets all the available units.
        /// </summary>
        /// <returns>The available units.</returns>
        public static IEnumerable<Unit> GetAll() => MetricSystemManager.GetAllUnits();
        #endregion

        #region Math API Integration
        //Multiplication Operators
        private static DerivedUnit multiplyDerivedUnits(DerivedUnit a, DerivedUnit b)
        {
            var expression = new DerivedUnitDimension(a.Dimension.Members
                .Concat(b.Dimension.Members).ToArray());
            return new DerivedUnit(expression);
        }

        private static DerivedUnit multiplyBaseUnits(BaseUnit a, BaseUnit b)
        {
            var derivedA = new DerivedUnit(a, 1);
            var derivedB = new DerivedUnit(b, 1);
            return multiplyDerivedUnits(derivedA, derivedB);
        }

        private static DerivedUnit multiplyBaseUnitByDerivedUnits(BaseUnit a, DerivedUnit b)
        {
            var derivedA = new DerivedUnit(a, 1);
            return multiplyDerivedUnits(derivedA, b);
        }

        private static DerivedUnit multiplyDerivedUnitByBaseUnits(DerivedUnit a, BaseUnit b)
        {
            var derivedB = new DerivedUnit(b, 1);
            return multiplyDerivedUnits(a, derivedB);
        }

        private static DerivedUnit multiplyBaseUnitByPrefix(BaseUnit a, MetricPrefix b)
            => new DerivedUnit(a, b, 1);

        private static DerivedUnit multiplyPrefixByBaseUnit(MetricPrefix a, BaseUnit b)
            => multiplyBaseUnitByPrefix(b, a);

        private static DerivedUnit exponentiateBaseUnit(BaseUnit a, double b)
            => new DerivedUnit(a, b);

        private static DerivedUnit exponentiateDerivedUnit(DerivedUnit a, double b)
        {
            var members = a.Dimension.Members.Select(m => new DerivedUnitDimensionMember(m.BaseUnit, m.Prefix,
                m.Exponent * b)).ToArray();
            var expression = new DerivedUnitDimension(members);
            return new DerivedUnit(expression);
        }
        #endregion

        static Unit()
        {
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

        #region Arithmetic Operators
        public static Unit operator *(Unit a, Unit b) => MathAPI::Math.Multiply<Unit, Unit, Unit>(a, b);

        public static Unit operator *(MetricPrefix a, Unit b)
            => MathAPI::Math.Multiply<Unit, MetricPrefix, Unit>(b, a);

        public static Unit operator *(Unit a, MetricPrefix b)
            => MathAPI::Math.Multiply<Unit, MetricPrefix, Unit>(a, b);

        public static Unit operator /(Unit a, Unit b) => MathAPI::Math.Divide<Unit, Unit, Unit>(a, b);

        public static Unit operator !(Unit a) => MathAPI::Math.Invert<Unit, Unit>(a);
        #endregion

        #region Parsing
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
        public static bool TryParse(string value, out Unit result)
            => tryParseBySymbol(value, out result) || tryParseByUISymbol(value, out result)
            || tryParseByName(value, out result);

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
        #endregion

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
        public abstract MetricSystem MetricSystem { get; }

        public abstract void Dispose();
    }
}