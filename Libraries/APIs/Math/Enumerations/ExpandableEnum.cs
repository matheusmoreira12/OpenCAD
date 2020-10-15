using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenCAD.APIs.Math.Enumerations
{
    public abstract class ExpandableEnum<T> : IComparable, IConvertible, IFormattable where T : ExpandableEnum<T>, new()
    {
        private static string GetName(ExpandableEnum<T> flag)
        {
            var flagProp = GetFlagProps().FirstOrDefault(prop => prop.GetValue(null) == flag);
            if (flagProp == null)
                throw new InvalidOperationException();
            else
                return flagProp.Name;
        }

        private static IEnumerable<PropertyInfo> GetFlagProps()
        {
            Type enumType = typeof(T);
            var publicStaticGetProps = enumType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.GetProperty);
            return publicStaticGetProps.Where(prop => prop.PropertyType == enumType);
        }

        private static int GenerateId()
        {
            var allFlags = GetAll();
            var lastFlagOrNull = allFlags.LastOrDefault();
            return lastFlagOrNull?.Id + 1 ?? 1;
        }

        private static ExpandableEnum<T> FromInt32(int value)
        {
            var allFlags = GetAll();
            var exactMatchFlag = allFlags.FirstOrDefault(flag => flag.Id == value);
            if (exactMatchFlag == null)
                return new ExpandableEnumUnknown<T>(value);
            else
                return exactMatchFlag;
        }

        public static explicit operator bool(ExpandableEnum<T> value) => Convert.ToBoolean(value);

        public static explicit operator char(ExpandableEnum<T> value) => Convert.ToChar(value);

        public static explicit operator sbyte(ExpandableEnum<T> value) => Convert.ToSByte(value);

        public static explicit operator byte(ExpandableEnum<T> value) => Convert.ToByte(value);

        public static explicit operator short(ExpandableEnum<T> value) => Convert.ToInt16(value);

        public static explicit operator ushort(ExpandableEnum<T> value) => Convert.ToUInt16(value);

        public static explicit operator int(ExpandableEnum<T> value) => Convert.ToInt32(value);

        public static explicit operator uint(ExpandableEnum<T> value) => Convert.ToUInt32(value);

        public static explicit operator long(ExpandableEnum<T> value) => Convert.ToInt64(value);

        public static explicit operator ulong(ExpandableEnum<T> value) => Convert.ToUInt64(value);

        public static explicit operator float(ExpandableEnum<T> value) => Convert.ToSingle(value);

        public static explicit operator double(ExpandableEnum<T> value) => Convert.ToDouble(value);

        public static explicit operator decimal(ExpandableEnum<T> value) => Convert.ToDecimal(value);

        public static explicit operator DateTime(ExpandableEnum<T> value) => Convert.ToDateTime(value);

        public static explicit operator ExpandableEnum<T> (bool value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (char value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (sbyte value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (byte value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (short value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (ushort value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (int value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (uint value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (long value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (ulong value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (float value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (double value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (decimal value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T> (DateTime value) => FromInt32(Convert.ToInt32(value));

        public static ExpandableEnum<T> operator ~(ExpandableEnum<T> value) => (ExpandableEnum<T>)(~(int)value);

        public static ExpandableEnum<T> operator &(ExpandableEnum<T> a, ExpandableEnum<T> b) => (ExpandableEnum<T>)((int)a & (int)b);

        public static ExpandableEnum<T> operator |(ExpandableEnum<T> a, ExpandableEnum<T> b) => (ExpandableEnum<T>)((int)a | (int)b);

        /// <summary>
        /// Gets all the available flags.
        /// </summary>
        /// <returns>All the flags.</returns>
        public static T[] GetAll()
        {
            var flags = GetFlagProps().Select(prop => (T)prop.GetValue(null));
            return flags.ToArray();
        }

        /// <summary>
        /// Creates a flag with an explicit Id.
        /// </summary>
        /// <param name="id">The explicit Id.</param>
        public ExpandableEnum(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Creates a flag with an implicit Id.
        /// </summary>
        public ExpandableEnum()
        {
            Id = GenerateId();
        }

        public override string ToString() => GetName(this);

        /// <summary>
        /// Returns a value indicating if the current value contains the specified flag.
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public bool HasFlag(T flag) => (Id & flag.Id) == flag.Id;

        public int CompareTo(object obj) => Id.CompareTo(((ExpandableEnum<T>)obj).Id);

        public TypeCode GetTypeCode() => Type.GetTypeCode(GetType());

        public bool ToBoolean(IFormatProvider provider) => Convert.ToBoolean(Id, provider);

        public char ToChar(IFormatProvider provider) => Convert.ToChar(Id, provider);

        public sbyte ToSByte(IFormatProvider provider) => Convert.ToSByte(Id, provider);

        public byte ToByte(IFormatProvider provider) => Convert.ToByte(Id, provider);

        public short ToInt16(IFormatProvider provider) => Convert.ToInt16(Id, provider);

        public ushort ToUInt16(IFormatProvider provider) => Convert.ToUInt16(Id, provider);

        public int ToInt32(IFormatProvider provider) => Convert.ToInt32(Id, provider);

        public uint ToUInt32(IFormatProvider provider) => Convert.ToUInt32(Id, provider);

        public long ToInt64(IFormatProvider provider) => Convert.ToInt64(Id, provider);

        public ulong ToUInt64(IFormatProvider provider) => Convert.ToUInt64(Id, provider);

        public float ToSingle(IFormatProvider provider) => Convert.ToSingle(Id, provider);

        public double ToDouble(IFormatProvider provider) => Convert.ToSingle(Id, provider);

        public decimal ToDecimal(IFormatProvider provider) => Convert.ToDecimal(Id, provider);

        public DateTime ToDateTime(IFormatProvider provider) => Convert.ToDateTime(Id, provider);

        public string ToString(IFormatProvider provider) => ToString();

        public object ToType(Type conversionType, IFormatProvider provider) => ToString();

        public string ToString(string format, IFormatProvider formatProvider) => ToString();

        /// <summary>
        /// Gets the ID of this flag.
        /// </summary>
        protected int Id { get; }
    }
}