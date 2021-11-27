using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Text;

namespace OpenCAD.APIs.Math.Numerics
{
    public struct RealNumber : IComparable, IComparable<RealNumber>, IEquatable<RealNumber>, IFormattable
    {
        public static readonly RealNumber Zero = new RealNumber(0, 0, false);

        public static RealNumber operator *(RealNumber a, RealNumber b) => new RealNumber(a.Mantissa * b.Mantissa, a.Exponent + b.Exponent, a.Sign ^ b.Sign);

        public static RealNumber operator /(RealNumber a, RealNumber b) => new RealNumber(a.Mantissa / b.Mantissa, a.Exponent + b.Exponent, a.Sign ^ b.Sign);

        public static RealNumber operator +(RealNumber a, RealNumber b)
        {
            if (a == Zero)
                return b;
            if (b == Zero)
                return a;
            if (a.Exponent == b.Exponent)
                return calculateForEqualExponents();
            return calculateForDifferentExponents();

            RealNumber calculateForEqualExponents()
            {
                BigInteger signedMantissa = getSignedMantissa(a) + getSignedMantissa(b);
                return new RealNumber(BigInteger.Abs(signedMantissa), a.Exponent, signedMantissa < 0);
            }

            RealNumber calculateForDifferentExponents()
            {
                (RealNumber max, RealNumber min) = a > b ? (a, b) : (b, a);
                int exponentDelta = max.Exponent - min.Exponent;
                BigInteger signedMantissa = getSignedMantissa(min) + applyExponentToMantissa(getSignedMantissa(max), exponentDelta);
                return new RealNumber(BigInteger.Abs(signedMantissa), max.Exponent, signedMantissa < 0);
            }

            BigInteger getSignedMantissa(RealNumber number) => number.Mantissa * (number.Sign ? -1 : 1);

            BigInteger applyExponentToMantissa(BigInteger mantissa, BigInteger exponent) => mantissa * BigInteger.Pow(10, (int)exponent);
        }

        public static RealNumber operator -(RealNumber a) =>
            new RealNumber(
                a.Mantissa,
                a.Exponent,
                !a.Sign
            );

        public static RealNumber operator -(RealNumber a, RealNumber b) => a + (-b);

        public static bool operator >(RealNumber a, RealNumber b) => a.CompareTo(b) > 0;

        public static bool operator <(RealNumber a, RealNumber b) => a.CompareTo(b) < 0;

        public static bool operator ==(RealNumber a, RealNumber b) => a.Equals(b);

        public static bool operator !=(RealNumber a, RealNumber b) => a.Equals(b);

        public static explicit operator RealNumber(byte value) => (RealNumber)(decimal)value;

        public static explicit operator RealNumber(long value) => (RealNumber)(decimal)value;

        public static explicit operator RealNumber(short value) => (RealNumber)(decimal)value;

        public static explicit operator RealNumber(int value) => (RealNumber)(decimal)value;

        public static explicit operator RealNumber(float value)
        {
            if (float.IsNaN(value) || float.IsPositiveInfinity(value) || float.IsNegativeInfinity(value))
                throw new InvalidCastException();

            return (RealNumber)(decimal)value;
        }

        public static explicit operator RealNumber(double value)
        {
            if (double.IsNaN(value) || double.IsPositiveInfinity(value) || double.IsNegativeInfinity(value))
                throw new InvalidCastException();

            return (RealNumber)(decimal)value;
        }

        public static explicit operator RealNumber(decimal value)
        {
            int[] decimalBits = decimal.GetBits(value);
            if (value == 0)
                return Zero;
            bool isNegative = value < 0;
            if (isNegative)
                value = -value;
            int exponent = 0;
            while (value < 1)
            {
                exponent--;
                value *= 10;
            }

            while (value >= 10)
            {
                exponent++;
                value /= 10;
            }

            return new RealNumber(
                extractMantissaFromDecimalBits(decimalBits),
                exponent,
                extractSignFromDecimalBits(decimalBits));

            BigInteger extractMantissaFromDecimalBits(int[] decimalBits)
            {
                byte[] mantissaBits = new byte[4 * 3];
                BitConverter.GetBytes(decimalBits[0]).CopyTo(mantissaBits, 0);
                BitConverter.GetBytes(decimalBits[1]).CopyTo(mantissaBits, 4);
                BitConverter.GetBytes(decimalBits[2]).CopyTo(mantissaBits, 8);
                return new BigInteger(mantissaBits);
            }

            bool extractSignFromDecimalBits(int[] decimalBits) => (decimalBits[3] & 1 << 31) != 0;
        }

        private RealNumber(BigInteger mantissa, int exponent, bool sign)
        {
            Mantissa = mantissa;
            Exponent = exponent;
            Flags = new BitArray(new[] { sign });
        }

