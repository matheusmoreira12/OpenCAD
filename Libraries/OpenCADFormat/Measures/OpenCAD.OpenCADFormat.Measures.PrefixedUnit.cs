using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public struct PrefixedUnit : IUnit
    {
        public PrefixedUnit(IUnit baseUnit, MetricPrefix prefix)
        {
            if (!baseUnit.IsMetric)
                throw new InvalidOperationException("Unable to create prefixed unit. The specified base unit is" +
                    " not metric.");

            BaseUnit = baseUnit ?? throw new ArgumentNullException("baseUnit");
            Prefix = prefix ?? throw new ArgumentNullException("prefix");

            PhysicalQuantity.AddUnit(this);
        }

        public PhysicalQuantity PhysicalQuantity => BaseUnit.PhysicalQuantity;

        public bool IsMetric => false;

        public string Symbol => $"{Prefix.Symbol}{BaseUnit.Symbol}";

        public string UISymbol => $"{Prefix.UISymbol}{BaseUnit.UISymbol}";

        public IUnit BaseUnit { get; private set; }

        public MetricPrefix Prefix { get; private set; }

        public double StandardAmount => BaseUnit.StandardAmount * Prefix.Multiplier;
    }
}