using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public struct Measurement : IComparable<Measurement>, IEquatable<Measurement>
    {
        public static readonly Measurement Zero = new Measurement(0, null);

        public static Measurement Add(Measurement a, Measurement b) => new Measurement(a.Amount
            + Utils.ConvertAmount(b, a.Unit), a.Unit);
        public static Measurement Subtract(Measurement a, Measurement b) => new Measurement(a.Amount
            - Utils.ConvertAmount(b, a.Unit), a.Unit);
        public static Measurement Negate(Measurement a) => new Measurement(-a.Amount, a.Unit);
        public static Measurement Multiply(Measurement a, double b) => new Measurement(a.Amount * b, a.Unit);
        public static Measurement Divide(Measurement a, double b) => new Measurement(a.Amount / b, a.Unit);
        public static double Divide(Measurement a, Measurement b) => a.Amount / Utils.ConvertAmount(b, a.Unit);
        public static Measurement Remainder(Measurement a, double b) => new Measurement(a.Amount % b, a.Unit);
        public static Measurement Remainder(Measurement a, Measurement b) => new Measurement(a.Amount
            % Utils.ConvertAmount(b, a.Unit), a.Unit);

        public static Measurement operator +(Measurement a, Measurement b) => Add(a, b);
        public static Measurement operator -(Measurement a, Measurement b) => Subtract(a, b);
        public static Measurement operator -(Measurement a) => Negate(a);
        public static Measurement operator *(Measurement a, double b) => Multiply(a, b);
        public static Measurement operator /(Measurement a, double b) => Divide(a, b);
        public static double operator /(Measurement a, Measurement b) => Divide(a, b);
        public static Measurement operator %(Measurement a, double b) => Remainder(a, b);
        public static Measurement operator %(Measurement a, Measurement b) => Remainder(a, b);

        public static bool operator ==(Measurement a, Measurement b) => a.Equals(b);
        public static bool operator !=(Measurement a, Measurement b) => !(a == b);

        public static bool operator >(Measurement a, Measurement b) => (a as IComparable<Measurement>).CompareTo(b) > 0;
        public static bool operator <(Measurement a, Measurement b) => (a as IComparable<Measurement>).CompareTo(b) < 0;

        public static Measurement Parse(string value)
        {
            StringScanner scanner = new StringScanner(value);
            StringUtils.ReadDecimalString(scanner, out string amountStr, out bool isFloatingPoint, out bool hasExponent);

            if (!double.TryParse(amountStr, Conventions.STANDARD_NUMBER_STYLE, Conventions.STANDARD_CULTURE, out double amount))
                throw new InvalidOperationException("String does not contain valid amount information.");

            string symbol = value.Remove(0, amountStr.Length);

            Unit unit; MetricPrefix prefix;

            (unit, prefix) = Utils.ParseUnitAndPrefix(symbol);

            return new Measurement(amount, unit, prefix);
        }

        public static bool TryParse(string value, out Measurement result)
        {
            try
            {
                result = Parse(value);

                return true;
            }
            catch {
                result = default(Measurement);

                return false;
            }
        }

        public Measurement(double amount, Unit unit, MetricPrefix prefix = null)
        {
            if (unit == null && amount != 0) Unit = unit ?? throw new ArgumentNullException("Cannot create new " +
                "measurement. Units can only be specified as null if the amount is zero.", "unit");

            Unit = unit;
            Amount = amount;
            Prefix = prefix;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Measurement)) throw new ArgumentException($"Type {obj.GetType()} cannot be compared to " +
                $"type Measurement. No conversion exists.", "obj");

            Measurement measurement = (Measurement)obj;

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
            $"{Unit.Symbol}";
        public string ToUIString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{Prefix?.UISymbol}" +
            $"{Unit.UISymbol}";

        public Measurement ConvertToUnit(Unit outUnit)
        {
            if (outUnit.Quantity != Unit.Quantity)
                throw new InvalidOperationException("Cannot convert measurement to the specified unit. " +
                    "Units have mismatched physical quantities.");

            return new Measurement(Utils.ConvertAmount(this, outUnit), outUnit);
        }

        public double GetAbsoluteAmount() => Utils.GetAbsoluteAmount(this);

        public double Amount { get; private set; }
        public Unit Unit { get; private set; }
        public MetricPrefix Prefix { get; private set; }

        int IComparable<Measurement>.CompareTo(Measurement other)
        {
            if (other.Unit.Quantity != Unit.Quantity)
                throw new InvalidOperationException("Cannot compare measurements. Measured physical quantities " +
                    "don't match.");

            return Comparer<double>.Default.Compare(GetAbsoluteAmount(), other.GetAbsoluteAmount());
        }

        bool IEquatable<Measurement>.Equals(Measurement other)
        {
            return Equals(other);
        }
    }
}