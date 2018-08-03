using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IPhysicalQuantity { }

    namespace Quantities
    {
        public sealed class Capacitance : IPhysicalQuantity
        {
            public static readonly IUnit<Capacitance> Ohm = new Unit<Capacitance>(1.0, "F");
        }

        public sealed class Charge : IPhysicalQuantity
        {
            public static readonly IUnit<Charge> Coulomb = new Unit<Charge>(1.0, "C");
        }

        public sealed class Conductance : IPhysicalQuantity
        {
            public static readonly IUnit<Conductance> Siemens = new Unit<Conductance>(1.0, "S");
        }

        public sealed class Current : IPhysicalQuantity
        {
            public static readonly IUnit<Current> Ampere = new Unit<Current>(1.0, "A");
        }

        public sealed class Frequency : IPhysicalQuantity
        {
            public static readonly IUnit<Frequency> Hertz = new Unit<Frequency>(1.0, "Hz");
        }

        public sealed class Indctance : IPhysicalQuantity
        {
            public static readonly IUnit<Indctance> Ohm = new Unit<Indctance>(1.0, "H");
        }

        public sealed class Length : IPhysicalQuantity
        {
            //Metric
            public static readonly IUnit<Length> Meter = new Unit<Length>(1, "m");

            //Imperial
            public static readonly IUnit<Length> Inch = new Unit<Length>(.0254, Meter, "in", null, false);
            public static readonly IUnit<Length> Mil = new Unit<Length>(.001, Inch, "mil", null, false);
            public static readonly IUnit<Length> Foot = new Unit<Length>(12, Inch, "ft", null, false);
            public static readonly IUnit<Length> Yard = new Unit<Length>(3, Foot, "yd", null, false);
            public static readonly IUnit<Length> Chain = new Unit<Length>(22, Yard, "ch", null, false);
            public static readonly IUnit<Length> Furlong = new Unit<Length>(10, Chain, "fur", null, false);
            public static readonly IUnit<Length> Mile = new Unit<Length>(8, Furlong, "mi", null, false);
        }

        public sealed class PlaneAngle : IPhysicalQuantity
        {
            public static readonly IUnit<PlaneAngle> Degree = new Unit<PlaneAngle>(1.0, "deg", "°", false);
            public static readonly IUnit<PlaneAngle> Radian = new Unit<PlaneAngle>(180.0 / Math.PI, Degree, "rad", null, false);
            public static readonly IUnit<PlaneAngle> Gradian = new Unit<PlaneAngle>(9.0 / 10.0, Degree, "grad", null, false);
            public static readonly IUnit<PlaneAngle> Minute = new Unit<PlaneAngle>(1.0 / 60.0, Degree, "min", "'", false);
            public static readonly IUnit<PlaneAngle> Second = new Unit<PlaneAngle>(1.0 / 60.0, Minute, "sec", "\"", false);
        }

        public sealed class Resistance : IPhysicalQuantity
        {
            public static readonly IUnit<Resistance> Ohm = new Unit<Resistance>(1.0, "ohm", "Ω");
        }

        public sealed class Temperature : IPhysicalQuantity
        {
            //Metric
            public static readonly IUnit<Temperature> DegreeCelsius = new Unit<Temperature>(1.0, "degC", "°C", false);
            public static readonly IUnit<Temperature> Kelvin = new Unit<Temperature>(1.0, "K");

            //Imperial
            public static readonly IUnit<Temperature> DegreeFahrenheit = new Unit<Temperature>(5.0 / 9.0, "degF", "°F"
                , false);
        }

        public sealed class Time : IPhysicalQuantity
        {
            public static readonly IUnit<Time> Second = new Unit<Time>(1.0, "s");
            public static readonly IUnit<Time> Minute = new Unit<Time>(60, Second, "min", null, false);
            public static readonly IUnit<Time> Hour = new Unit<Time>(60, Minute, "h", null, false);
            public static readonly IUnit<Time> Day = new Unit<Time>(24, Hour, "d", null, false);
            public static readonly IUnit<Time> Week = new Unit<Time>(7, Day, "week", null, false);
            public static readonly IUnit<Time> Month = new Unit<Time>(30, Day, "month", null, false);
            public static readonly IUnit<Time> Year = new Unit<Time>(12, Month, "year", null, false);
            public static readonly IUnit<Time> Decade = new Unit<Time>(10, Year, "decade", null, false);
            public static readonly IUnit<Time> Century = new Unit<Time>(100, Year, "century", null, false);
            public static readonly IUnit<Time> Millenium = new Unit<Time>(100, Year, "millenium", null, false);
        }
    }
}