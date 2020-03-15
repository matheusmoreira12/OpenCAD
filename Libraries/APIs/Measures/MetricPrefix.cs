using System;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public sealed class MetricPrefix : IEquatable<MetricPrefix>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
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

        public override bool Equals(object obj)
        {
            if (!(obj is MetricPrefix))
                return false;
            else
                return ((IEquatable<MetricPrefix>)this).Equals((MetricPrefix)obj);
        }

        bool IEquatable<MetricPrefix>.Equals(MetricPrefix other)
        {
            if (Utils.VerifyStackOverflow())
                return Name == other.Name;
            else
            {
                bool metricSystemMatches = Utils.NullableEquals(MetricSystem, other?.MetricSystem);
                return Name == other?.Name && metricSystemMatches && Multiplier == other.Multiplier;
            }
        }
    }
}