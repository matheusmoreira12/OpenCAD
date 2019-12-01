using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Quantity
    {
        public static Quantity Parse(string value)
        {
            Quantity result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static bool TryParse(string value, out Quantity result) =>
            tryParseBySymbol(value, out result) || tryParseByUISymbol(value, out result) || tryParseByName(value, out result);

        private static bool tryParseBySymbol(string symbol, out Quantity result)
        {
            result = MetricSystemManager.GetAllQuantities().FirstOrDefault(q => q.Symbol == symbol);
            if (result == null)
                return false;

            return true;
        }

        private static bool tryParseByUISymbol(string uiSymbol, out Quantity result)
        {
            result = MetricSystemManager.GetAllQuantities().FirstOrDefault(q => q.UISymbol == uiSymbol);
            if (result == null)
                return false;

            return true;
        }

        private static bool tryParseByName(string name, out Quantity result)
        {
            result = MetricSystemManager.GetAllQuantities().FirstOrDefault(q => q.Name == name);
            if (result == null)
                return false;

            return true;
        }

        public abstract string Name { get; }

        public abstract string Symbol { get; }

        public abstract string UISymbol { get; }

        public Unit Unit { get; internal set; } = null;

        public MetricSystem MetricSystem { get; internal set; } = null;
    }
}