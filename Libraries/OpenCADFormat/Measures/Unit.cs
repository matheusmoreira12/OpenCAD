using OpenCAD.OpenCADFormat.Measures.Math;
using OpenCAD.Utils;
using System.Collections.Generic;
using System.Text;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Unit
    {
        public static Unit Parse(string value)
        {
            IEnumerable<Unit> allUnits = Utils.GetSupportedUnits();

            foreach (var unit in allUnits)
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

        public static Unit operator *(Unit a, Unit b) => MathExtension.Multiply(a, b);

        public static Unit operator *(Unit a, MetricPrefix b) => MathExtension.Multiply(a, b);

        public static Unit operator /(Unit a, Unit b) => MathExtension.Divide(a, b);

        public static Unit operator !(Unit a) => MathExtension.Invert(a);

        public abstract Unit Collapse();

        public abstract string Name { get; }

        public abstract Quantity Quantity { get; }

        public abstract string Symbol { get; }

        public abstract string UISymbol { get; }

        public MetricSystem MetricSystem { get; internal set; }
    }
}