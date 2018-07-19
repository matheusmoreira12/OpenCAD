using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using OpenCAD.OpenCADFormat.DataTypes;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IUnit<M> where M : IPhysicalQuantity, new()
    {
        string Symbol { get; }
        Quantity<M> Quantity { get; }
    }

    public sealed class Unit<M> : IUnit<M> where M : IPhysicalQuantity, new()
    {
        public string Symbol { get; private set; }
        public Quantity<M> Quantity { get; private set; }

        public Unit(string symbol, BigFloat standardAmount)
        {
            Symbol = symbol;
            Quantity = new Quantity<M>(standardAmount);
        }

        public Unit(string symbol, BigFloat conversionFactor, IUnit<M> originalUnit)
        {
            Symbol = symbol;
            Quantity = new Quantity<M>(originalUnit.Quantity.StandardAmount * conversionFactor);
        }
    }

    public sealed class UnitCollection<M> : ReadOnlyCollection<IUnit<M>> where M : IPhysicalQuantity, new()
    {
        public IUnit<M> FindBySymbol(string symbol)
        {
            foreach (var unit in this)
                if (unit.Symbol == symbol)
                    return unit;

            throw new ArgumentOutOfRangeException($"Could not find by symbol. No unit matches the symbol \"{symbol}\".");
        }

        public bool TryFindBySymbol(string symbol, out IUnit<M> result)
        {
            try
            {
                result = FindBySymbol(symbol);
                return true;
            }
            catch
            {
                result = null;
                return false;
            }
        }

        public UnitCollection(IList<IUnit<M>> original) : base(original) { }
        public UnitCollection(IEnumerable<IUnit<M>> original) : base(original.ToList()) { }
    }
}