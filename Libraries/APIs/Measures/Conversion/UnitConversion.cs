using System;

namespace OpenCAD.APIs.Measures.UnitConversion
{
    /// <summary>
    /// Defines a conversion between two units. 
    /// The form is Y = A*X + B, where "A" is the multiplier, "B" is a constant.
    /// </summary>
    public class UnitConversion
    {
        /// <summary>
        /// Initializes an instance of the UnitConversion class.
        /// </summary>
        /// <param name="unitA">The source class.</param>
        /// <param name="unitB">The destination class.</param>
        /// <param name="factor">The multiplier.</param>
        public UnitConversion(Unit unitA, Unit unitB, double factor)
        {
            UnitA = unitA ?? throw new ArgumentNullException(nameof(unitA));
            Factor = factor;
        }

        /// <summary>
        /// Gets the source class for this conversion.
        /// </summary>
        public Unit UnitA { get; }

        /// <summary>
        /// Gets the destination class for this conversion.
        /// </summary>
        public Unit UnitB { get; }

        /// <summary>
        /// Gets the multiplier.
        /// </summary>
        public double Factor { get; }
 
        public UnitConversion Invert()
        {
            return new UnitConversion(UnitB, UnitA, 1.0 / Factor);
        }
    }
}
