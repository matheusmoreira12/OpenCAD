using System;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    public sealed class BaseUnit : Unit
    {
        public BaseUnit(string name, Quantity quantity, string symbol, string uISymbol = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uISymbol;
        }

        public override Unit Collapse() => this;

        public override string Name { get; }

        public override Quantity Quantity { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }
    }
}