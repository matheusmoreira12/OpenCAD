using System;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures
{
    public sealed class BaseQuantity : Quantity
    {
        public override string Name { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public BaseQuantity(MetricSystem metricSystem, string name, string symbol,
            string uiSymbol) {
            MetricSystem = metricSystem;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol;
            UISymbol = uiSymbol;

            MetricSystem?.AddQuantity(this);
        }

        public BaseQuantity(MetricSystem metricSystem, string name, string symbol)
            : this(metricSystem, name, symbol, null) { }

        public BaseQuantity(MetricSystem metricSystem, string name)
            : this(metricSystem, name, null, null) { }

        public BaseQuantity(string name, string symbol, string uiSymbol)
            : this(null, name, symbol, uiSymbol) { }

        public BaseQuantity(string name, string symbol)
            : this(name, symbol, null) { }

        public BaseQuantity(string name)
            : this(name, null, null) { }

        public override Quantity Collapse() => this;

        ~BaseQuantity() {
            MetricSystem?.RemoveQuantity(this);
        }
    }
}