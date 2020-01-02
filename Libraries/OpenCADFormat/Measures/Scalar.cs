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

        public static explicit operator Scalar(double value) => new Scalar(value);

        public static Scalar operator +(Scalar a, Scalar b) => (Scalar)Math.Add(a, b);

        public static Scalar operator -(Scalar a, Scalar b) => (Scalar)Math.Subtract(a, b);

        public static Scalar operator -(Scalar a) => (Scalar)Math.Negate(a);

        public static Scalar operator *(Scalar a, Scalar b) => (Scalar)Math.Multiply(a, b);

        public static Scalar operator /(Scalar a, Scalar b) => (Scalar)Math.Divide(a, b);

        public static Scalar operator ^(Scalar a, double b) => (Scalar)Math.Power(a, b);

        public static Scalar operator !(Scalar a) => (Scalar)Math.Invert<Scalar, Scalar>(a);

        public static bool operator ==(Scalar a, Scalar b) => a.Equals(b);

        public static bool operator !=(Scalar a, Scalar b) => !(a == b);

        public static bool operator >(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) > 0;

        public static bool operator <(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) < 0;

        static Scalar()
        {
            //Addition Operator
            Func<Scalar, Scalar, Scalar> addScalars = (a, b) =>
                new Scalar(a.Amount + b.ConvertTo(a.Unit).Amount, a.Unit);

            //Negation Operator
            Func<Scalar, Scalar> negateScalar = (a) => new Scalar(-a.Amount, a.Unit);

            //Multiplication Operator
            Func<Scalar, Scalar, Scalar> multiplyScalars = (a, b) =>
                new Scalar(a.Amount * b.Amount, (Unit)Math.Multiply(a, b));

            //Exponentiation Operator
            Func<Scalar, double, Scalar> exponetiateScalar = (a, b) =>
                new Scalar(System.Math.Pow(a.Amount, b), (Unit)Math.Power(a.Unit, b));

            MathOperationManager.RegisterMany(new MathOperation[] {
                new MathOperation<Scalar, Scalar, Scalar>(addScalars, MathOperationType.Addition),
                new MathOperation<Scalar, Scalar>(negateScalar, MathOperationType.Negation),
                new MathOperation<Scalar, Scalar, Scalar>(multiplyScalars, MathOperationType.Multiplication),
                new MathOperation<Scalar, double, Scalar>(exponetiateScalar, MathOperationType.Exponentiation)
            });
        }

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
        public string ToUIString() => $"{Amount.ToString(Conventions.STANDARD_CULTURE)}{Unit?.UISymbol ?? Unit?.Symbol}";

        public Scalar ConvertTo(Unit outUnit)
        {
            throw new NotImplementedException();
        }

        public double Amount { get; private set; }
        public Unit Unit { get; private set; }

        int IComparable<Scalar>.CompareTo(Scalar other)
        {
            if (other.Unit.Quantity != Unit.Quantity)
                throw new InvalidOperationException("Cannot compare measurements. Measured physical quantities " +
                    "don't match.");

            return Comparer<double>.Default.Compare(ConvertTo(null).Amount, other.ConvertTo(null).Amount);
        }

        bool IEquatable<Scalar>.Equals(Scalar other)
        {
            return Equals(other);
        }

        public override bool Equals(object obj)
        {
            return obj is Scalar scalar &&
                   Amount == scalar.Amount &&
                   EqualityComparer<Unit>.Default.Equals(Unit, scalar.Unit);
        }

        public override int GetHashCode()
        {
            var hashCode = 1156833016;
            hashCode = hashCode * -1521134295 + Amount.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Unit>.Default.GetHashCode(Unit);
            return hashCode;
        }
    }
}