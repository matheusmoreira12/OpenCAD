using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    public sealed class MetricPrefix: IDisposable
    {
        #region Metric System Management
        /// <summary>
        /// Gets all the available metric prefixes.
        /// </summary>
        /// <returns>The available metric prefixes.</returns>
        public static IEnumerable<MetricPrefix> GetAll() => MetricSystemManager.GetAllMetricPrefixes();
        #endregion

        public static MetricPrefix Parse(string value)
        {
            MetricPrefix result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static bool TryParse(string value, out MetricPrefix result)
            => tryParseBySymbol(value, out result) || tryParseByUISymbol(value, out result)
            || tryParseByName(value, out result);

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

        public MetricPrefix(MetricSystem metricSystem, double multiplier,
            string name, string symbol, string uiSymbol)
        {
            MetricSystem = metricSystem;
            Name = name;
            Symbol = symbol;
            UISymbol = uiSymbol ?? symbol;
            Multiplier = multiplier;

            MetricSystem?.AddPrefix(this);
        }

        public MetricPrefix(MetricSystem metricSystem, double multiplier, string name, string symbol)
            : this(metricSystem, multiplier, name, symbol, null)
        { }

        public MetricPrefix(MetricSystem metricSystem, double multiplier, string name)
            : this(metricSystem, multiplier, name, null, null) { }

        public MetricPrefix(double multiplier, string name, string symbol, string uiSymbol)
            : this(null, multiplier, name, symbol, uiSymbol) { }

        public MetricPrefix(double multiplier, string name, string symbol)
            : this(null, multiplier, name, symbol, null) { }

        public MetricPrefix(double multiplier, string name) : this(null, multiplier, name, null, null) { }

        public double Multiplier { get; }

        public string Name { get; }

        public string Symbol { get; }

        public string UISymbol { get; }

        public MetricSystem MetricSystem { get; }

        public void Dispose()
        {
            MetricSystem?.RemovePrefix(this);
        }
    }
}