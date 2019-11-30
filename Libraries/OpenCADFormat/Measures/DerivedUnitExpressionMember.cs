using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class DerivedUnitExpressionMember
    {
        public static implicit operator DerivedUnitExpressionMember(Unit unit) => new DerivedUnitExpressionMember(unit, 1);

        public DerivedUnitExpressionMember(Unit baseUnit, double exponent)
        {
            BaseUnit = baseUnit ?? throw new ArgumentNullException(nameof(baseUnit));
            Exponent = exponent;
        }

        public Unit BaseUnit { get; }

        public double Exponent { get; }
    }
}
