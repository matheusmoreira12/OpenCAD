using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OpenCAD.DataTypes
{
    /// <summary>
    /// An expandable enumeration class made to be as compatible as possible with the existing Enum type.
    /// </summary>
    /// <typeparam name="T">The type of the created enumeration class.</typeparam>
    public abstract class ExpandableEnum<T> : IComparable, IConvertible, IFormattable where T : ExpandableEnum<T>, new()
    {
        private static T FromInt32(int value) {
            var allFlags = EnumerateAll();
            var matchingFlag = allFlags.FirstOrDefault(flag => flag.Id == value);
            if (matchingFlag == null)
                return new T() { _Id = value };
            return matchingFlag;
        }

        private static void AssignAllIds()
        {
            var allFlags = EnumerateAll();
            int lastId = -1;
            foreach (var flag in allFlags)
            {
                if (flag._Id != null)
                {
                    lastId = (int)flag._Id;
                    continue;
                }
                flag._Id = lastId++;
            }
        }

        private static void AssignAllNames()
        {
            var flagFields = EnumerateFlagFields();
            foreach (var flagField in flagFields)
            {
                var flagName = flagField.Name;
                var flag = (T)flagField.GetValue(null);
                flag._Name = flagName;
            }
        }

        private static IEnumerable<T> EnumerateAll()
        {
            var flagFields = EnumerateFlagFields();
            var flags = flagFields.Select(field => (T)field.GetValue(null));
            return flags;
        }

        private static IEnumerable<FieldInfo> EnumerateFlagFields()
        {
            var enumType = typeof(T);
            var publicStaticFields = ReflectionUtils.GetPublicStaticFields(enumType);
            var flagFields = publicStaticFields.Where(field => field.FieldType == enumType);
            return flagFields;
        }

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

        public static explicit operator ExpandableEnum<T>(char value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(sbyte value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(byte value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(short value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(ushort value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(int value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(uint value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(long value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(ulong value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(float value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(double value) => FromInt32(Convert.ToInt32(value));

        public static explicit operator ExpandableEnum<T>(decimal value) => FromInt32(Convert.ToInt32(value));

        public static ExpandableEnum<T> operator ~(ExpandableEnum<T> value) => (ExpandableEnum<T>)(~value.Id);

        public static ExpandableEnum<T> operator &(ExpandableEnum<T> a, ExpandableEnum<T> b) => (ExpandableEnum<T>)((int)a & (int)b);

        public static ExpandableEnum<T> operator |(ExpandableEnum<T> a, ExpandableEnum<T> b) => (ExpandableEnum<T>)((int)a | (int)b);

        /// <summary>
        /// Creates a flag with an explicit Id.
        /// </summary>
        /// <param name="id">The explicit Id.</param>
        public ExpandableEnum(int id) => _Id = id;

        /// <summary>
        /// Creates a flag with an implicit Id.
        /// </summary>
        public ExpandableEnum() { }

        public override string ToString() => Name ?? Id.ToString();

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

        public override bool Equals(object obj) => obj.GetHashCode() == GetHashCode();

        public override int GetHashCode() => 2108858624 + Id.GetHashCode();

        private int Id
        {
            get
            {
                if (_Id == null)
                    AssignAllIds();
                return _Id ?? 0;
            }
        }

        private int? _Id = null;

        private string Name
        {
            get
            {
                if (_Name == null)
                    AssignAllNames();

                return _Name;
            }
        }

        private string _Name = null;
    }
}