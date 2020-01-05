using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.APIs.Measures
{
    /// <summary>
    /// Defines a conversion between two units. 
    /// The form is Y = A*X^B + C, where "A" is the multiplier, "B" is the exponent and "C" is a constant.
    /// </summary>
    class UnitConversion
    {
        /// <summary>
        /// Initializes an instance of the UnitConversion class.
        /// </summary>
        /// <param name="unitA">The source class.</param>
        /// <param name="unitB">The destination class.</param>
        /// <param name="a">The multiplier.</param>
        /// <param name="b">The exponent.</param>
        /// <param name="c">The constant.</param>
        public UnitConversion(Unit unitA, Unit unitB, double a, double b, double c)
        {
            UnitA = unitA ?? throw new ArgumentNullException(nameof(unitA));
            UnitB = unitB ?? throw new ArgumentNullException(nameof(unitB));
            A = a;
            B = b;
            C = c;
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
        /// Gets the exponent.
        /// </summary>
        public double B { get; }

        /// <summary>
        /// Gets the constant.
        /// </summary>
        public double C { get; }

        public UnitConversion Invert()
        {
            throw new NotImplementedException();
        }
    }
}
