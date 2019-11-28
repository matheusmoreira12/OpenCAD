using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class PrefixedUnit : Unit
    {
        internal PrefixedUnit(Unit baseUnit, MetricPrefix prefix)
        {
            BaseUnit = baseUnit ?? throw new ArgumentNullException(nameof(baseUnit));
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
        }

        public Unit BaseUnit { get; }

        public MetricPrefix Prefix { get; }

        public override string Name => $"{Prefix.Name} {BaseUnit.Name}";

        public override Quantity Quantity => BaseUnit.Quantity;

        public override double StandardAmount => Prefix.Multiplier * BaseUnit.StandardAmount;

        public override string Symbol => $"{Prefix.Symbol}{BaseUnit.Symbol}";

        public override string UISymbol => $"{Prefix.UISymbol}{BaseUnit.UISymbol}";

        public override Unit Collapse() => this;

        public override MetricSystem MetricSystem => BaseUnit.MetricSystem;
    }
}