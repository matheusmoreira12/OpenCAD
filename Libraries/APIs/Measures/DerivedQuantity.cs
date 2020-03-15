using System;
using System.Linq;

namespace OpenCAD.APIs.Measures
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public sealed class DerivedQuantity : Quantity, IEquatable<DerivedQuantity>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public DerivedQuantity(DerivedQuantityDimension dimension)
        {
            _name = null;
            _symbol = null;
            _uiSymbol = null;
            Dimension = dimension ?? throw new ArgumentNullException(nameof(dimension));
            IsNamed = false;
        }

        public DerivedQuantity(string name, string symbol, DerivedQuantityDimension dimension)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = null;
            Dimension = dimension ?? throw new ArgumentNullException(nameof(dimension));
            IsNamed = true;
        }

        public DerivedQuantity(string name, string symbol, string uiSymbol, DerivedQuantityDimension dimension)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = uiSymbol;
            Dimension = dimension;
            IsNamed = true;
        }

        public DerivedQuantity(BaseQuantity baseQuantity, double exponent)
        {
            _name = null;
            _symbol = null;
            _uiSymbol = null;
            Dimension = getDimension(baseQuantity, exponent);
            IsNamed = false;
        }

        public DerivedQuantity(string name, string symbol, BaseQuantity baseQuantity, double exponent)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = null;
            Dimension = getDimension(baseQuantity, exponent);
            IsNamed = true;
        }

        public DerivedQuantity(string name, string symbol, string uiSymbol, BaseQuantity baseQuantity, double exponent)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = uiSymbol;
            Dimension = getDimension(baseQuantity, exponent);
            IsNamed = true;
        }

        private DerivedQuantityDimension getDimension(BaseQuantity baseQuantity, double exponent) =>
            new DerivedQuantityDimension(new DerivedQuantityDimensionMember(baseQuantity ??
                throw new ArgumentNullException(nameof(baseQuantity)), exponent));

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

        public readonly bool IsNamed;

        public override Quantity Collapse()
        {
            var members = Dimension.Members
                .GroupBy(m => m.BaseQuantity.Collapse())
                .Select(g => new DerivedQuantityDimensionMember(g.Key, g.Sum(m => m.Exponent)))
                .Where(m => m.Exponent != 0)
                .ToArray();

            var dimension = new DerivedQuantityDimension(members);
            return new DerivedQuantity(dimension);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DerivedQuantity))
                return false;
            else
                return ((IEquatable<DerivedQuantity>)this).Equals((DerivedQuantity)obj);
        }

        bool IEquatable<DerivedQuantity>.Equals(DerivedQuantity other)
        {
            Func<bool> dimensionMatches = () => Utils.NullableEquals(Dimension, other.Dimension);
            return (this as IEquatable<Quantity>).Equals(other) && dimensionMatches();
        }
    }
}