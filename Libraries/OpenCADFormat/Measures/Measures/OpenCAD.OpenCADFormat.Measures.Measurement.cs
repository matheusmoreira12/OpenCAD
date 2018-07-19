using OpenCAD.Utils;
using System;

using OpenCAD.OpenCADFormat.DataTypes;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IMeasurement<M> where M : IPhysicalQuantity, new()
    {
        BigFloat Amount { get; }
        IPrefixedUnit<M> PrefixedUnit { get; }

        IMeasurement<M> ConvertTo(IUnit<M> outUnit, IMetricPrefix outPrefix = null);
        IMeasurement<M> ConvertTo(IPrefixedUnit<M> outPrefixedUnit);
    }

    public sealed class Measurement<M> : IMeasurement<M> where M : IPhysicalQuantity, new()
    {
        public static Measurement<M> Parse(string s)
        {
            bool isFloatingPoint,
                hasExponent;

            string amountStr = null;

            StringScanner scanner = new StringScanner(s);
            StringUtils.ReadDecimalString(scanner, out amountStr, out isFloatingPoint, out hasExponent);

            string symbol = s.Substring(amountStr.Length, s.Length - amountStr.Length);

            BigFloat amount;

            if (!BigFloat.TryParse(amountStr, Conventions.STANDARD_NUMBER_FORMAT, out amount))
                throw new InvalidOperationException("String does not contain valid amount information.");

            return null;
        }

        public static BigFloat ConvertAmountTo(BigFloat inAmount, IPrefixedUnit<M> inPrefixedUnit, IPrefixedUnit<M> outPrefixedUnit)
        {
            return new Measurement<M>(inAmount, inPrefixedUnit).ConvertTo(outPrefixedUnit).Amount;
        }

        /// <summary>
        /// Returns a string that represents the current measurement.
        /// </summary>
        public override string ToString()
        {
            return $"{Amount}{PrefixedUnit}";
        }

        public BigFloat Amount { get; set; }
        public IPrefixedUnit<M> PrefixedUnit { get; private set; }

        public IMeasurement<M> ConvertTo(IPrefixedUnit<M> outPrefixedUnit)
        {
            return new Measurement<M>(Utils.ConvertAmount(this, outPrefixedUnit), outPrefixedUnit);
        }

        public IMeasurement<M> ConvertTo(IUnit<M> outUnit, IMetricPrefix outPrefix = null)
        {
            return ConvertTo(new PrefixedUnit<M>(outUnit, outPrefix));
        }

        public Measurement(BigFloat amount, IUnit<M> unit, IMetricPrefix prefix = null)
        {
            Amount = amount;
            PrefixedUnit = new PrefixedUnit<M>(unit, prefix);
        }

        public Measurement(BigFloat amount, IPrefixedUnit<M> prefixedUnit)
        {
            Amount = amount;
            PrefixedUnit = prefixedUnit;
        }
    }
}