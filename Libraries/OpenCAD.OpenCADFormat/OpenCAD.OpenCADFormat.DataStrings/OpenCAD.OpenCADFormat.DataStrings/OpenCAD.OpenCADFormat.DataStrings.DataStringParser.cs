using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenCAD.OpenCADFormat.DataConversion;
using System.Numerics;
using OpenCAD.Utils;
using System.Reflection;
using System.Collections;

namespace OpenCAD.OpenCADFormat.DataStrings.Serialization
{
    public class AttributeException : Exception
    {
        public Type[] AttributeTypes
        {
            get
            {
                return (Type[])Data["AttributeTypes"];
            }
            set
            {
                if (AttributeType != null)
                    throw new InvalidOperationException("Cannot set both AttributeType and AttributeTypes properties.");

                Data["AttributeTypes"] = value;
            }
        }

        public Type AttributeType
        {
            get
            {
                return (Type)Data["AttributeType"];
            }
            set
            {
                if (AttributeTypes != null)
                    throw new InvalidOperationException("Cannot set both AttributeType and AttributeTypes properties.");

                Data["AttributeType"] = value;
            }
        }
        public MemberInfo Member
        {
            get
            {
                return (MemberInfo)Data["Member"];
            }
            set
            {
                Data["Member"] = value;
            }
        }

        public AttributeException() { }
        public AttributeException(string message = "") : base(message) { }
    }

    public class AttributeExpectedException : AttributeException
    {
        public AttributeExpectedException() { }
        public AttributeExpectedException(string message = "") : base(message) { }
    }

    public class InvalidAttributeContextException : AttributeException
    {
        public InvalidAttributeContextException() { }
        public InvalidAttributeContextException(string message = "") : base(message) { }
    }

    internal static class ExtensionMethods
    {
        private static IEnumerable<MemberInfo> getMatchingMember(Type type,
            MemberTypes memberType = MemberTypes.All, Type reflectedType = null, Type declaringType = null,
            params string[] memberNames)
        {
            MemberInfo[] members = type.GetMembers();

            foreach (var member in members)
            {
                bool memberTypeMatches = (memberType & member.MemberType) == member.MemberType,
                    memberNamesMatch = memberNames.Length == 0 || memberNames.Contains(member.Name);

                if (memberTypeMatches && memberNamesMatch)
                    yield return member;
            }
        }

        public static MemberInfo[] GetMatchingMembers(this Type type,
            MemberTypes memberType = MemberTypes.All, Type reflectedType = null, Type declaringType = null,
            params string[] memberNames)
        {
            return getMatchingMember(type, memberType, reflectedType, declaringType, memberNames).ToArray();
        }

        public static object GetValue(this MemberInfo member, object obj)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                PropertyInfo property = (PropertyInfo)member;

                if (property.CanRead)
                    return property.GetMethod.Invoke(obj, new object[0]);
            }
            else if (member.MemberType == MemberTypes.Field)
                return ((FieldInfo)member).GetValue(obj);

