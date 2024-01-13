using System;
using System.Runtime.Serialization;

namespace OpenCAD.Modules.Math.Exceptions
{
    [Serializable]
    internal class ValueConverterNotFoundException : Exception
    {
        public ValueConverterNotFoundException()
        {
        }

        public ValueConverterNotFoundException(string message) : base(message)
        {
        }

        public ValueConverterNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ValueConverterNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}