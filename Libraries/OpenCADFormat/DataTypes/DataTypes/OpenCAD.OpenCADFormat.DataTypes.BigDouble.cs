using System;
using System.Numerics;

namespace OpenCAD.OpenCADFormat.DataTypes
{
    public struct BigDouble : IComparable, IComparable<BigDouble>, IEquatable<BigDouble>
    {
        public static readonly BigDouble One = 1;
        public static readonly BigDouble Zero = 0;
        public static readonly BigDouble MinusOne = -1;
        public static readonly BigDouble OneHalf = 0.5;

        public static implicit operator BigDouble(int value) => new BigDouble(value, 0);

        public static implicit operator BigDouble(double value)
        {
        }

        public static implicit operator BigDouble(decimal value) => (double)value;
        public static implicit operator BigDouble(float value) => (double)value;

        private bool sign;
        private BigInteger mantissa;
        private BigInteger exponent;

        private BigDouble(BigInteger mantissa, BigInteger exponent) : this()
        {
            this.mantissa = mantissa;
            this.exponent = exponent;
        }

        int IComparable.CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        int IComparable<BigDouble>.CompareTo(BigDouble other)
        {
            throw new NotImplementedException();
        }

        bool IEquatable<BigDouble>.Equals(BigDouble other)
        {
            throw new NotImplementedException();
        }
    }
}