            return null;
        }
    }

    internal sealed class Encoder
    {
        #region Encoding
        private DataStringItem encodeRoot(object target)
        {
            Type targetType = target.GetType();

            bool targetTypeIsMarkedAsFunction = Attribute.IsDefined(targetType, typeof(FunctionAttribute)),
                targetTypeIsMarkedAsMainContext = Attribute.IsDefined(targetType, typeof(MainContextAttribute));

            if (targetTypeIsMarkedAsFunction)
                return encodeAsFunction(target, target.GetType());
            else if (targetTypeIsMarkedAsMainContext)
                return encodeAsMainContext(target);
            else
                throw new AttributeExpectedException()
                { AttributeTypes = new[] { typeof(FunctionAttribute), typeof(AnyFunctionAttribute), typeof(MainContextAttribute) } };
        }

        private IEnumerable<DataStringItem> encodeAllFields(object target)
        {
            MemberInfo[] targetMembers = target.GetType().GetMatchingMembers(MemberTypes.Field | MemberTypes.Property);

            foreach (var targetMember in targetMembers)
            {
                DataStringItem encodedField = encodeField(target, targetMember);

                if (encodedField != null)
                    yield return encodedField;
            }
        }

        private DataStringItem encodeField(object target, MemberInfo targetMember)
        {
            bool fieldIsMarkedAsBinaryLiteral = Attribute.IsDefined(targetMember, typeof(BinaryLiteralAttribute)),
                fieldIsMarkedAsFloatLiteral = Attribute.IsDefined(targetMember, typeof(FloatLiteralAttribute)),
                fieldIsMarkedAsIntegerLiteral = Attribute.IsDefined(targetMember, typeof(IntegerLiteralAttribute)),
                fieldIsMarkedAsStringLiteral = Attribute.IsDefined(targetMember, typeof(StringLiteralAttribute)),
                fieldIsMarkedAsFunction = Attribute.IsDefined(targetMember, typeof(FunctionAttribute));

            object fieldValue = targetMember.GetValue(target);

            if (fieldIsMarkedAsBinaryLiteral)
            {
                BinaryLiteralAttribute fieldBinaryLiteralAttribute = targetMember.GetCustomAttribute<BinaryLiteralAttribute>();

                return encodeAsBinaryLiteral(fieldValue, fieldBinaryLiteralAttribute.OriginalRepresentation);
            }
            else if (fieldIsMarkedAsFloatLiteral)
                return encodeAsFloatLiteral(fieldValue);
            else if (fieldIsMarkedAsIntegerLiteral)
                return encodeAsIntegerLiteral(fieldValue);
            else if (fieldIsMarkedAsStringLiteral)
                return encodeAsStringLiteral(fieldValue);
            else if (fieldIsMarkedAsFunction)
            {
                FunctionItemAttribute fieldFunctionAttribute = targetMember.GetCustomAttribute<FunctionItemAttribute>();

                return encodeAsFunction(fieldValue, fieldFunctionAttribute.TargetType);
            }

            return null;
        }

        private DataStringMainContext encodeAsMainContext(object target)
        {
            DataStringItem[] mainContextItems = encodeAllFields(target).ToArray();
            return new DataStringMainContext(mainContextItems);
        }

        private DataStringLiteralBinary encodeAsBinaryLiteral(object value, DataStringLiteralBinaryRepresentation originalRepr)
        {
            BitArray fieldAsBitArray = null;

            if (ObjectConversion.TryConvertTo(value, out fieldAsBitArray))
                return new DataStringLiteralBinary(fieldAsBitArray, originalRepr);

            return null;
        }

        private DataStringLiteralFloatingPoint encodeAsFloatLiteral(object value)
        {
            BigFloat fieldAsBigFloat;

            if (ObjectConversion.TryConvertTo(value, out fieldAsBigFloat))
                return new DataStringLiteralFloatingPoint(fieldAsBigFloat);

            return null;
        }

        private DataStringLiteralInteger encodeAsIntegerLiteral(object value)
        {
            BigInteger? fieldAsBigInteger;

            if (ObjectConversion.TryConvertTo(value, out fieldAsBigInteger))
                return new DataStringLiteralInteger((BigInteger)fieldAsBigInteger);

            return null;
        }

        private DataStringLiteralString encodeAsStringLiteral(object value)
        {
            return new DataStringLiteralString(value.ToString());
        }

        private string resolveFunctionName(Type targetType)
        {
            bool typeIsMarkedAsFunction = Attribute.IsDefined(targetType, typeof(FunctionAttribute));

            if (typeIsMarkedAsFunction)
            {
                FunctionAttribute typeFunctionAttribute = targetType.GetCustomAttribute<FunctionAttribute>();

                return typeFunctionAttribute.FunctionName;
            }
            else
                throw new AttributeExpectedException() { AttributeType = typeof(FunctionAttribute), Member = targetType };
        }

        private DataStringFunction encodeAsFunction(object target, Type targetType)
        {
            if (targetType.IsInstanceOfType(target))
            {
                string functionName = resolveFunctionName(target.GetType());

                DataStringItem[] functionParameters = encodeAllFields(target).ToArray();
                return new DataStringFunction(functionName, functionParameters);
            }

            return null;
        }
        #endregion

        public object Target { get; private set; }

        public DataStringItem Encode()
        {
            return encodeRoot(Target);
        }

        public Encoder(object value)
        {
            Target = value;
        }
    }
}