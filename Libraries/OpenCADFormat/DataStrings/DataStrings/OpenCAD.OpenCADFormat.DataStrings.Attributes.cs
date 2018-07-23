using System;

namespace OpenCAD.OpenCADFormat.DataStrings
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class MainContextAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class AnyFunctionAttribute : Attribute
    {
        public string[] FunctionNames;

        public AnyFunctionAttribute() { }
        public AnyFunctionAttribute(params string[] functionNames)
        {
            FunctionNames = functionNames;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
    public sealed class FunctionAttribute : Attribute
    {
        public string FunctionName;

        public FunctionAttribute(string functionName)
        {
            FunctionName = functionName;
        }
    }

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class FunctionItemAttribute : Attribute
    {
        public Type TargetType = null;
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