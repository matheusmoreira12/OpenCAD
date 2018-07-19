using System;
using System.Collections.Generic;
using System.Linq;

using System.Numerics;
using System.Reflection;
using System.Collections;

using OpenCAD.OpenCADFormat.DataTypes;
using OpenCAD.OpenCADFormat.DataConversion;
using OpenCAD.OpenCADFormat.DataStrings.Exceptions;

namespace OpenCAD.OpenCADFormat.DataStrings.Encoding
{
    public sealed class Encoder
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
                throw new AttributeExpectedException
                {
                    AttributeTypes = new[] {
                        typeof(FunctionAttribute),
                        typeof(AnyFunctionAttribute),
                        typeof(MainContextAttribute)
                    }
                };
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
            object fieldValue = targetMember.GetValue(target);

            bool fieldIsMarkedAsBinaryLiteral = Attribute.IsDefined(targetMember, typeof(BinaryLiteralAttribute)),
                fieldIsMarkedAsFloatLiteral = Attribute.IsDefined(targetMember, typeof(FloatLiteralAttribute)),
                fieldIsMarkedAsIntegerLiteral = Attribute.IsDefined(targetMember, typeof(IntegerLiteralAttribute)),
                fieldIsMarkedAsStringLiteral = Attribute.IsDefined(targetMember, typeof(StringLiteralAttribute)),
                fieldIsMarkedAsFunction = Attribute.IsDefined(targetMember, typeof(FunctionAttribute));

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
            return new DataStringLiteralString($"{value}");
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