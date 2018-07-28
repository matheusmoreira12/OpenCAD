using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IPhysicalQuantity { }

    public interface IQuantity<M> where M : IPhysicalQuantity, new()
    {
        double StandardAmount { get; }
    }

    public sealed class Quantity<M> : IQuantity<M> where M : IPhysicalQuantity, new()
    {
        public double StandardAmount { get; private set; }

        public Quantity(double standardAmount)
        {
            StandardAmount = standardAmount;
        }
    }

    namespace Quantities
    {
        public sealed class Length : IPhysicalQuantity
        {
            //Metric
            public static readonly Unit<Length> Meter = new Unit<Length>(1, "m");

            //Imperial
            public static readonly Unit<Length> Inch = new Unit<Length>(.0254, Meter, "in");
            public static readonly Unit<Length> Mil = new Unit<Length>(.001, Inch, "mil");
            public static readonly Unit<Length> Foot = new Unit<Length>(12, Inch, "ft");
            public static readonly Unit<Length> Yard = new Unit<Length>(3, Foot, "yd");
            public static readonly Unit<Length> Chain = new Unit<Length>(22, Yard, "ch");
            public static readonly Unit<Length> Furlong = new Unit<Length>(10, Chain, "fur");
            public static readonly Unit<Length> Mile = new Unit<Length>(8, Furlong, "mi");
        }

        public sealed class PlaneAngle : IPhysicalQuantity
        {
            public static readonly Unit<PlaneAngle> Degree = new Unit<PlaneAngle>(1.0, "deg", "°");
            public static readonly Unit<PlaneAngle> Radian = new Unit<PlaneAngle>(180.0 / Math.PI, Degree, "rad");
            public static readonly Unit<PlaneAngle> Gradian = new Unit<PlaneAngle>(9.0 / 10.0, Degree, "grad");
            public static readonly Unit<PlaneAngle> Minute = new Unit<PlaneAngle>(1.0 / 60.0, Degree, "min", "'");
            public static readonly Unit<PlaneAngle> Second = new Unit<PlaneAngle>(1.0 / 60.0, Minute, "sec", "\"");
        }

        public sealed class Frequency : IPhysicalQuantity
        {
            public static readonly Unit<Frequency> Hertz = new Unit<Frequency>(1.0, "Hz");
        }

        public sealed class Charge : IPhysicalQuantity
        {
            public static readonly Unit<Charge> Coulomb = new Unit<Charge>(1.0, "C");
        }

        public sealed class Current : IPhysicalQuantity
        {
            public static readonly Unit<Current> Ampere = new Unit<Current>(1.0, "A");
        }

        public sealed class Resistance : IPhysicalQuantity
        {
            public static readonly Unit<Resistance> Ohm = new Unit<Resistance>(1.0, "ohm", "Ω");
        }

        public sealed class Conductance : IPhysicalQuantity
        {
            public static readonly Unit<Conductance> Siemens = new Unit<Conductance>(1.0, "S");
        }

        public sealed class Temperature : IPhysicalQuantity
        {
            //Metric
            public static readonly Unit<Temperature> DegreeCelsius = new Unit<Temperature>(1.0, "degC", "°C");
            public static readonly Unit<Temperature> Kelvin = new Unit<Temperature>(1.0, "K");

            //Imperial
            public static readonly Unit<Temperature> DegreeFahrenheit = new Unit<Temperature>(5.0 / 9.0, "degF", "°F");
        }

        public sealed class Time : IPhysicalQuantity
        {
            public static readonly Unit<Time> Second = new Unit<Time>(1.0, "s");
            public static readonly Unit<Time> Minute = new Unit<Time>(60, Second, "min");
            public static readonly Unit<Time> Hour = new Unit<Time>(60, Minute, "h");
            public static readonly Unit<Time> Day = new Unit<Time>(24, Hour, "d");
            public static readonly Unit<Time> Week = new Unit<Time>(7, Day, "week");
            public static readonly Unit<Time> Month = new Unit<Time>(30, Day, "month");
            public static readonly Unit<Time> Year = new Unit<Time>(12, Month, "year");
            public static readonly Unit<Time> Decade = new Unit<Time>(10, Year, "decade");
            public static readonly Unit<Time> Century = new Unit<Time>(100, Year, "century");
            public static readonly Unit<Time> Millenium = new Unit<Time>(100, Year, "millenium");
        }
    }
}