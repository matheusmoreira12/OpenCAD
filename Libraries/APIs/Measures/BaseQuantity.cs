using System;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures
{
    public sealed class BaseQuantity : Quantity
    {
        public override string Name { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public BaseQuantity(string name, string symbol,
            string uiSymbol) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol;
            UISymbol = uiSymbol;

            MetricSystemManager.AddQuantity(this);
        }

        public BaseQuantity(string name, string symbol)
            : this(name, symbol, null) { }

        public BaseQuantity(string name)
            : this(name, null, null) { }

        public override Quantity Collapse() => this;

        public override void Dispose()
        {
            MetricSystemManager.RemoveQuantity(this);
        }
    }
}