using System;

namespace OpenCAD.APIs.Measures
{
    public sealed class BaseUnit : Unit
    {
        public BaseUnit(MetricSystem metricSystem, Quantity quantity, string name, string symbol, string uiSymbol)
        {
            MetricSystem = metricSystem ?? throw new ArgumentNullException(nameof(metricSystem));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uiSymbol;

            MetricSystem?.AddUnit(this);
        }

        public BaseUnit(MetricSystem metricSystem, Quantity quantity, string name, string symbol)
        {
            MetricSystem = metricSystem ?? throw new ArgumentNullException(nameof(metricSystem));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = null;

            MetricSystem?.AddUnit(this);
        }

        public override Unit Collapse() => this;

        public override Quantity Quantity { get; }

        public override MetricSystem MetricSystem { get; }

        public override string Name { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public override void Dispose()
        {
            MetricSystem?.RemoveUnit(this);
        }
    }
}