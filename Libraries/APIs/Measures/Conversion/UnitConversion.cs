using System.Collections.Generic;

namespace OpenCAD.APIs.Measures.Conversion
{
    public class UnitConversion
    {
        /// <summary>
        /// Defines a conversion factor between two units.
        /// </summary>
        /// <param name="sourceUnit">The conversion source unit.</param>
        /// <param name="targetUnit">The conversion destination unit.</param>
        /// <param name="factor">The conversion factor.</param>
        public static UnitConversion Define(Unit sourceUnit, Unit targetUnit, double factor)
        {
            var conversion = new UnitConversion(sourceUnit, targetUnit, factor);
            UnitConversionManager.Add(conversion);
            return conversion;
        }

        /// <summary>
        /// Defines the scale zero for the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        /// <param name="zero">The scale zero value.</param>
        public static void DefineScaleZero(Unit unit, Scalar zero)
            => UnitConversionManager.DefineScaleZero(unit, zero);

        /// <summary>
        /// Gets the scale zero for the specified unit.
        /// </summary>
        /// <param name="unit">The unit.</param>
        public static Scalar GetScaleZero(Unit unit)
            => UnitConversionManager.GetScaleZero(unit);

        /// <summary>
        /// Gets the easiest conversion between two units.
        /// </summary>
        /// <param name="sourceUnit">The conversion source unit.</param>
        /// <param name="targetUnit">The conversion target unit.</param>
        /// <returns></returns>
        public static UnitConversion Get(Unit sourceUnit, Unit targetUnit)
            => UnitConversionManager.Get(sourceUnit, targetUnit);

        /// <summary>
        /// Gets the direct conversion between two units.
        /// </summary>
        /// <param name="sourceUnit">The conversion source unit.</param>
        /// <param name="targetUnit">The conversion target unit.</param>
        /// <returns></returns>
        public static UnitConversion GetDirect(Unit sourceUnit, Unit targetUnit)
            => UnitConversionManager.GetDirect(sourceUnit, targetUnit);

        /// <summary>
        /// Gets all the possible direct conversions from the specified source unit.
        /// </summary>
        /// <param name="sourceUnit">The conversion source unit.</param>
        /// <returns></returns>
        public static IEnumerable<UnitConversion> GetDirectFrom(Unit sourceUnit)
            => UnitConversionManager.GetDirectFrom(sourceUnit);

        /// <summary>
        /// Gets all the possible direct conversions to the specified target unit.
        /// </summary>
        /// <param name="targetUnit">The conversion target unit.</param>
        /// <returns></returns>
        public static IEnumerable<UnitConversion> GetDirectTo(Unit targetUnit)
            => UnitConversionManager.GetDirectTo(targetUnit);

        internal UnitConversion(Unit sourceUnit, Unit targetUnit, double factor)
        {
            SourceUnit = sourceUnit;
            TargetUnit = targetUnit;
            Factor = factor;
        }

        public override string ToString() => $"{SourceUnit?.UISymbol ?? SourceUnit.Symbol ?? "1"}→" +
            $"{TargetUnit?.UISymbol ?? TargetUnit.Symbol ?? "1"}";

        /// <summary>
        /// Gets the source class for this conversion.
        /// </summary>
        public Unit SourceUnit { get; }

        /// <summary>
        /// Gets the destination class for this conversion.
        /// </summary>
        public Unit TargetUnit { get; }

        /// <summary>
        /// Gets the multiplier.
        /// </summary>
        public double Factor { get; }

        public UnitConversion Invert()
        {
            return new UnitConversion(TargetUnit, SourceUnit, 1.0 / Factor);
        }
    }
}
