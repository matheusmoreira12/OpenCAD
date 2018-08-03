using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class PrefixedUnit<M>: IUnit<M> where M : IPhysicalQuantity, new()
    {
        public IUnit<M> BaseUnit { get; private set; }
        public IMetricPrefix Prefix { get; private set; }
        public bool IsPrefixable => false;
        public string Symbol => $"{Prefix.Symbol}{BaseUnit.Symbol}";
        public string UISymbol => $"{Prefix.UISymbol}{BaseUnit.UISymbol}";

        public double StandardAmount => BaseUnit.StandardAmount * Prefix.Multiplier;

        public PrefixedUnit(IUnit<M> baseUnit, IMetricPrefix prefix)
        {
            if (!baseUnit.IsPrefixable)
                throw new InvalidOperationException("Unable to create a prefixed unit from a base unit that does not " +
                    "accept prefixes.");

            BaseUnit = baseUnit ?? throw new ArgumentNullException("baseUnit");
            Prefix = prefix ?? throw new ArgumentNullException("prefix");
        }
    }
}