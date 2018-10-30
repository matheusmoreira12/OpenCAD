using System;
using System.Runtime.Serialization;

namespace OpenCAD.OpenCADFormat.Measures
{
    [Serializable]
    public class WrongUnitQuantityException : Exception
    {
        private void setData(Quantity expected, Quantity found)
        {
            Data["Expected"] = expected;
            Data["Found"] = found;
        }

        public WrongUnitQuantityException() { }

        public WrongUnitQuantityException(string message) { }

        public WrongUnitQuantityException(Quantity expected, Quantity found)
        {
            setData(expected, found);
        }

        public WrongUnitQuantityException(string message, Quantity expected, Quantity found) : base(message)
        {
            setData(expected, found);
        }

        public WrongUnitQuantityException(string message, Quantity expected, Quantity found, 
            Exception innerException) : base(message, innerException)
        {
            setData(expected, found);
        }

        protected WrongUnitQuantityException(SerializationInfo info, Quantity expected, Quantity found, 
            StreamingContext context) : base(info, context)
        {
            setData(expected, found);
        }
    }
}