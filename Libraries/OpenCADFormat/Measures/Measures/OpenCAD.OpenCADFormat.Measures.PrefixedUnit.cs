namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IPrefixedUnit<M> where M : IPhysicalQuantity, new()
    {
        IMetricPrefix Prefix { get; }
        IUnit<M> Unit { get; }
    }

    public sealed class PrefixedUnit<M> : IPrefixedUnit<M> where M : IPhysicalQuantity, new()
    {
        public IUnit<M> Unit { get; private set; }
        public IMetricPrefix Prefix { get; private set; }

        /// <summary>
        /// Returns a string that represents the current prefix and unit.
        /// </summary>
        public override string ToString()
        {
            string prefixSymbol = Prefix == null ? "" : Prefix.Symbol;

            return $"{prefixSymbol}{Unit.Symbol}";
        }

        public PrefixedUnit(IUnit<M> unit, IMetricPrefix prefix)
        {
            Unit = unit;
            Prefix = prefix;
        }
    }
}