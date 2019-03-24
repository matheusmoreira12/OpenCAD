using System;

namespace OpenCAD.OpenCADFormat.DataStrings
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class MainContextAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ParameterSet: Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true)]
    public sealed class FunctionAttribute : Attribute
    {
        public string FunctionName;
        public Type Type;

        public FunctionAttribute(string functionName, Type type)
        {
            FunctionName = functionName;
            Type = type;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class StringLiteralAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class FloatLiteralAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class IntegerLiteralAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class BinaryLiteralAttribute : Attribute
    {
        public DataStringLiteralBinaryRepresentation OriginalRepresentation = DataStringLiteralBinaryRepresentation.Binary;
    }
}