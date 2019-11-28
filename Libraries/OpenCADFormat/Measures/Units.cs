using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace OpenCAD.OpenCADFormat.Measures
{
    public static class Units
    {
        internal static void AddUnit(Unit unit) => SupportedUnits.Add(unit);
        internal static void RemoveUnit(Unit unit) => SupportedUnits.Remove(unit);

        public static readonly List<Unit> SupportedUnits = new List<Unit>();

        public static class Capacitance
        {
            public readonly static Unit Farad = new BaseUnit("Farad", Quantities.Capacitance, 1.0, "F");
        }

        public static class Charge
        {
            public readonly static Unit Coulomb = new BaseUnit("Coulomb", Quantities.Charge, 1.0, "C");
        }

        public static class Conductance
        {
            public readonly static Unit Siemens = new BaseUnit("Siemens", Quantities.Conductance, 1.0, "S");
        }

        public static class Current
        {
            public readonly static Unit Ampere = new BaseUnit("Ampere", Quantities.Current, 1.0, "A");
        }

        public static class Frequency
        {
            public readonly static Unit Hertz = new BaseUnit("Hertz", Quantities.Frequency, 1.0, "Hz");
        }

        public static class Inductance
        {
            public readonly static Unit Henry = new BaseUnit("Henry", Quantities.Inductance, 1.0, "H");
        }

        public static class Length
        {
            //Metric
            public readonly static Unit Meter = new BaseUnit("Meter", Quantities.Length, 1, "m");
            //Imperial
            public readonly static Unit Inch = Meter.Derive("Inch", .0254, "in", "\"");
            public readonly static Unit Mil = Inch.Derive("Mil", .001, "mil");
            public readonly static Unit Foot = Inch.Derive("Foot", 12, "ft");
            public readonly static Unit Yard = Foot.Derive("Yard", 3, "yd");
            public readonly static Unit Chain = Yard.Derive("Chain", 22, "ch");
            public readonly static Unit Furlong = Chain.Derive("Furlong", 10, "fur");
            public readonly static Unit Mile = Furlong.Derive("Mile", 8, "mi");
            public readonly static Unit PixelX = Inch.Derive("PixelX", 1 / Utils.GetScreenDPI().X, "px_x");
            public readonly static Unit PixelY = Inch.Derive("PixelY", 1 / Utils.GetScreenDPI().Y, "px_y");
        }

        public static class PlaneAngle
        {
            public static readonly Unit Degree = new BaseUnit("Degree", Quantities.PlaneAngle, 1.0, "deg", "°");
            public static readonly Unit Radian = Degree.Derive("Radian", 180.0 / Math.PI, "rad");
            public static readonly Unit Gradian = Degree.Derive("Gradian", 9.0 / 10.0, "grad");
            public static readonly Unit ArcMinute = Degree.Derive("Arc Minute", 1.0 / 60.0, "arcmin", "\'");
            public static readonly Unit ArcSecond = ArcMinute.Derive("Arc Second", 1.0 / 60.0, "arcsec", "\"");
        }

        public static class Power
        {
            public static readonly Unit Watt = new BaseUnit("Watt", Quantities.Power, 1.0, "W");
        }

        public static class Resistance
        {
            public static readonly Unit Ohm = new BaseUnit("Ohm", Quantities.Resistance, 1.0, "ohm", "Ω");
        }

        public static class Temperature
        {
            //Metric
            public static readonly Unit Kelvin = new BaseUnit("Kelvin", Quantities.Temperature, 1.0, "K");
            public static readonly Unit DegreeCelsius = new BaseUnit("Degree Celsius", Quantities.Temperature, 1.0, "degC", "°C");

            //Imperial
            public static readonly Unit DegreeFahrenheit = DegreeCelsius.Derive("Degree Fahrenheit", 5.0 / 9.0, "degF", "°F");
        }

        public static class Time
        {
            public static readonly Unit Second = new BaseUnit("Second", Quantities.Time, 1.0, "s");
            public static readonly Unit Minute = Second.Derive("Minute", 60, "min");
            public static readonly Unit Hour = Minute.Derive("Hour", 60, "h");
            public static readonly Unit Day = Hour.Derive("Day", 24, "d");
            public static readonly Unit Week = Day.Derive("Week", 7, "wk");
            public static readonly Unit Month = Day.Derive("Month", 30, "mth");
            public static readonly Unit Year = Month.Derive("Year", 12, "yr");
        }

        static Units()
        {
            RuntimeHelpers.RunClassConstructor(typeof(Capacitance).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Charge).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Conductance).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Current).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Frequency).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Inductance).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Length).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(PlaneAngle).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Power).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Resistance).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Temperature).TypeHandle);
            RuntimeHelpers.RunClassConstructor(typeof(Time).TypeHandle);
        }
    }
}