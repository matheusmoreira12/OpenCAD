using System;
using System.Runtime.Serialization;

namespace OpenCAD.APIs.Measures.Conversion.Exceptions
{
    class UnitConversionNotSupportedException : Exception
    {
        const string DEFAULT_MESSAGE = "The specified source unit is not convertible to the specified destination unit.";

        public UnitConversionNotSupportedException(Unit sourceUnit, Unit destUnit) : base(DEFAULT_MESSAGE)
        {
            setData(sourceUnit, destUnit);
        }

        public UnitConversionNotSupportedException(Unit sourceUnit, Unit destUnit, string message) : base(message)
        {
            setData(sourceUnit, destUnit);
        }

        public UnitConversionNotSupportedException(Unit sourceUnit, Unit destUnit, string message,
            Exception innerException) : base(message, innerException)
        {
            setData(sourceUnit, destUnit);
        }

        protected UnitConversionNotSupportedException(Unit sourceUnit, Unit destUnit,
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
