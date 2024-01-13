using System;
using System.Runtime.Serialization;

namespace OpenCAD.Modules.Math.Exceptions
{
    [Serializable]
    internal class ValueConversionNotFoundException : Exception
    {
        public ValueConversionNotFoundException()
        {
        }

        public ValueConversionNotFoundException(string message) : base(message)
        {
        }

        public ValueConversionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueConversionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}