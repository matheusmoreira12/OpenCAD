using System;

namespace OpenCAD.APIs.Measures
{
    public class DerivedQuantityDimensionMember : IEquatable<DerivedQuantityDimensionMember>
    {
        public DerivedQuantityDimensionMember(Quantity baseQuantity, double exponent)
        {
            BaseQuantity = baseQuantity ?? throw new ArgumentNullException(nameof(baseQuantity));
            Exponent = exponent;
        }

        public Quantity BaseQuantity { get; }

        public double Exponent { get; }

        bool IEquatable<DerivedQuantityDimensionMember>.Equals(DerivedQuantityDimensionMember other)
        {
            return (BaseQuantity as IEquatable<Quantity>).Equals(other.BaseQuantity)
                && Exponent == other.Exponent;
        }
    }
}