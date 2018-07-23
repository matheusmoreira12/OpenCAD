using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IPrefixedUnit<M> where M : IPhysicalQuantity, new()
    {
        IMetricPrefix Prefix { get; }
        IUnit<M> Unit { get; }
    }

    /// <summary>
    /// Represents a metric unit combined with a metric prefix.
    /// </summary>
    /// <typeparam name="M">The physical quantity being measured.</typeparam>
    public sealed class PrefixedUnit<M> : IPrefixedUnit<M> where M : IPhysicalQuantity, new()
    {
        /// <summary>
        /// Gets the original metric unit.
        /// </summary>
        public IUnit<M> Unit { get; private set; }
        /// <summary>
        /// Gets the metric prefix in use.
        /// </summary>
        public IMetricPrefix Prefix { get; private set; }

        /// <summary>
        /// Creates a prefixed metric unit from the specified metric unit and prefix.
        /// </summary>
        /// <param name="unit">The metric unit.</param>
        /// <param name="prefix">The metric prefix.</param>
        public PrefixedUnit(IUnit<M> unit, IMetricPrefix prefix)
        {
            Unit = unit ?? throw new ArgumentNullException("unit");
            Prefix = prefix;
        }
    }
}