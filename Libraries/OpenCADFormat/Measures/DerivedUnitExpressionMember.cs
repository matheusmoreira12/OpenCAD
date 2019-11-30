using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class DerivedUnitExpressionMember
    {
        public DerivedUnitExpressionMember(Unit baseUnit, double exponent)
        {
            BaseUnit = baseUnit ?? throw new ArgumentNullException(nameof(baseUnit));
            Prefix = null;
            Exponent = exponent;
        }

        public DerivedUnitExpressionMember(Unit baseUnit, MetricPrefix prefix, double exponent)
        {
            BaseUnit = baseUnit ?? throw new ArgumentNullException(nameof(baseUnit));
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
            Exponent = exponent;
        }

        public Unit BaseUnit { get; }

        public MetricPrefix Prefix { get; }

        public double Exponent { get; }
    }
}
