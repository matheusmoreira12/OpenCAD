using System;
using System.Reflection;

namespace OpenCAD.OpenCADFormat.DataStrings.Exceptions
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
}