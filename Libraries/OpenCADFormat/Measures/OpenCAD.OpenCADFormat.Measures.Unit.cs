using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IUnit
    {
        double StandardAmount { get; }
        string Symbol { get; }
        string UISymbol { get; }
        bool IsPrefixable { get; }
    }

    public interface IUnit<M>: IUnit { }

    public class Unit<M>: IUnit<M> where M : IPhysicalQuantity, new()
    {
        public Unit(double standardAmount, string symbol, string uiSymbol = null, bool isPrefixable = true)
        {
            StandardAmount = standardAmount;
            Symbol = symbol;
            UISymbol = uiSymbol ?? symbol;
            IsPrefixable = isPrefixable;
        }

        public Unit(double conversionFactor, IUnit<M> originalUnit, string symbol, string uiSymbol = null,
            bool isPrefixable = true) : this(originalUnit.StandardAmount * conversionFactor, symbol, uiSymbol
                , isPrefixable) { }

        public double StandardAmount { get; private set; }
        public bool IsPrefixable { get; private set; }
        public string Symbol { get; private set; }
        public string UISymbol { get; private set; }
    }
}