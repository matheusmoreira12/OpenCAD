using System;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures
{
    public sealed class BaseQuantity : Quantity
    {
        public override string Name { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public BaseQuantity(string name, string symbol, string uiSymbol = null) {
            Name = name;
            Symbol = symbol;
            UISymbol = uiSymbol;
        }

        public override Quantity Collapse() => this;
    }
}