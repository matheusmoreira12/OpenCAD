using System;

namespace OpenCAD.APIs.Measures
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public sealed class DerivedUnitDimensionMember: IEquatable<DerivedUnitDimensionMember>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public DerivedUnitDimensionMember(Unit baseUnit, double exponent)
        {
            BaseUnit = baseUnit ?? throw new ArgumentNullException(nameof(baseUnit));
            Prefix = null;
            Exponent = exponent;
        }

        public DerivedUnitDimensionMember(Unit baseUnit, MetricPrefix prefix, double exponent)
        {
            BaseUnit = baseUnit ?? throw new ArgumentNullException(nameof(baseUnit));
            Prefix = prefix;
            Exponent = exponent;
        }

        public Unit BaseUnit { get; }

        public MetricPrefix Prefix { get; }

        public double Exponent { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is DerivedUnitDimensionMember))
                return false;
            else
                return ((IEquatable<DerivedUnitDimensionMember>)this).Equals((DerivedUnitDimensionMember)obj);
        }

        bool IEquatable<DerivedUnitDimensionMember>.Equals(DerivedUnitDimensionMember other)
        {
            Func<bool> prefixMatches = () => Prefix?.Equals(other.Prefix) ?? Prefix == other.Prefix;
            Func<bool> baseUnitMatches = () => BaseUnit.Equals(other.BaseUnit);
            return Exponent == other.Exponent && prefixMatches() && baseUnitMatches();
        }
    }
}
