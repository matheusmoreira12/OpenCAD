using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Measures
{
    public class PhysicalQuantity
    {
        public PhysicalQuantity(string name, string symbol, string uISymbol = null)
        {
            Name = name ?? throw new ArgumentNullException("name");
            Symbol = symbol ?? throw new ArgumentNullException("symbol");
            UISymbol = uISymbol ?? symbol;
        }

        public string Name { get; private set; }

        public string Symbol { get; private set; }

        public string UISymbol { get; private set; }
    }

    public static class Quantities
    {
        public static PhysicalQuantity Capacitance = new PhysicalQuantity("Capacitance", "C");

        public static PhysicalQuantity Charge = new PhysicalQuantity("Charge", "Q");

        public static PhysicalQuantity Conductance = new PhysicalQuantity("Conductance", "");

        public static PhysicalQuantity Current = new PhysicalQuantity("Current", "I");

        public static PhysicalQuantity Frequency = new PhysicalQuantity("Frequency", "F");

        public static PhysicalQuantity Inductance = new PhysicalQuantity("Inductance", "L");

        public static PhysicalQuantity Length = new PhysicalQuantity("Length", "l");

        public static PhysicalQuantity PlaneAngle = new PhysicalQuantity("PlaneAngle", "α", "alpha");

        public static PhysicalQuantity Resistance = new PhysicalQuantity("Resistance", "R");

        public static PhysicalQuantity Temperature = new PhysicalQuantity("Temperature", "T");

        public static PhysicalQuantity Time = new PhysicalQuantity("Time", "t");
    }
}