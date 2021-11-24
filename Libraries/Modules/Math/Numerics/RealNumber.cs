using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace OpenCAD.APIs.Math.Numerics
{
    public struct RealNumber : IComparable, IComparable<RealNumber>, IEquatable<RealNumber>
    {
        public static readonly RealNumber Zero = new RealNumber(0, 0, 0, false);

        public static RealNumber operator *(RealNumber a, RealNumber b) => new RealNumber(a.Mantissa * b.Mantissa, a.Exponent + b.Exponent, a.Precision < b.Precision ? a.Precision : b.Precision, a.IsNegative ^ b.IsNegative);

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
                throw new NotImplementedException();

            return (RealNumber)(decimal)value;
        }

        public static explicit operator RealNumber(double value)
        {
            if (double.IsNaN(value) || double.IsPositiveInfinity(value) || double.IsNegativeInfinity(value))
                throw new NotImplementedException();

            return (RealNumber)(decimal)value;
        }

        public static explicit operator RealNumber(decimal value)
        {
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

            int precision = 1;
            while (value % 1 > 0)
            {
                value *= 10;
                precision++;
            }

            return new RealNumber((BigInteger)value, exponent, precision, isNegative);
        }

        private RealNumber(BigInteger mantissa, BigInteger exponent, BigInteger precision, bool isNegative)
        {
            Mantissa = mantissa;
            Exponent = exponent;
            Precision = precision;
            Flags = new BitArray(new[] { isNegative });
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            string mantissaStr = Mantissa.ToString();
            if (mantissaStr.Length < Precision)
                mantissaStr = mantissaStr.PadLeft((int)Precision, '0');

            int exponent = (int)Exponent;
            if (exponent < 10 && -exponent < 10)
            {
                int decimalPosition = (int)Exponent + 1;
                if (decimalPosition < 1)
                    builder.Append(new string('0', 1 - decimalPosition));

                builder.Append(mantissaStr);

                int precision = (int)Precision;
                if (decimalPosition > Precision - 1)
                    builder.Append(new string('0', decimalPosition - (precision - 1)));

                if (decimalPosition < 1)
                    decimalPosition = 1;

                builder.Insert(decimalPosition, ".");
            }
            else
            {
                builder.Append(mantissaStr);

                builder.Insert(1, ".");

                if (exponent >= 0)
                    builder.Append($"e{exponent}");
                else
                    builder.Append($"e-{-exponent}");
            }

            if (IsNegative)
                builder.Insert(0, "-");

            return builder.ToString();
        }

        public int CompareTo(object obj) => obj is RealNumber ? CompareTo((RealNumber)obj) : throw new InvalidOperationException();

        public int CompareTo(RealNumber other)
        {
            if (IsNegative)
            {
                if (other.IsNegative)
                    return -compareMantissaAndExponent(Mantissa, Exponent, other.Mantissa, other.Exponent);

                return -1;
            }

            if (other.IsNegative)
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
            hashCode = hashCode * -1521134295 + Precision.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<BitArray>.Default.GetHashCode(Flags);
            return hashCode;
        }

        public readonly BigInteger Mantissa;

        public readonly BigInteger Exponent;

        public readonly BigInteger Precision;

        public bool IsNegative => Flags.Get(0);

        private BitArray Flags;
    }
}
