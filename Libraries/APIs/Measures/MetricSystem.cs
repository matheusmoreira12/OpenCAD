using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.APIs.Measures
{
    public sealed class MetricSystem
    {
        public MetricSystem(string name, IList<Quantity> quantities, IList<Unit> units, 
            IList<MetricPrefix> prefixes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantities = (quantities ?? throw new ArgumentNullException(nameof(quantities))).ToList();
            Units = (units ?? throw new ArgumentNullException(nameof(units))).ToList();
            Prefixes = (prefixes ?? throw new ArgumentNullException(nameof(prefixes))).ToList();
        }

        public static MetricPrefix Parse(string value)
        {
            MetricPrefix result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static bool TryParse(string value, out MetricPrefix result) => tryParseByName(value, out result)
            || tryParseByFullName(value, out result);

        private static bool tryParseByName(string symbol, out MetricPrefix result)
        {
            result = MetricSystemManager.GetAllMetricPrefixes().FirstOrDefault(u => u.Symbol == symbol);
            if (result == default)
                return false;
            return true;
        }

        private static bool tryParseByFullName(string uiSymbol, out MetricPrefix result)
        {
            result = MetricSystemManager.GetAllMetricPrefixes().FirstOrDefault(u => u.UISymbol == uiSymbol);
            if (result == default)
                return false;
            return true;
        }

        public string Name { get; }

        public string FullName { get; }

        public List<Quantity> Quantities { get; }

        public List<Unit> Units { get; }

        public List<MetricPrefix> Prefixes { get; }
    }
}
