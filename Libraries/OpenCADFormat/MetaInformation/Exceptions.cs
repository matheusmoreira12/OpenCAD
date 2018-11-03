using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.MetaInformation.Exceptions
{
    [Serializable]
    public abstract class MetaFieldException : Exception
    {
        public string FieldName
        {
            get => (string)Data[nameof(FieldName)];
            protected set => Data[nameof(FieldName)] = value;
        }

        public MetaFieldException(string fieldName = null) => FieldName = fieldName;
        public MetaFieldException(string message, Exception inner) : base(message, inner) { }
        public MetaFieldException(string message, string fieldName = null) : base(message) => FieldName = fieldName;
        public MetaFieldException(string message, Exception inner, string fieldName = null)
            : base(message, inner) => FieldName = fieldName;

        protected MetaFieldException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class FieldReadOnlyException : MetaFieldException
    {
        public FieldReadOnlyException(string fieldName = null) : base(fieldName) { }
        public FieldReadOnlyException(string message, Exception inner) : base(message, inner) { }
        public FieldReadOnlyException(string message, string fieldName = null) : base(message, fieldName) { }
        public FieldReadOnlyException(string message, Exception inner, string fieldName = null)
            : base(message, inner, fieldName) { }

        protected FieldReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class FieldRequiredException : MetaFieldException
    {
        public FieldRequiredException(string fieldName = null) : base(fieldName) { }
        public FieldRequiredException(string message, Exception inner) : base(message, inner) { }
        public FieldRequiredException(string message, string fieldName = null) : base(message, fieldName) { }
        public FieldRequiredException(string message, Exception inner, string fieldName = null)
            : base(message, inner, fieldName) { }

        protected FieldRequiredException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class FieldNameConflictException : MetaFieldException
    {
        public FieldNameConflictException(string fieldName = null) : base(fieldName) { }
        public FieldNameConflictException(string message, Exception inner) : base(message, inner) { }
        public FieldNameConflictException(string message, string fieldName = null) : base(message, fieldName) { }
        public FieldNameConflictException(string message, Exception inner, string fieldName = null)
            : base(message, inner, fieldName) { }

        protected FieldNameConflictException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class FieldInvalidValueException : MetaFieldException
    {
        [XmlElement]
        public ArbitraryValueContainer FieldValue
        {
            get => (ArbitraryValueContainer)Data[nameof(FieldName)];
            protected set => Data[nameof(ArbitraryValueContainer)] = value;
        }

        public FieldInvalidValueException(string fieldName = null, object fieldValue = null) : base(fieldName) { }
        public FieldInvalidValueException(string message, Exception inner, object fieldValue = null)
            : base(message, inner) { }

        public FieldInvalidValueException(string message, string fieldName = null, object fieldValue = null)
            : base(message, fieldName) { }

        public FieldInvalidValueException(string message, Exception inner, string fieldName = null
            , object fieldValue = null) : base(message, inner, fieldName) { }

        protected FieldInvalidValueException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }

    [Serializable]
    public class FieldNullException : FieldInvalidValueException
    {
        public FieldNullException(string fieldName = null) : base(fieldName, fieldValue: null) { }
        public FieldNullException(string message, Exception inner) : base(message, inner, fieldValue: null) { }
        public FieldNullException(string message, string fieldName = null) : base(message, fieldName, fieldValue: null) { }
        public FieldNullException(string message, Exception inner, string fieldName = null) : base(message, inner,
            fieldName, fieldValue: null)
        { }

        protected FieldNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}