using System;

namespace OpenCAD.OpenCADFormat.Libraries.Exceptions
{
    public class LibraryException: Exception
    {
        public LibraryException() { }
        public LibraryException(string message): base(message) { }
        public LibraryException(string message, Exception innerException) : base(message, innerException) { }
        public LibraryException(System.Runtime.Serialization.SerializationInfo info, 
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}