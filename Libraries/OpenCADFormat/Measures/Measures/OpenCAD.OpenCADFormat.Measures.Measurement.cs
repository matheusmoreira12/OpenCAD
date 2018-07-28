using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class Measurement<M> : IComparable<Measurement<M>>, IEquatable<Measurement<M>>
        where M : IPhysicalQuantity, new()
    {
        public static Measurement<M> Add(Measurement<M> a, Measurement<M> b) => new Measurement<M>(a.Amount
            + ConvertAmountTo(b.Amount, b.PrefixedUnit, a.PrefixedUnit), a.PrefixedUnit);
        public static Measurement<M> Subtract(Measurement<M> a, Measurement<M> b) => new Measurement<M>(a.Amount
            - ConvertAmountTo(b.Amount, b.PrefixedUnit, a.PrefixedUnit), a.PrefixedUnit);
        public static Measurement<M> Multiply(Measurement<M> a, double b) => new Measurement<M>(a.Amount * b,
            a.PrefixedUnit);
        public static Measurement<M> Divide(Measurement<M> a, double b) => new Measurement<M>(a.Amount / b,
            a.PrefixedUnit);
        public static double Divide(Measurement<M> a, Measurement<M> b) => a.Amount
            / ConvertAmountTo(b.Amount, a.PrefixedUnit, a.PrefixedUnit);
        public static Measurement<M> Modulus(Measurement<M> a, double b) => new Measurement<M>(a.Amount % b,
            a.PrefixedUnit);
        public static Measurement<M> Modulus(Measurement<M> a, Measurement<M> b) => new Measurement<M>(a.Amount
            % ConvertAmountTo(b.Amount, a.PrefixedUnit, a.PrefixedUnit), a.PrefixedUnit);

        public static double AsDouble(Measurement<M> value) => value.Amount * value.PrefixedUnit.Unit.Quantity
            .StandardAmount;

        public static Measurement<M> operator +(Measurement<M> a, Measurement<M> b) => Add(a, b);
        public static Measurement<M> operator -(Measurement<M> a, Measurement<M> b) => Subtract(a, b);
        public static Measurement<M> operator *(Measurement<M> a, double b) => Multiply(a, b);
        public static Measurement<M> operator /(Measurement<M> a, double b) => Divide(a, b);
        public static double operator /(Measurement<M> a, Measurement<M> b) => Divide(a, b);
        public static Measurement<M> operator %(Measurement<M> a, double b) => Modulus(a, b);
        public static Measurement<M> operator %(Measurement<M> a, Measurement<M> b) => Modulus(a, b);

        public static bool operator >(Measurement<M> a, Measurement<M> b) => a.CompareTo(b) > 0;
        public static bool operator <(Measurement<M> a, Measurement<M> b) => a.CompareTo(b) < 0;

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

            PrefixedUnit<M>[] supportedUnits = Utils.GetSupportedPrefixedUnits<M>().ToArray();

            PrefixedUnit<M> unit = supportedUnits.First(u => $"{u.Prefix?.Symbol}{u.Unit.Symbol}" == symbol);

            return new Measurement<M>(amount, unit);
        }

        public static double ConvertAmountTo(double inAmount, PrefixedUnit<M> inPrefixedUnit, PrefixedUnit<M> outPrefixedUnit)
        {
            return new Measurement<M>(inAmount, inPrefixedUnit).ConvertTo(outPrefixedUnit).Amount;
        }

        public Measurement(double amount, Unit<M> unit, IMetricPrefix prefix = null)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");

            Amount = amount;
            PrefixedUnit = new PrefixedUnit<M>(unit, prefix);
        }

        public Measurement(double amount, PrefixedUnit<M> prefixedUnit)
        {
            Amount = amount;
            PrefixedUnit = prefixedUnit ?? throw new ArgumentNullException("prefixedUnit");
        }

        public override string ToString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{PrefixedUnit}";
        public string ToUIString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{PrefixedUnit?.ToUIString()}";

        public Measurement<M> ConvertTo(PrefixedUnit<M> outPrefixedUnit)
        {
            if (outPrefixedUnit == null)
                throw new ArgumentNullException("outPrefixedUnit");

            return new Measurement<M>(Utils.ConvertAmount(this, outPrefixedUnit), outPrefixedUnit);
        }

        public Measurement<M> ConvertTo(Unit<M> outUnit, IMetricPrefix outPrefix = null)
        {
            if (outUnit == null)
                throw new ArgumentNullException("outUnit");

            return ConvertTo(new PrefixedUnit<M>(outUnit, outPrefix));
        }

        #region IComparable Interface
        public int CompareTo(Measurement<M> other) => Comparer<double>.Default.Compare(AsDouble(this),
            AsDouble(other));
        #endregion

        #region IEquatable Interface
        public bool Equals(Measurement<M> other) => AsDouble(this) == AsDouble(other);
        #endregion

        public double Amount { get; set; }
        public PrefixedUnit<M> PrefixedUnit { get; private set; }
    }
}