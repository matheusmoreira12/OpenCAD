using System;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
    public sealed class DerivedQuantity : Quantity
    {
        private static DerivedQuantityDimension generateDimensionFromBaseQuantity(BaseQuantity baseQuantity, 
            double exponent) => new DerivedQuantityDimension(new DerivedQuantityDimensionMember(baseQuantity ??
                throw new ArgumentNullException(nameof(baseQuantity)), exponent));

        public DerivedQuantity(DerivedQuantityDimension dimension)
        {
            _name = null;
            _symbol = null;
            _uiSymbol = null;
            Dimension = dimension ?? throw new ArgumentNullException(nameof(dimension));
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
            _uiSymbol = uiSymbol;
            Dimension = dimension;
        }

        public DerivedQuantity(BaseQuantity baseQuantity, double exponent)
        {
            _name = null;
            _symbol = null;
            _uiSymbol = null;
            Dimension = generateDimensionFromBaseQuantity(baseQuantity, exponent);
        }

        public DerivedQuantity(string name, string symbol, BaseQuantity baseQuantity, double exponent)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = null;
            Dimension = generateDimensionFromBaseQuantity(baseQuantity, exponent);
        }

        public DerivedQuantity(string name, string symbol, string uiSymbol, BaseQuantity baseQuantity, double exponent)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = uiSymbol;
            Dimension = generateDimensionFromBaseQuantity(baseQuantity, exponent);
        }

        public DerivedQuantityDimension Dimension { get; }

        public override string Name => _name ?? generateName();
        private string generateName() => null;
        private readonly string _name;

        public override string Symbol => _symbol ?? generateSymbol();
        private string generateSymbol() => null;
        private readonly string _symbol;

        public override string UISymbol => _uiSymbol ?? generateUISymbol();
        private string generateUISymbol() => null;
        private readonly string _uiSymbol;

        public override Quantity Collapse()
        {
            var members = Dimension.Members
                .Select(m => (bu: m.BaseQuantity.Collapse(), e: m.Exponent))
                .Where(mt => mt.bu != null)
                .GroupBy(mt => mt.bu)
                .Select(g => new DerivedQuantityDimensionMember(g.Key, g.Sum(m => m.e)))
                .Where(m => m.Exponent != 0)
                .ToArray();
            DerivedQuantityDimensionMember firstMember;
            if (members.Length == 0)
                return null;
            else if (members.Length == 1 && (firstMember = members.First()).Exponent == 1)
                return firstMember.BaseQuantity;
            else
                return new DerivedQuantity(new DerivedQuantityDimension(members));
        }

        public override void Dispose() { }
    }
}