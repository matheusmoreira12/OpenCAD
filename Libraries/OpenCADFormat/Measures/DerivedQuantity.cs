namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class DerivedQuantity : Quantity
    {
        public DerivedQuantity(string name, string symbol, DerivedQuantityDimension dimension) : base(name, symbol)
        {
            Dimension = dimension;
        }

        public DerivedQuantity(string name, string symbol, string uiSymbol, DerivedQuantityDimension dimension) : base(name, symbol, uiSymbol)
        {
            Dimension = dimension;
        }

        public DerivedQuantityDimension Dimension { get; }
    }
}