using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MathAPI = OpenCAD.APIs.Math;
using OpenCAD.APIs.Math;
using System.Globalization;
using OpenCAD.APIs.Measures.UnitConversion;

namespace OpenCAD.APIs.Measures
{
    public struct Scalar : IComparable<Scalar>, IEquatable<Scalar>
    {
        /// <summary>
        /// Gets a dimensionless, unitless Scalar with an amount of 0.
        /// </summary>
        public static readonly Scalar Zero = new Scalar(0);

        /// <summary>
        /// Gets a dimensionless, unitless Scalar with an amount of 1.
        /// </summary>
        public static readonly Scalar One = new Scalar(1);

        /// <summary>
        /// Gets a dimensionless, unitless Scalar with an amount of positive infinity.
        /// </summary>
        public static readonly Scalar PositiveInfinity = new Scalar(double.PositiveInfinity);

        /// <summary>
        /// Gets a dimensionless, unitless Scalar with an amount of positive infinity.
        /// </summary>
        public static readonly Scalar NegativeInfinity = new Scalar(double.NegativeInfinity);

        #region Cast Operators
        public static explicit operator Scalar(int value) => new Scalar(Convert.ToDouble(value));

        public static explicit operator Scalar(short value) => new Scalar(Convert.ToDouble(value));

        public static explicit operator Scalar(long value) => new Scalar(Convert.ToDouble(value));

        public static explicit operator Scalar(float value) => new Scalar(Convert.ToDouble(value));

        public static explicit operator Scalar(double value) => new Scalar(value);

        public static explicit operator Scalar(decimal value) => new Scalar(Convert.ToDouble(value));
        #endregion

        #region Arithmetic Operators
        public static Scalar operator +(Scalar a, Scalar b) => (Scalar)MathAPI::Math.Add(a, b);

        public static Scalar operator -(Scalar a, Scalar b) => (Scalar)MathAPI::Math.Subtract(a, b);

        public static Scalar operator -(Scalar a) => (Scalar)MathAPI::Math.Negate(a);

        public static Scalar operator *(Scalar a, Scalar b) => (Scalar)MathAPI::Math.Multiply(a, b);

        public static Scalar operator *(Scalar a, Unit b) => (Scalar)MathAPI::Math.Multiply(a, new Scalar(1, b));

        public static Scalar operator /(Scalar a, Scalar b) => (Scalar)MathAPI::Math.Divide(a, b);

        public static Scalar operator /(Scalar a, Unit b) => (Scalar)MathAPI::Math.Divide(a, new Scalar(1, b));

        public static Scalar operator ^(Scalar a, int b) => (Scalar)MathAPI::Math.Power(a, Convert.ToDouble(b));

        public static Scalar operator ^(Scalar a, short b) => (Scalar)MathAPI::Math.Power(a, Convert.ToDouble(b));

        public static Scalar operator ^(Scalar a, long b) => (Scalar)MathAPI::Math.Power(a, Convert.ToDouble(b));

        public static Scalar operator ^(Scalar a, float b) => (Scalar)MathAPI::Math.Power(a, Convert.ToDouble(b));

        public static Scalar operator ^(Scalar a, double b) => (Scalar)MathAPI::Math.Power(a, b);

        public static Scalar operator ^(Scalar a, decimal b) => (Scalar)MathAPI::Math.Power(a, Convert.ToDouble(b));

        public static Scalar operator !(Scalar a) => MathAPI::Math.Invert<Scalar, Scalar>(a);
        #endregion

        #region Comparison Operators
        public static bool operator ==(Scalar a, Scalar b) => a.Equals(b);

        public static bool operator ==(Scalar a, Unit b) => a.Unit.Equals(b);

        public static bool operator ==(Scalar a, Quantity b) => a.Unit.Quantity.Equals(b);

        public static bool operator ==(Scalar a, MetricSystem b) => a.Unit.MetricSystem.Equals(b);

        public static bool operator !=(Scalar a, Scalar b) => !a.Equals(b);

        public static bool operator !=(Scalar a, Unit b) => !a.Unit.Equals(b);

        public static bool operator !=(Scalar a, Quantity b) => !a.Unit.Quantity.Equals(b);

        public static bool operator !=(Scalar a, MetricSystem b) => !a.Unit.MetricSystem.Equals(b);

        public static bool operator >(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) > 0;

        public static bool operator <(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) < 0;
        #endregion

        static Scalar()
        {
            //Addition Operator
            Func<Scalar, Scalar, Scalar> addScalars = (a, b) =>
                new Scalar(a.Amount + b.ConvertTo(a.Unit).Amount, a.Unit);

            //Negation Operator
            Func<Scalar, Scalar> negateScalar = (a) => new Scalar(-a.Amount, a.Unit);

            //Multiplication Operator
            Func<Scalar, Scalar, Scalar> multiplyScalars = (a, b) =>
                new Scalar(a.Amount * b.Amount, (Unit)MathAPI::Math.Multiply(a, b));

            //Exponentiation Operator
            Func<Scalar, double, Scalar> exponetiateScalar = (a, b) =>
                new Scalar(System.Math.Pow(a.Amount, b), (Unit)MathAPI::Math.Power(a.Unit, b));

            MathOperationManager.RegisterMany(new MathOperation[] {
                new Addition<Scalar, Scalar, Scalar>(addScalars),
                new Negation<Scalar, Scalar>(negateScalar),
                new Multiplication<Scalar, Scalar, Scalar>(multiplyScalars),
                new Exponentiation<Scalar, double, Scalar>(exponetiateScalar)
            });
        }

        /// <summary>
        /// Parses a scalar from the given string.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <returns>The resulting scalar.</returns>
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

        /// <summary>
        /// Tries parsing a scalar from the given string.
        /// </summary>
        /// <param name="value">The string value to be parsed.</param>
        /// <param name="result">The resulting scalar.</param>
        /// <returns>true if the conversion was successful, false otherwise.</returns>
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

        /// <summary>
        /// Creates a new Scalar from the given amount and unit.
        /// </summary>
        /// <param name="amount">The measured amount.</param>
        /// <param name="unit">The measurement unit.</param>
        public Scalar(double amount, Unit unit = null)
        {
            Unit = unit;
            Amount = amount;
        }

        public override string ToString() => 
            $"{Amount.ToString(CultureInfo.InvariantCulture)}{Unit?.Symbol}";
        public string ToUIString() => 
            $"{Amount.ToString(CultureInfo.InvariantCulture)}{Unit?.UISymbol ?? Unit?.Symbol}";

        public Scalar ConvertTo(Unit destUnit)
        {
            var conversion = UnitConversionManager.GetFor(Unit, destUnit);
            if (conversion != null)
            {
                double amount = Amount * conversion.A;
                return new Scalar(amount, destUnit);
            }
            throw new InvalidUnitConversionException(Unit, destUnit);
        }

        public double Amount { get; private set; }
        public Unit Unit { get; private set; }

        int IComparable<Scalar>.CompareTo(Scalar other)
        {
            if (other.Unit.Quantity != Unit.Quantity)
                throw new InvalidOperationException("Cannot compare measurements. Measured physical quantities " +
                    "don't match.");

            return Comparer<double>.Default.Compare(Amount, other.ConvertTo(Unit).Amount);
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

        public TypeCode GetTypeCode()
        {
            return Type.GetTypeCode(this.GetType());
        }

    }
}