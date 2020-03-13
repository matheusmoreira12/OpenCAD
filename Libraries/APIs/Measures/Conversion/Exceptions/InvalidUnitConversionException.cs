using System;
using System.Runtime.Serialization;

namespace OpenCAD.APIs.Measures.Conversion.Exceptions
{
    class InvalidUnitConversionException : Exception
    {
        const string DEFAULT_MESSAGE = "The specified source unit is not convertible to the specified destination unit.";

        public InvalidUnitConversionException(Unit sourceUnit, Unit destUnit) : base(DEFAULT_MESSAGE)
        {
            setData(sourceUnit, destUnit);
        }

        public InvalidUnitConversionException(Unit sourceUnit, Unit destUnit, string message) : base(message)
        {
            setData(sourceUnit, destUnit);
        }

        public InvalidUnitConversionException(Unit sourceUnit, Unit destUnit, string message,
            Exception innerException) : base(message, innerException)
        {
            setData(sourceUnit, destUnit);
        }

        protected InvalidUnitConversionException(Unit sourceUnit, Unit destUnit,
            SerializationInfo info, StreamingContext context) : base(info, context)
        {
            setData(sourceUnit, destUnit);
        }

        private void setData(Unit sourceUnit, Unit destUnit)
        {
            Data["SourceUnit"] = sourceUnit;
            Data["DestUnit"] = destUnit;
        }
    }
}
