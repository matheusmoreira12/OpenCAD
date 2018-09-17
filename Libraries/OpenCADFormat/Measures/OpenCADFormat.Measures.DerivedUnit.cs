using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.OpenCADFormat.Measures
{
    public struct DerivedUnit : IUnit
    {
        public DerivedUnit(IUnit baseUnit, double conversionFactor, string symbol)
            : this(baseUnit, conversionFactor, symbol, null) { }

        public DerivedUnit(IUnit baseUnit, double conversionFactor, string symbol, string uISymbol)
        {
            BaseUnit = baseUnit;
            ConversionFactor = conversionFactor;
            Symbol = symbol;
            UISymbol = uISymbol;
        }

        public PhysicalQuantity PhysicalQuantity => BaseUnit.PhysicalQuantity;

        public double StandardAmount => BaseUnit.StandardAmount * ConversionFactor;

        public IUnit BaseUnit { get; private set; }

        public double ConversionFactor { get; private set; }

        public string Symbol { get; private set; }

        public string UISymbol { get; private set; }

        public bool IsMetric => false;
    }
}
