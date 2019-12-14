using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Unit: IMultipliable<Unit, Unit>, IExponentiable<double, Unit>
    {
        public static Unit operator *(Unit a, Unit b) => Math.Multiply(a, b);

        public static Unit operator *(MetricPrefix a, Unit b) => Math.Multiply(a, b);

        public static Unit operator *(Unit a, MetricPrefix b) => Math.Multiply(a, b);

        public static Unit operator /(Unit a, Unit b) => Math.Divide(a, b);

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

        private static bool tryParseBySymbol(string symbol, out Unit result) {
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

        Unit IMultipliable<Unit, Unit>.Multiply(Unit value) => this.MultiplyImplicit(value);

        Unit IExponentiable<double, Unit>.Exponentiate(double exponent) => this.ExponentiateImplicit(exponent);
    }
}