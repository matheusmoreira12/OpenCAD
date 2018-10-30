using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Measures
{
    public class Quantity
    {
        public Quantity(string name, string symbol, string uISymbol = null)
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
        public static Quantity Capacitance = new Quantity("Capacitance", "C");
        public static Quantity Charge = new Quantity("Charge", "Q");
        public static Quantity Conductance = new Quantity("Conductance", "");
        public static Quantity Current = new Quantity("Current", "I");
        public static Quantity Frequency = new Quantity("Frequency", "F");
        public static Quantity Inductance = new Quantity("Inductance", "L");
        public static Quantity Length = new Quantity("Length", "l");
        public static Quantity PlaneAngle = new Quantity("PlaneAngle", "α", "alpha");
        public static Quantity Resistance = new Quantity("Resistance", "R");
        public static Quantity Temperature = new Quantity("Temperature", "T");
        public static Quantity Time = new Quantity("Time", "t");
    }
}