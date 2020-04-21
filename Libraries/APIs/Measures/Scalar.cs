using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MathAPI = OpenCAD.APIs.Math;
using OpenCAD.APIs.Math;
using System.Globalization;
using OpenCAD.APIs.Measures.Conversion;
using OpenCAD.APIs.Measures.Conversion.Exceptions;

namespace OpenCAD.APIs.Measures
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    public struct Scalar : IComparable<Scalar>, IEquatable<Scalar>
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
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

        public static Scalar operator *(Scalar a, double b) => (Scalar)MathAPI::Math.Multiply(a, new Scalar(b));

        public static Scalar operator *(Scalar a, Unit b) => (Scalar)MathAPI::Math.Multiply(a, new Scalar(1, b));

        public static Scalar operator /(Scalar a, Scalar b) => (Scalar)MathAPI::Math.Divide(a, b);

        public static Scalar operator /(Scalar a, Unit b) => (Scalar)MathAPI::Math.Divide(a, new Scalar(1, b));

        public static Scalar operator /(Scalar a, double b) => (Scalar)MathAPI::Math.Divide(a, new Scalar(b));

        public static Scalar operator !(Scalar a) => MathAPI::Math.Invert<Scalar, Scalar>(a);
        #endregion

        #region Comparison Operators
        public static bool operator ==(Scalar a, Scalar b) => a.Equals(b);

        public static bool operator ==(Scalar a, double b) => a.Equals(new Scalar(b));

        public static bool operator ==(Scalar a, Unit b) => a.Unit.Equals(b);

        public static bool operator ==(Scalar a, Quantity b) => a.Unit.Quantity.Equals(b);

        public static bool operator ==(Scalar a, MetricSystem b) => a.Unit.MetricSystem.Equals(b);

        public static bool operator !=(Scalar a, Scalar b) => !a.Equals(b);

        public static bool operator !=(Scalar a, double b) => !a.Equals(new Scalar(b));

        public static bool operator !=(Scalar a, Unit b) => !a.Unit.Equals(b);

        public static bool operator !=(Scalar a, Quantity b) => !a.Unit.Quantity.Equals(b);

        public static bool operator !=(Scalar a, MetricSystem b) => !a.Unit.MetricSystem.Equals(b);

        public static bool operator >(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) > 0;

        public static bool operator <(Scalar a, Scalar b) => (a as IComparable<Scalar>).CompareTo(b) < 0;
        #endregion

        #region Math API Integration
        //Addition Operation
        static Scalar addScalars(Scalar a, Scalar b) => new Scalar(a.Amount + b.ConvertTo(a.Unit).Amount, a.Unit);

        //Negation Operation
        static Scalar negateScalar(Scalar a) => new Scalar(-a.Amount, a.Unit);

        //Multiplication Operation
        static Scalar multiplyScalars(Scalar a, Scalar b)
        {
            Unit unit;
            if (a.Unit is null)
                unit = b.Unit;
            else if (b.Unit is null)
                unit = b.Unit;
            else
                unit = (Unit)MathAPI::Math.Multiply(a.Unit, b.Unit);
            return new Scalar(a.Amount * b.Amount, unit);
        }

        //Exponentiation Operation
        static Scalar exponentiateScalar(Scalar a, double b)
        {
            Unit unit;
            if (a.Unit is null)
                unit = null;
            else
                unit = (Unit)MathAPI::Math.Power(a.Unit, b);
            return new Scalar(System.Math.Pow(a.Amount, b), unit);
        }
        #endregion

        static Scalar()
        {
            MathOperationManager.RegisterMany(new MathOperation[] {
                new Addition<Scalar, Scalar, Scalar>(addScalars),
                new Negation<Scalar, Scalar>(negateScalar),
                new Multiplication<Scalar, Scalar, Scalar>(multiplyScalars),
                new Exponentiation<Scalar, double, Scalar>(exponentiateScalar)
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
        public string ToUIString()
        {
            if (Amount == double.PositiveInfinity)
                return $"+∞{Unit?.UISymbol}";
            else if (Amount == double.NegativeInfinity)
                return $"-∞{Unit?.UISymbol}";
            else if (Amount == double.NaN)
                return $"!!";
            else
                return $"{Amount.ToString(CultureInfo.InvariantCulture)}{Unit?.UISymbol ?? Unit?.Symbol}";
        }

        public Scalar ConvertTo(Unit destUnit, bool fullScale = false)
        {
            if (destUnit == Unit)
                return this;
            {
                var conversion = UnitConversion.Get(Unit, destUnit);
                if (conversion is null)
                    throw new UnitConversionNotSupportedException(Unit, destUnit);
                else
                {
                    double amount;
                    if (fullScale)
                    {
                        var sourceZeroAmount = UnitConversion.GetScaleZero(Unit)
                            .ConvertTo(destUnit).Amount;
                        var destZeroAmount = UnitConversion.GetScaleZero(Unit)
                            .ConvertTo(destUnit).Amount;
                        amount = (Amount - sourceZeroAmount) * conversion.Factor + destZeroAmount;
                    }
                    else
                        amount = Amount * conversion.Factor;
                    return new Scalar(amount, destUnit);
                }
            }
        }

        public double Amount { get; private set; }
        public Unit Unit { get; private set; }

        int IComparable<Scalar>.CompareTo(Scalar other)
        {
            if (other.Unit.Quantity != Unit.Quantity)
                throw new InvalidOperationException("Cannot compare measurements. " +
                    "Measured physical quantities don't match.");

            return Comparer<double>.Default.Compare(Amount, other.ConvertTo(Unit).Amount);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Scalar))
                return false;
            else
                return ((IEquatable<Scalar>)this).Equals((Scalar)obj);
        }

        bool IEquatable<Scalar>.Equals(Scalar other)
        {
            var _self = this;
            Func<bool> unitMatches = () => Utils.NullableEquals(_self.Unit, other.Unit);
            return Amount == other.Amount && unitMatches();
        }
    }
}