        public int CompareTo(object obj) => obj is RealNumber ? CompareTo((RealNumber)obj) : throw new InvalidOperationException();

        public int CompareTo(RealNumber other)
        {
            if (Sign)
            {
                if (other.Sign)
                    return -compareMantissaAndExponent(Mantissa, Exponent, other.Mantissa, other.Exponent);
                return -1;
            }
            if (other.Sign)
                return 1;
            return compareMantissaAndExponent(Mantissa, Exponent, other.Mantissa, other.Exponent);

            int compareMantissaAndExponent(BigInteger mantissaA, BigInteger exponentA, BigInteger mantissaB, BigInteger exponentB)
            {
                if (mantissaB == 0 && mantissaA > 0)
                    return 1;
                if (mantissaA == 0 && mantissaB > 0)
                    return -1;
                if (exponentA > exponentB)
                    return 1;
                if (exponentA < exponentB)
                    return -1;
                if (mantissaA > mantissaB)
                    return 1;
                if (mantissaA < mantissaB)
                    return -1;
                return 0;
            }
        }

        public bool Equals(RealNumber other)
        {
            if (other.Exponent != Exponent)
                return false;
            if (other.Mantissa != Mantissa)
                return false;
            return true;
        }

        public override bool Equals(object obj) => obj is RealNumber ? Equals((RealNumber)obj) : throw new ArgumentOutOfRangeException(nameof(obj));

        public override int GetHashCode()
        {
            int hashCode = -1083325826;
            hashCode = hashCode * -1521134295 + Mantissa.GetHashCode();
            hashCode = hashCode * -1521134295 + Exponent.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<BitArray>.Default.GetHashCode(Flags);
            return hashCode;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            NumberFormatInfo formatInfo = NumberFormatInfo.GetInstance(formatProvider);
            if (format.ToUpper().StartsWith("F"))
            {
                bool specifiesDecimalPlaces = format.Length > 1;
                if (!specifiesDecimalPlaces)
                    return convertToDecimalNotation(Mantissa, Exponent, Sign, null);
                if (int.TryParse(format.Substring(1), out var decimalPlaces))
                    return convertToDecimalNotation(Mantissa, Exponent, Sign, decimalPlaces);
            }
            if (format.ToUpper().StartsWith("E"))
            {
                string exponentDesignator = format.Substring(0, 1);
                bool specifiesDecimalPlaces = format.Length > 1;
                if (!specifiesDecimalPlaces)
                    return convertToExponentialNotation(Mantissa, Exponent, Sign, null, exponentDesignator);
                if (int.TryParse(format.Substring(1), out var decimalPlaces))
                    return convertToExponentialNotation(Mantissa, Exponent, Sign, decimalPlaces, exponentDesignator);
            }
            throw new FormatException();

            string convertToDecimalNotation(BigInteger mantissa, int exponent, bool isNegative, int? decimalDigits)
            {
                decimalDigits ??= formatInfo.NumberDecimalDigits;
                StringBuilder builder = new StringBuilder();
                string mantissaStr = mantissa.ToString();
                int decimalPosition = exponent + 1;
                if (decimalPosition < 1)
                    builder.Append(new string('0', 1 - decimalPosition));
                builder.Append(mantissaStr);
                if (decimalPosition > mantissaStr.Length - 1)
                    builder.Append(new string('0', decimalPosition - (mantissaStr.Length - 1)));
                if (decimalPosition < 1)
                    decimalPosition = 1;
                builder.Insert(decimalPosition, formatInfo.NumberDecimalSeparator);
                if (isNegative)
                    builder.Insert(0, formatInfo.NegativeSign);
                return builder.ToString();
            }

            string convertToExponentialNotation(BigInteger mantissa, int exponent, bool isNegative, int? decimalDigits, string exponentDesignator)
            {
                decimalDigits ??= formatInfo.NumberDecimalDigits;
                StringBuilder builder = new StringBuilder();
                string mantissaStr = mantissa.ToString();
                builder.Append(mantissaStr);
                builder.Insert(1, formatInfo.NumberDecimalSeparator);
                if (exponent > 0)
                    builder.Append($"{exponentDesignator}{formatInfo.PositiveSign}{exponent}");
                else if (exponent < 0)
                    builder.Append($"{exponentDesignator}{formatInfo.NegativeSign}{-exponent}");
                if (isNegative)
                    builder.Insert(0, formatInfo.NegativeSign);
                return builder.ToString();
            }
        }

        public override string ToString() => ToString("F", CultureInfo.CurrentCulture);

        public readonly BigInteger Mantissa;

        public readonly int Exponent;

        public bool Sign => Flags.Get(0);

        private BitArray Flags;
    }
}
