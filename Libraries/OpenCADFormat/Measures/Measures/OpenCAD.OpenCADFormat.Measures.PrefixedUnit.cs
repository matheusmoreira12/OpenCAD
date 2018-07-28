using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class PrefixedUnit<M> where M : IPhysicalQuantity, new()
    {
        public Unit<M> Unit { get; private set; }
        public IMetricPrefix Prefix { get; private set; }

        public PrefixedUnit(Unit<M> unit, IMetricPrefix prefix)
        {
            Unit = unit ?? throw new ArgumentNullException("unit");
            Prefix = prefix;
        }

        public override string ToString() => $"{Prefix}{Unit}";
        public string ToUIString() => $"{Prefix?.ToUIString()}{Unit.ToUIString()}";
    }
}