using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class Unit<M> where M : IPhysicalQuantity, new()
    {
        public Unit(double standardAmount, string symbol, string uiSymbol = null)
        {
            Quantity = new Quantity<M>(standardAmount);
            Symbol = symbol;
            UISymbol = uiSymbol ?? symbol;
        }

        public Unit(double conversionFactor, Unit<M> originalUnit, string symbol, string uiSymbol = null) :
            this(originalUnit.Quantity.StandardAmount * conversionFactor, symbol, uiSymbol)
        { }

        public Quantity<M> Quantity { get; private set; }
        public string Symbol { get; private set; }
        public string UISymbol { get; private set; }

        public override string ToString() => Symbol;
        public string ToUIString() => UISymbol;
    }
}