using System;

namespace OpenCAD.APIs.Measures
{
    public class DerivedQuantityDimensionMember
    {
        public DerivedQuantityDimensionMember(Quantity baseQuantity, double exponent)
        {
            BaseQuantity = baseQuantity ?? throw new ArgumentNullException(nameof(baseQuantity));
            Exponent = exponent;
        }

        public Quantity BaseQuantity { get; }

        public double Exponent { get; }
    }
}