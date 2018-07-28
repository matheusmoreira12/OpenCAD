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
            + ConvertAmountTo(b.Amount, b.Unit, a.Unit), a.Unit);
        public static Measurement<M> Subtract(Measurement<M> a, Measurement<M> b) => new Measurement<M>(a.Amount
            - ConvertAmountTo(b.Amount, b.Unit, a.Unit), a.Unit);
        public static Measurement<M> Multiply(Measurement<M> a, double b) => new Measurement<M>(a.Amount * b,
            a.Unit);
        public static Measurement<M> Divide(Measurement<M> a, double b) => new Measurement<M>(a.Amount / b,
            a.Unit);
        public static double Divide(Measurement<M> a, Measurement<M> b) => a.Amount
            / ConvertAmountTo(b.Amount, a.Unit, a.Unit);
        public static Measurement<M> Modulus(Measurement<M> a, double b) => new Measurement<M>(a.Amount % b,
            a.Unit);
        public static Measurement<M> Modulus(Measurement<M> a, Measurement<M> b) => new Measurement<M>(a.Amount
            % ConvertAmountTo(b.Amount, a.Unit, a.Unit), a.Unit);

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

            IUnit<M>[] supportedUnits = Utils.GetSupportedUnits<M>().ToArray();

            IUnit<M> unit = supportedUnits.First(u => u.Symbol == symbol);

            return new Measurement<M>(amount, unit);
        }

        public static double ConvertAmountTo(double inAmount, IUnit<M> inUnit, IUnit<M> outUnit)
        {
            return new Measurement<M>(inAmount, inUnit).ConvertTo(outUnit).Amount;
        }

        public Measurement(double amount, IUnit<M> unit, IMetricPrefix prefix)
        {
            if (unit == null)
                throw new ArgumentNullException("unit");
            if (prefix == null)
                throw new ArgumentNullException("prefix");

            Amount = amount;
            Unit = new PrefixedUnit<M>(unit ?? throw new ArgumentNullException("unit"),
                prefix);
        }

        public Measurement(double amount, IUnit<M> unit)
        {
            Amount = amount;
            Unit = unit ?? throw new ArgumentNullException("unit");
        }

        public override string ToString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{Unit.Symbol}";
        public string ToUIString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{Unit.UISymbol}";

        public Measurement<M> ConvertTo(IUnit<M> outUnit) => new Measurement<M>(Utils.ConvertAmount(this
            , outUnit), outUnit);

        public double GetAbsoluteAmount() => Utils.GetAbsoluteAmount<M>(this);

        #region IComparable Interface
        public int CompareTo(Measurement<M> other) => Comparer<double>.Default.Compare(GetAbsoluteAmount(),
            other.GetAbsoluteAmount());
        #endregion

        #region IEquatable Interface
        public bool Equals(Measurement<M> other) => GetAbsoluteAmount() == other.GetAbsoluteAmount();
        #endregion

        public double Amount { get; set; }
        public IUnit<M> Unit { get; private set; }
    }
}