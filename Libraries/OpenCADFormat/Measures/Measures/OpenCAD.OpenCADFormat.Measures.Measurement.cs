using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IMeasurement<M> where M : IPhysicalQuantity, new()
    {
        double Amount { get; }
        IPrefixedUnit<M> PrefixedUnit { get; }

        IMeasurement<M> ConvertTo(IUnit<M> outUnit, IMetricPrefix outPrefix = null);
        IMeasurement<M> ConvertTo(IPrefixedUnit<M> outPrefixedUnit);
    }

    /// <summary>
    /// Represents a physical measurement in a specific metric unit.
    /// </summary>
    /// <typeparam name="M">The physical quantity being measured.</typeparam>
    public sealed class Measurement<M> : IMeasurement<M> where M : IPhysicalQuantity, new()
    {
        /// <summary>
        /// Parses a string representation into a new measurement.
        /// </summary>
        /// <param name="value">The string representation.</param>
        /// <returns>The new measurement.</returns>
        public static Measurement<M> Parse(string value)
        {
            bool isFloatingPoint,
                hasExponent;

            string amountStr = null;

            StringScanner scanner = new StringScanner(value);
            StringUtils.ReadDecimalString(scanner, out amountStr, out isFloatingPoint, out hasExponent);

            double amount;

            if (!double.TryParse(amountStr, Conventions.STANDARD_NUMBER_STYLE, Conventions.STANDARD_CULTURE, out amount))
                throw new InvalidOperationException("String does not contain valid amount information.");

            string symbol = value.Remove(0, amountStr.Length);

            IPrefixedUnit<M>[] supportedUnits = Utils.GetSupportedPrefixedUnits<M>().ToArray();

            IPrefixedUnit<M> unit = supportedUnits.First(u => $"{u.Prefix?.Symbol}{u.Unit.Symbol}" == symbol);

            return new Measurement<M>(amount, unit);
        }

        /// <summary>
        /// Converts only the amount, from the specified input unit to the specified output unit.
        /// </summary>
        /// <param name="inAmount">The amount of the input unit.</param>
        /// <param name="inPrefixedUnit">The input unit and prefix.</param>
        /// <param name="outPrefixedUnit">The output unit and prefix.</param>
        /// <returns></returns>
        public static double ConvertAmountTo(double inAmount, IPrefixedUnit<M> inPrefixedUnit, IPrefixedUnit<M> outPrefixedUnit)
        {
            return new Measurement<M>(inAmount, inPrefixedUnit).ConvertTo(outPrefixedUnit).Amount;
        }

        /// <summary>
        /// Creates a new measurement from the specified amount, in the specified unit, with the specified prefix.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="unit">The unit.</param>
        /// <param name="prefix">The prefix.</param>
        public Measurement(double amount, IUnit<M> unit, IMetricPrefix prefix = null)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            Amount = amount;
            PrefixedUnit = new PrefixedUnit<M>(unit, prefix);
        }

        /// <summary>
        /// Creates a new measurement from the specified amount, in the specified prefixed unit.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <param name="prefix">The prefixed unit.</param>
        public Measurement(double amount, IPrefixedUnit<M> prefixedUnit)
        {
            Amount = amount;
            PrefixedUnit = prefixedUnit ?? throw new ArgumentNullException("prefixedUnit");
        }

        /// <summary>
        /// Returns a string that represents the current physical measurement.
        /// </summary>
        public override string ToString()
        {
            return $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{PrefixedUnit.Prefix?.Symbol}{PrefixedUnit.Unit.Symbol}";
        }

        /// <summary>
        /// Gets the amount represented by this physical measurement.
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Gets the metric unit in which the amount is being expressed.
        /// </summary>
        public IPrefixedUnit<M> PrefixedUnit { get; private set; }

        /// <summary>
        /// Converts the measurement to the specified prefixed unit.
        /// </summary>
        /// <param name="outPrefixedUnit">The output unit and prefix.</param>
        /// <returns>The new measurement in the specified prefixed unit.</returns>
        public IMeasurement<M> ConvertTo(IPrefixedUnit<M> outPrefixedUnit)
        {
            if (outPrefixedUnit == null)
                throw new ArgumentNullException("outPrefixedUnit");

            return new Measurement<M>(Utils.ConvertAmount(this, outPrefixedUnit), outPrefixedUnit);
        }

        /// <summary>
        /// Converts the measurement to the specified unit, with no prefix.
        /// </summary>
        /// <param name="outPrefixedUnit">The output unit.</param>
        /// <returns>The new measurement in the specified unit.</returns>
        public IMeasurement<M> ConvertTo(IUnit<M> outUnit, IMetricPrefix outPrefix = null)
        {
            if (outUnit == null)
                throw new ArgumentNullException("outUnit");

            return ConvertTo(new PrefixedUnit<M>(outUnit, outPrefix));
        }
    }
}