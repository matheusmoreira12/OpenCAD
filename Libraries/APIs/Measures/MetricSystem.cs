using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    public sealed class MetricSystem : IDisposable
    {
        #region Metric System Management
        /// <summary>
        /// Gets all the available metric systems.
        /// </summary>
        /// <returns>The available metric systems.</returns>
        public static IEnumerable<MetricSystem> GetAll()
            => MetricSystemManager.GetAllMetricSystems();
        #endregion

        public MetricSystem(string name, string fullName, IList<Unit> units, IList<MetricPrefix> prefixes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            this.units = new HashSet<Unit>(units ?? throw new ArgumentNullException(nameof(units)));
            this.prefixes = new HashSet<MetricPrefix>(prefixes ?? throw new ArgumentNullException(nameof(prefixes)));

            MetricSystemManager.AddMetricSystem(this);
        }

        public MetricSystem(string name, IList<Unit> units, IList<MetricPrefix> prefixes)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FullName = null;
            this.units = new HashSet<Unit>(units ?? throw new ArgumentNullException(nameof(units)));
            this.prefixes = new HashSet<MetricPrefix>(prefixes ?? throw new ArgumentNullException(nameof(prefixes)));

            MetricSystemManager.AddMetricSystem(this);
        }

        public MetricSystem(string name, string fullName, IList<Unit> units)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FullName = fullName;
            this.units = new HashSet<Unit>(units ?? throw new ArgumentNullException(nameof(units)));
            prefixes = new HashSet<MetricPrefix>();

            MetricSystemManager.AddMetricSystem(this);
        }

        public MetricSystem(string name, IList<Unit> units)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FullName = null;
            this.units = new HashSet<Unit>(units ?? throw new ArgumentNullException(nameof(units)));
            prefixes = new HashSet<MetricPrefix>();

            MetricSystemManager.AddMetricSystem(this);
        }

        public MetricSystem(string name, string fullName)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            units = new HashSet<Unit>();
            prefixes = new HashSet<MetricPrefix>();

            MetricSystemManager.AddMetricSystem(this);
        }

        public MetricSystem(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            FullName = null;
            units = new HashSet<Unit>();
            prefixes = new HashSet<MetricPrefix>();

            MetricSystemManager.AddMetricSystem(this);
        }

        internal void AddQuantity(Quantity quantity)
            => quantities.Add(quantity);

        internal void RemoveQuantity(Quantity quantity)
            => quantities.Remove(quantity);

        internal void AddUnit(Unit unit) => units.Add(unit);

        internal void RemoveUnit(Unit unit) => units.Remove(unit);

        internal void AddPrefix(MetricPrefix metricPrefix)
            => prefixes.Add(metricPrefix);

        internal void RemovePrefix(MetricPrefix metricPrefix)
            => prefixes.Remove(metricPrefix);

        public static MetricSystem Parse(string value)
        {
            MetricSystem result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static bool TryParse(string value, out MetricSystem result)
            => tryParseByName(value, out result) || tryParseByFullName(value, out result);

        private static bool tryParseByName(string symbol, out MetricSystem result)
        {
            result = MetricSystemManager.GetAllMetricSystems()
                .FirstOrDefault(ms => ms.Name == symbol);
            if (result == default)
                return false;
            return true;
        }

        private static bool tryParseByFullName(string uiSymbol, out MetricSystem result)
        {
            result = MetricSystemManager.GetAllMetricSystems()
                .FirstOrDefault(ms => ms.FullName == uiSymbol);
            if (result == default)
                return false;
            return true;
        }

        public string Name { get; }

        public string FullName { get; }

        public Quantity[] Quantities => quantities.ToArray();
        private HashSet<Quantity> quantities { get; }

        public Unit[] Units => units.ToArray();
        private HashSet<Unit> units { get; }

        public MetricPrefix[] Prefixes => prefixes.ToArray();
        private HashSet<MetricPrefix> prefixes { get; }

        public void Dispose()
        {
            MetricSystemManager.RemoveMetricSystem(this);
        }
    }
}
