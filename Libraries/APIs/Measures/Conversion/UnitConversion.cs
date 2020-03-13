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
        /// <param name="a">The multiplier.</param>
        /// <param name="b">The constant.</param>
        public UnitConversion(Unit unitA, Unit unitB, double a, double b)
        {
            UnitA = unitA ?? throw new ArgumentNullException(nameof(unitA));
            UnitB = unitB ?? throw new ArgumentNullException(nameof(unitB));
            A = a;
            B = b;
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
        public double A { get; }

        /// <summary>
        /// Gets the constant.
        /// </summary>
        public double B { get; }

        public UnitConversion Invert()
        {
            return new UnitConversion(UnitB, UnitA, 1.0 / A, B / A);
        }
    }
}
