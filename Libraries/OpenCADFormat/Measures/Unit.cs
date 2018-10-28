using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace OpenCAD.OpenCADFormat.Measures
{
    public class Unit : IDisposable
    {
        public Unit BaseUnit { get; private set; }

        private double conversionFactor;

        public PhysicalQuantity PhysicalQuantity => BaseUnit?.PhysicalQuantity ?? physicalQuantity;
        private PhysicalQuantity physicalQuantity;

        public double StandardAmount => BaseUnit?.StandardAmount * conversionFactor ?? standardAmount;
        private double standardAmount;

        public string Symbol { get; private set; }

        public string Name { get; private set; }

        public string UISymbol { get; private set; }

        public Unit(string name, PhysicalQuantity physicalQuantity, double standardAmount, string symbol, string uiSymbol = null,
            bool isMetric = true)
        {
            Name = name;
            BaseUnit = null;
            this.physicalQuantity = physicalQuantity ?? throw new ArgumentNullException(nameof(physicalQuantity));
            this.standardAmount = standardAmount;
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uiSymbol ?? symbol;
            IsMetric = isMetric;

            Units.AddUnit(this);
        }

        public Unit(string name, Unit baseUnit, double conversionFactor, string symbol, string uiSymbol = null, bool isMetric = true)
        {
            Name = name;
            BaseUnit = baseUnit ?? throw new ArgumentNullException(nameof(baseUnit));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uiSymbol ?? symbol;
            this.conversionFactor = conversionFactor;
            IsMetric = isMetric;

            Units.AddUnit(this);
        }

        public bool IsMetric { get; private set; }

        private void dispose()
        {
            Units.RemoveUnit(this);
        }

        public bool Disposed { get; private set; } = false;

        public void Dispose()
        {
            if (!Disposed)
                dispose();

            Disposed = true;
        }
    }

    public static class Units
    {
        internal static void AddUnit(Unit unit) => SupportedUnits.Add(unit);
        internal static void RemoveUnit(Unit unit) => SupportedUnits.Remove(unit);

        public static readonly List<Unit> SupportedUnits = new List<Unit>();

        public static class Capacitance
        {
            public readonly static Unit Farad = new Unit("Farad", Quantities.Capacitance, 1.0, "F");
        }

        public static class Charge
        {
            public readonly static Unit Coulomb = new Unit("Coulomb", Quantities.Charge, 1.0, "C");
        }

        public static class Conductance
        {
            public readonly static Unit Siemens = new Unit("Siemens", Quantities.Conductance, 1.0, "S");
        }

        public static class Current
        {
            public readonly static Unit Ampere = new Unit("Ampere", Quantities.Current, 1.0, "A");
        }

        public static class Frequency
        {
            public readonly static Unit Hertz = new Unit("Hertz", Quantities.Frequency, 1.0, "Hz");
        }

        public static class Inductance
        {
            public readonly static Unit Henry = new Unit("Henry", Quantities.Inductance, 1.0, "H");
        }

        public static class Length
        {
            //Metric
            public readonly static Unit Meter = new Unit("Meter", Quantities.Length, 1, "m");
            //Imperial
            public readonly static Unit Inch = new Unit("Inch", Meter, .0254, "in", "\"", false);
            public readonly static Unit Mil = new Unit("Mil", Inch, .001, "mil", isMetric: false);
            public readonly static Unit Foot = new Unit("Foot", Inch, 12, "ft", isMetric: false);
            public readonly static Unit Yard = new Unit("Yard", Foot, 3, "yd", isMetric: false);
            public readonly static Unit Chain = new Unit("Chain", Yard, 22, "ch", isMetric: false);
            public readonly static Unit Furlong = new Unit("Furlong", Chain, 10, "fur", isMetric: false);
            public readonly static Unit Mile = new Unit("Mile", Furlong, 8, "mi", isMetric: false);
        }

        public static class PlaneAngle
        {
            public static readonly Unit Degree = new Unit("Degree", Quantities.PlaneAngle, 1.0, "deg", "°", false);
            public static readonly Unit Radian = new Unit("Radian", Degree, 180.0 / Math.PI, "rad", isMetric: false);
            public static readonly Unit Gradian = new Unit("Gradian", Degree, 9.0 / 10.0, "grad", isMetric: false);
            public static readonly Unit Minute = new Unit("Minute", Degree, 1.0 / 60.0, "ang_min", "\'", false);
            public static readonly Unit Second = new Unit("Second", Minute, 1.0 / 60.0, "ang_s", "\"", false);
        }

        public static class Resistance
        {
            public static readonly Unit Ohm = new Unit("Ohm", Quantities.Resistance, 1.0, "ohm", "Ω");
        }

        public static class Temperature
        {
            //Metric
            public static readonly Unit Kelvin = new Unit("Kelvin", Quantities.Temperature, 1.0, "K");
            public static readonly Unit DegreeCelsius = new Unit("Degree Celsius", Kelvin, 1.0, "degC", "°C");

            //Imperial
            public static readonly Unit DegreeFahrenheit = new Unit("Degree Fahrenheit", DegreeCelsius, 5.0 / 9.0, 
                "degF", "°F", false);
        }

        public static class Time
        {
            public static readonly Unit Second = new Unit("Second", Quantities.Time, 1.0, "s");
            public static readonly Unit Minute = new Unit("Minute", Second, 60, "min", isMetric: false);
            public static readonly Unit Hour = new Unit("Hour", Minute, 60, "h", isMetric: false);
            public static readonly Unit Day = new Unit("Day", Hour, 24, "d", isMetric: false);
            public static readonly Unit Week = new Unit("Week", Day, 7, "wk", isMetric: false);
            public static readonly Unit Month = new Unit("Month", Day, 30, "mth", isMetric: false);
            public static readonly Unit Year = new Unit("Year", Month, 12, "yr", isMetric: false);
            public static readonly Unit Decade = new Unit("Decade", Year, 10, "dec", isMetric: false);
            public static readonly Unit Century = new Unit("Century", Year, 100, "cent", isMetric: false);
            public static readonly Unit Millenium = new Unit("Millenium", Year, 1000, "millenium", isMetric: false);
        }

        static Units()
        {
            RuntimeHelpers.RunClassConstructor(typeof(Capacitance).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Charge).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Conductance).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Current).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Frequency).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Length).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PlaneAngle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Resistance).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Temperature).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Time).TypeHandle);
        }
    }
}