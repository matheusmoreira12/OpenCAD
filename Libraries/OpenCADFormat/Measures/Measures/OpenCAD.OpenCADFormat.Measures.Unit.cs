using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IUnit<M> where M : IPhysicalQuantity, new()
    {
        Quantity<M> Quantity { get; }
        string Symbol { get; }
        string UISymbol { get; }
    }

    /// <summary>
    /// Represents a metric unit.
    /// </summary>
    /// <typeparam name="M">The physical quantity being measured.</typeparam>
    public sealed class Unit<M> : IUnit<M> where M : IPhysicalQuantity, new()
    {
        /// <summary>
        /// Creates a new metric unit from the specified standard amount.
        /// </summary>
        /// <param name="standardAmount">The standard amount.</param>
        /// <param name="symbol">The symbol used internally.</param>
        /// <param name="uiSymbol">The symbol used in the UI.</param>
        public Unit(double standardAmount, string symbol, string uiSymbol = null)
        {
            Quantity = new Quantity<M>(standardAmount);
            Symbol = symbol;
            UISymbol = uiSymbol ?? symbol;
        }

        /// <summary>
        /// Creates a new metric unit from an existing metric unit, and a conversion factor.
        /// </summary>
        /// <param name="conversionFactor">The conversion factor.</param>
        /// <param name="originalUnit">The original unit.</param>
        /// <param name="symbol">The symbol used internally.</param>
        /// <param name="uiSymbol">The symbol used in the UI.</param>
        public Unit(double conversionFactor, IUnit<M> originalUnit, string symbol, string uiSymbol = null) :
            this(originalUnit.Quantity.StandardAmount * conversionFactor, symbol, uiSymbol)
        { }

        /// <summary>
        /// Gets the quantity represented by this metric unit.
        /// </summary>
        public Quantity<M> Quantity { get; private set; }
        /// <summary>
        /// Gets the symbol used internally.
        /// </summary>
        public string Symbol { get; private set; }
        /// <summary>
        /// Gets the symbol used in the UI.
        /// </summary>
        public string UISymbol { get; private set; }
    }
}