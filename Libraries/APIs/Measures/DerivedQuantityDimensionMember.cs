using System;

namespace OpenCAD.APIs.Measures
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public class DerivedQuantityDimensionMember : IEquatable<DerivedQuantityDimensionMember>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public DerivedQuantityDimensionMember(Quantity baseQuantity, double exponent)
        {
            BaseQuantity = baseQuantity ?? throw new ArgumentNullException(nameof(baseQuantity));
            Exponent = exponent;
        }

        public Quantity BaseQuantity { get; }

        public double Exponent { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is DerivedQuantityDimensionMember))
                return false;
            else
                return ((IEquatable<DerivedQuantityDimensionMember>)this)
                    .Equals((DerivedQuantityDimensionMember)obj);
        }

        bool IEquatable<DerivedQuantityDimensionMember>.Equals(DerivedQuantityDimensionMember other)
        {
            bool baseQuantityMatches = BaseQuantity.Equals(other.BaseQuantity);
            return baseQuantityMatches && Exponent == other.Exponent;
        }
    }
}