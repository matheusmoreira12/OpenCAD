using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;

using OpenCAD.OpenCADFormat.DataConversion;

namespace OpenCAD.OpenCADFormat.DataStrings.Encoding
{
    public sealed class Encoder
    {
        private class TypeMarkings
        {
            public TypeMarkings(MemberInfo member)
            {
                IsMainContext = Attribute.IsDefined(member, typeof(MainContextAttribute));
            }

            public bool IsMainContext { get; }
        }

        private class FieldMarkings
        {
            public FieldMarkings(MemberInfo member)
            {
                IsBinaryLiteral = Attribute.IsDefined(member, typeof(BinaryLiteralAttribute));
                IsFloatLiteral = Attribute.IsDefined(member, typeof(FloatLiteralAttribute));
                IsIntegerLiteral = Attribute.IsDefined(member, typeof(IntegerLiteralAttribute));
                IsStringLiteral = Attribute.IsDefined(member, typeof(StringLiteralAttribute));
                IsFunctionItem = Attribute.IsDefined(member, typeof(FunctionAttribute));
            }

            public bool IsBinaryLiteral { get; }
            public bool IsFloatLiteral { get; }
            public bool IsIntegerLiteral { get; }
            public bool IsStringLiteral { get; }
            public bool IsFunctionItem { get; }
        }

        #region Encoding
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
            FieldMarkings targetMemberMarkings = new FieldMarkings(targetMember);

            if (targetMemberMarkings.IsBinaryLiteral)
            {
                BinaryLiteralAttribute fieldBinaryLiteralAttribute = targetMember.GetCustomAttribute<BinaryLiteralAttribute>();

                return encodeAsBinaryLiteral(fieldValue, fieldBinaryLiteralAttribute.OriginalRepresentation);
            }
            else if (targetMemberMarkings.IsFloatLiteral) return encodeAsFloatLiteral(fieldValue);
            else if (targetMemberMarkings.IsIntegerLiteral) return encodeAsIntegerLiteral(fieldValue);
            else if (targetMemberMarkings.IsStringLiteral) return encodeAsStringLiteral(fieldValue);
            else if (targetMemberMarkings.IsFunctionItem) return encodeAsFunction(fieldValue);

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
            if (value is double)
                return new DataStringLiteralFloatingPoint((double)value);

            return null;
        }

        private DataStringLiteralInteger encodeAsIntegerLiteral(object value)
        {
            if (value is int)
                return new DataStringLiteralInteger((int)value);

            return null;
        }

        private DataStringLiteralString encodeAsStringLiteral(object value)
        {
            return new DataStringLiteralString(value?.ToString());
        }

        private DataStringParameterSet encodeAsParameterSet(object target)
        {
            IEnumerable<DataStringItem> parameters = encodeAllFields(target);

            return new DataStringParameterSet(parameters);
        }

        private string resolveFunctionName(Type targetType)
        {
            FunctionAttribute typeFunctionAttribute = targetType.GetCustomAttribute<FunctionAttribute>();

            return typeFunctionAttribute?.FunctionName;
        }

        private DataStringFunction encodeAsFunction(object target)
        {
            string name = resolveFunctionName(target.GetType());

            DataStringItem[] parameters = encodeAllFields(target).ToArray();

            return new DataStringFunction(name, parameters);
        }
        #endregion

        public object Target { get; private set; }

        public DataStringItem Encode()
        {
            return encodeAsMainContext(Target);
        }

        public Encoder(object value)
        {
            Target = value;
        }
    }
}