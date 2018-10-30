using System;
using System.Reflection;

namespace OpenCAD.OpenCADFormat.DataStrings.Exceptions
{
    public class AttributeException : Exception
    {
        public MemberInfo OffendingMember
        {
            get { return (MemberInfo)Data[nameof(OffendingMember)]; }
            set { Data[nameof(OffendingMember)] = value; }
        }

        public AttributeException() { }
        public AttributeException(MemberInfo offendingMember)
        {
            OffendingMember = offendingMember;
        }
        public AttributeException(string message) : base(message) { }
        public AttributeException(string message, MemberInfo offendingMember) : base(message)
        {
            OffendingMember = offendingMember;
        }
    }

    public class AttributeExpectedException : AttributeException
    {
        public Type[] ExpectedAttributeTypes
        {
            get { return (Type[])Data[nameof(ExpectedAttributeTypes)]; }
            set { Data[nameof(ExpectedAttributeTypes)] = value; }
        }

        public AttributeExpectedException() { }
        public AttributeExpectedException(MemberInfo offendingMember)
        {
            OffendingMember = offendingMember;
        }
        public AttributeExpectedException(MemberInfo offendingMember, params Type[] expectedAttributeTypes)
        {
            OffendingMember = offendingMember;
            ExpectedAttributeTypes = expectedAttributeTypes;
        }
        public AttributeExpectedException(string message) : base(message) { }
        public AttributeExpectedException(string message, MemberInfo offendingMember) : base(message)
        {
            OffendingMember = offendingMember;
        }
        public AttributeExpectedException(string message, MemberInfo offendingMember,
            params Type[] expectedAttributeTypes) : base(message)
        {
            OffendingMember = offendingMember;
            ExpectedAttributeTypes = expectedAttributeTypes;
        }
    }

    public class InvalidAttributeContextException : AttributeException
    {
        public Type[] OffendingAttributeTypes
        {
            get { return (Type[])Data[nameof(OffendingAttributeTypes)]; }
            set { Data[nameof(OffendingAttributeTypes)] = value; }
        }

        public override string Message => base.Message ?? "The expected attributes were not specified.";

        public InvalidAttributeContextException() { }
        public InvalidAttributeContextException(MemberInfo offendingMember)
        {
            OffendingMember = offendingMember;
        }
        public InvalidAttributeContextException(MemberInfo offendingMember, params Type[] offendingAttributeTypes)
        {
            OffendingMember = offendingMember;
            OffendingAttributeTypes = offendingAttributeTypes;
        }
        public InvalidAttributeContextException(string message) : base(message) { }
        public InvalidAttributeContextException(string message, MemberInfo offendingMember) : base(message)
        {
            OffendingMember = offendingMember;
        }
        public InvalidAttributeContextException(string message, MemberInfo offendingMember,
            params Type[] offendingAttributeTypes) : base(message)
        {
            OffendingMember = offendingMember;
            OffendingAttributeTypes = offendingAttributeTypes;
        }
    }
}