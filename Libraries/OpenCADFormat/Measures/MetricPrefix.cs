using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class MetricPrefix
    {
        public static MetricPrefix Parse(string value)
        {
            MetricPrefix result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static bool TryParse(string value, out MetricPrefix result) =>
            tryParseBySymbol(value, out result) || tryParseByUISymbol(value, out result) || tryParseByName(value, out result);

        private static bool tryParseBySymbol(string symbol, out MetricPrefix result)
        {
            result = MetricSystemManager.GetAllMetricPrefixes().FirstOrDefault(q => q.Symbol == symbol);
            if (result == null)
                return false;

            return true;
        }

        private static bool tryParseByUISymbol(string uiSymbol, out MetricPrefix result)
        {
            result = MetricSystemManager.GetAllMetricPrefixes().FirstOrDefault(q => q.UISymbol == uiSymbol);
            if (result == null)
                return false;

            return true;
        }

        private static bool tryParseByName(string name, out MetricPrefix result)
        {
            result = MetricSystemManager.GetAllMetricPrefixes().FirstOrDefault(q => q.Name == name);
            if (result == null)
                return false;

            return true;
        }

        public MetricPrefix(string name, double multiplier, string symbol, string uiSymbol = null)
        {
            Name = name;
            Multiplier = multiplier;
            Symbol = symbol;
            UISymbol = uiSymbol ?? symbol;
        }

        public double Multiplier { get; }

        public string Name { get; }

        public string Symbol { get; }

        public string UISymbol { get; }

        public MetricSystem MetricSystem { get; internal set; }
    }
}