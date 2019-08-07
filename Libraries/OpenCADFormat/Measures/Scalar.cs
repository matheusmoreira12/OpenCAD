using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public struct Scalar : IComparable<Scalar>, IEquatable<Scalar>
    {
        public static readonly Scalar Zero = new Scalar(0, null);

        public static Scalar Add(Scalar a, Scalar b) => new Scalar(a.Amount + Utils.ConvertAmount(b, a.Unit), a.Unit);

        public static Scalar Subtract(Scalar a, Scalar b) => new Scalar(a.Amount - Utils.ConvertAmount(b, a.Unit), a.Unit);

        public static Scalar Negate(Scalar a) => new Scalar(-a.Amount, a.Unit);

        public static Scalar Multiply(Scalar a, double b) => new Scalar(a.Amount * b, a.Unit);

        public static Scalar Multiply(Scalar a, Scalar b) => new Scalar(a.Amount * b.Amount, a.Unit * b.Unit);

        public static Scalar Divide(Scalar a, double b) => new Scalar(a.Amount / b, a.Unit);

        public static Scalar Divide(Scalar a, Scalar b) => new Scalar(a.Amount / b.Amount, a.Unit / b.Unit);

        public static Scalar operator +(Scalar a, Scalar b) => Add(a, b);

        public static Scalar operator -(Scalar a, Scalar b) => Subtract(a, b);

        public static Scalar operator -(Scalar a) => Negate(a);

        public static Scalar operator *(Scalar a, double b) => Multiply(a, b);

        public static Scalar operator /(Scalar a, double b) => Divide(a, b);

        public static Scalar operator /(Scalar a, Scalar b) => Divide(a, b);

        public static bool operator ==(Scalar a, Scalar b) => a.Equals(b);

        public static bool operator !=(Scalar a, Scalar b) => !(a == b);

        public static bool operator >(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) > 0;

        public static bool operator <(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) < 0;

        public static Scalar Parse(string value)
        {
            StringScanner scanner = new StringScanner(value);
            StringUtils.ReadDecimalString(scanner, out string amountStr, out bool isFloatingPoint, out bool hasExponent);

            if (!double.TryParse(amountStr, Conventions.STANDARD_NUMBER_STYLE, Conventions.STANDARD_CULTURE, out double amount))
                throw new InvalidOperationException("String does not contain valid amount information.");

            string symbol = value.Remove(0, amountStr.Length);

            Unit unit; MetricPrefix prefix;

            (unit, prefix) = Utils.ParseUnitAndPrefix(symbol);

            return new Scalar(amount, unit, prefix);
        }

        public static bool TryParse(string value, out Scalar result)
        {
            try
            {
                result = Parse(value);

                return true;
            }
            catch
            {
                result = default(Scalar);

                return false;
            }
        }

        public Scalar(double amount, Unit unit, MetricPrefix prefix = null)
        {
            if (unit == null && amount != 0) Unit = unit ?? throw new ArgumentNullException("Cannot create new " +
                "measurement. Units can only be specified as null if the amount is zero.", "unit");

            Unit = unit;
            Amount = amount;
            Prefix = prefix;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Scalar)) throw new ArgumentException($"Type {obj.GetType()} cannot be compared to " +
                $"type Measurement. No conversion exists.", "obj");

            Scalar measurement = (Scalar)obj;

            return measurement != null &&
                   Amount == measurement.Amount &&
                   EqualityComparer<Unit>.Default.Equals(Unit, measurement.Unit) &&
                   EqualityComparer<MetricPrefix>.Default.Equals(Prefix, measurement.Prefix);
        }

        public override int GetHashCode()
        {
            var hashCode = -1262229599;
            hashCode = hashCode * -1521134295 + Amount.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Unit>.Default.GetHashCode(Unit);
            hashCode = hashCode * -1521134295 + EqualityComparer<MetricPrefix>.Default.GetHashCode(Prefix);
            return hashCode;
        }

        public override string ToString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{Prefix?.Symbol}" +
            $"{Unit?.Symbol}";
        public string ToUIString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{Prefix?.UISymbol}" +
            $"{Unit?.UISymbol}";

        public Scalar ConvertToUnit(Unit outUnit)
        {
            if (outUnit.Quantity != Unit.Quantity)
                throw new InvalidOperationException("Cannot convert measurement to the specified unit. " +
                    "Units have mismatched physical quantities.");

            return new Scalar(Utils.ConvertAmount(this, outUnit), outUnit);
        }

        public double GetAbsoluteAmount() => Utils.GetAbsoluteAmount(this);

        public double Amount { get; private set; }
        public Unit Unit { get; private set; }
        public MetricPrefix Prefix { get; private set; }

        int IComparable<Scalar>.CompareTo(Scalar other)
        {
            if (other.Unit.Quantity != Unit.Quantity)
                throw new InvalidOperationException("Cannot compare measurements. Measured physical quantities " +
                    "don't match.");

            return Comparer<double>.Default.Compare(GetAbsoluteAmount(), other.GetAbsoluteAmount());
        }

        bool IEquatable<Scalar>.Equals(Scalar other)
        {
            return Equals(other);
        }
    }
}