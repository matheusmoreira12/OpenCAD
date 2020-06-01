using System;

namespace OpenCAD.APIs.Measures
{
    public sealed class BaseQuantity : Quantity
    {
        public BaseQuantity(string name, string symbol,
            string uiSymbol)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol;
            UISymbol = uiSymbol;

            MetricSystemManager.AddQuantity(this);
        }

        public BaseQuantity(string name, string symbol)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol;
            UISymbol = null;

            MetricSystemManager.AddQuantity(this);
        }

        public BaseQuantity(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = null;
            UISymbol = null;

            MetricSystemManager.AddQuantity(this);
        }

        public override string Name { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public override Quantity Collapse() => this;

        public override void Dispose()
        {
            MetricSystemManager.RemoveQuantity(this);
        }
    }
}