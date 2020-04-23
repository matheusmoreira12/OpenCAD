using System;

namespace OpenCAD.APIs.Measures
{
    public sealed class BaseUnit : Unit
    {
        public BaseUnit(Quantity quantity, string name, string symbol, string uiSymbol)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uiSymbol;

            MetricSystem?.AddUnit(this);
        }

        public BaseUnit(Quantity quantity, string name, string symbol) : this(quantity, name, symbol, null) { }

        public override Unit Collapse() => this;

        public override string Name { get; }

        public override Quantity Quantity { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public override void Dispose()
        {
            MetricSystem?.RemoveUnit(this);
        }
    }
}