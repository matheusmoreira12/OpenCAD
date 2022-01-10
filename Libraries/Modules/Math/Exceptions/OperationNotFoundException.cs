using System;
using System.Runtime.Serialization;

namespace OpenCAD.Modules.Math.Exceptions
{
    [Serializable]
    internal class OperationNotFoundException : Exception
    {
        public OperationNotFoundException()
        {
        }

        public OperationNotFoundException(string message) : base(message)
        {
        }

        public OperationNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected OperationNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}