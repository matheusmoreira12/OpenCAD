using OpenCAD.Utils;
using System.Collections.Generic;
using System.Text;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Unit
    {
        public static BaseUnit Derive(string name, Unit original, double conversionAmount, string symbol, string uiSymbol = null) => 
            new BaseUnit(name, original.Quantity, original.StandardAmount * conversionAmount, symbol, uiSymbol);

        public static Unit Parse(string value)
        {
            IEnumerable<Unit> supportedUnits = Utils.GetSupportedUnits();

            foreach (var unit in supportedUnits)
                if (unit.Symbol == value)
                    return unit;

            throw new KeyNotFoundException("Unable to parse unit. The provided unit/prefix found no matches.");
        }

        public static bool TryParse(string value, out Unit result)
        {
            try
            {
                result = Parse(value);

                return true;
            }
            catch
            {
                result = default;

                return false;
            }
        }

        public Unit()
        {
            Units.SupportedUnits.Add(this);
        }

        public static Unit operator *(Unit a, Unit b) => UnitMath.Multiply(a, b);

        public static Unit operator *(Unit a, MetricPrefix b) => UnitMath.Multiply(a, b);

        public static Unit operator /(Unit a, Unit b) => UnitMath.Divide(a, b);

        public static Unit operator !(Unit a) => UnitMath.Invert(a);

        public abstract Unit Collapse();

        public abstract string Name { get; }

        public abstract Quantity Quantity { get; }

        public abstract double StandardAmount { get; }

        public abstract string Symbol { get; }

        public abstract string UISymbol { get; }

        public abstract MetricSystem MetricSystem { get; }

        public Unit Derive(string name, double conversionAmount, string symbol, string uiSymbol = null) =>
            Derive(name, this, conversionAmount, symbol, uiSymbol);
    }
}