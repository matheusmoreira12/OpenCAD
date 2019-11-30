using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Quantity
    {
        public static bool TryParse(string value, out Quantity result)
        {
            result = parseBySymbol(value) ?? parseByUISymbol(value) ?? parseByName(value);

            if (result == null)
                return false;

            return true;
        }

        public static Quantity Parse(string value)
        {
            Quantity result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        private static Quantity parseBySymbol(string symbol) =>
            MetricSystemManager.GetAllQuantities().FirstOrDefault(q => q.Symbol == symbol);

        private static Quantity parseByUISymbol(string uiSymbol) =>
            MetricSystemManager.GetAllQuantities().FirstOrDefault(q => q.UISymbol == uiSymbol);

        private static Quantity parseByName(string name) =>
            MetricSystemManager.GetAllQuantities().FirstOrDefault(q => q.Name == name);

        public abstract string Name { get; }

        public abstract string Symbol { get; }

        public abstract string UISymbol { get; }

        public Unit Unit { get; internal set; } = null;

        public MetricSystem System { get; internal set; } = null;
    }
}