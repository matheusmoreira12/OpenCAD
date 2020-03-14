using System;

namespace OpenCAD.APIs.Measures
{
    public sealed class DerivedUnitDimensionMember: IEquatable<DerivedUnitDimensionMember>
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

        bool IEquatable<DerivedUnitDimensionMember>.Equals(DerivedUnitDimensionMember other)
        {
            return Exponent == other.Exponent && (Prefix as IEquatable<MetricPrefix>)
                .Equals(other.Prefix) && (BaseUnit as IEquatable<Unit>).Equals(other.BaseUnit);
        }
    }
}
