using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Quantity
    {
        public string Name { get; }

        public string Symbol { get; }

        public string UISymbol { get; }

        public Unit Unit { get; internal set; }

        protected Quantity(string name, string symbol, string uiSymbol = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uiSymbol;
        }
    }
}