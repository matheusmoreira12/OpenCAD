using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class DerivedQuantity : Quantity
    {
        public DerivedQuantity(DerivedQuantityDimension dimension)
        {
            _name = null;
            _symbol = null;
            _uiSymbol = null;
            Dimension = Dimension ?? throw new ArgumentNullException(nameof(dimension));
        }

        public DerivedQuantity(string name, string symbol, DerivedQuantityDimension dimension)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = null;
            Dimension = dimension ?? throw new ArgumentNullException(nameof(dimension));
        }

        public DerivedQuantity(string name, string symbol, string uiSymbol, DerivedQuantityDimension dimension)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = uiSymbol ?? throw new ArgumentNullException(nameof(uiSymbol));
            Dimension = dimension;
        }

        public DerivedQuantityDimension Dimension { get; }

        public override string Name => _name;
        private readonly string _name;

        public override string Symbol => _symbol;
        private readonly string _symbol;

        public override string UISymbol => _uiSymbol;
        private readonly string _uiSymbol;
    }
}