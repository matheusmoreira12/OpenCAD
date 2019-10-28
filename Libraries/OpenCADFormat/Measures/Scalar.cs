using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OpenCAD.OpenCADFormat.Measures
{
    public struct Scalar : IComparable<Scalar>, IEquatable<Scalar>
    {
        public static readonly Scalar Zero = new Scalar(0);

        public static readonly Scalar One = new Scalar(1);

        public static Scalar operator +(Scalar a, Scalar b) => ScalarMath.Add(a, b);

        public static Scalar operator -(Scalar a, Scalar b) => ScalarMath.Subtract(a, b);

        public static Scalar operator -(Scalar a) => ScalarMath.Negate(a);

        public static Scalar operator *(Scalar a, double b) => ScalarMath.Multiply(a, b);

        public static Scalar operator /(Scalar a, double b) => ScalarMath.Divide(a, b);

        public static Scalar operator /(Scalar a, Scalar b) => ScalarMath.Divide(a, b);

        public static Scalar operator !(Scalar a) => ScalarMath.Invert(a);

        public static bool operator ==(Scalar a, Scalar b) => a.Equals(b);

        public static bool operator !=(Scalar a, Scalar b) => !(a == b);

        public static bool operator >(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) > 0;

        public static bool operator <(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) < 0;

        public static Scalar Parse(string value)
        {
            string pattern = @"(?<amount>\d+)(?<unit>.*)";

            var matches = Regex.Match(value, pattern);

            if (matches.Success)
            {
                Group amountGroup = matches.Groups["amount"];
                string amountStr = amountGroup.Value;

                double amount;

                if (!double.TryParse(amountStr, out amount))
                    throw new InvalidOperationException("Cannot parse Scalar. Invalid amount information.");

                Group unitGroup = matches.Groups["unit"];
                string unitStr = unitGroup.Value;

                Unit unit = null;

                if (!string.IsNullOrEmpty(unitStr))
                    if (!Unit.TryParse(unitStr, out unit))
                        throw new InvalidOperationException("Cannot parse Scalar. No unit matches the specified string.");

                return new Scalar(amount, unit);
            }
            else
                throw new InvalidOperationException("Cannot parse Scalar. String is in a wrong format.");
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
                result = default;

                return false;
            }
        }

        public Scalar(double amount, Unit unit = null)
        {
            Unit = unit;
            Amount = amount;
        }

        public override string ToString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{Unit?.Symbol}";
        public string ToUIString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{Unit?.UISymbol}";

        public Scalar ConvertToUnit(Unit outUnit)
        {
            if (outUnit.Quantity != Unit.Quantity)
                throw new InvalidOperationException("Cannot convert scalar to the specified unit. " +
                    "Units have mismatched physical quantities.");

            return new Scalar(Utils.ConvertAmount(this, outUnit), outUnit);
        }

        public double GetAbsoluteAmount() => Utils.GetAbsoluteAmount(this);

        public double Amount { get; private set; }
        public Unit Unit { get; private set; }

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