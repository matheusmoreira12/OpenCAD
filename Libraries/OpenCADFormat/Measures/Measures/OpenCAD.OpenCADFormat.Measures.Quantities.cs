using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IPhysicalQuantity
    {
    }

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
            public static readonly IUnit<Length> Meter = new Unit<Length>(1, "m");

            //Imperial
            public static readonly IUnit<Length> Inch = new Unit<Length>(.0254, Meter, "in");
            public static readonly IUnit<Length> Mil = new Unit<Length>(.001, Inch, "mil");
            public static readonly IUnit<Length> Foot = new Unit<Length>(12, Inch, "ft");
            public static readonly IUnit<Length> Yard = new Unit<Length>(3, Foot, "yd");
            public static readonly IUnit<Length> Chain = new Unit<Length>(22, Yard, "ch");
            public static readonly IUnit<Length> Furlong = new Unit<Length>(10, Chain, "fur");
            public static readonly IUnit<Length> Mile = new Unit<Length>(8, Furlong, "mi");
        }

        public sealed class PlaneAngle : IPhysicalQuantity
        {
            public static readonly IUnit<PlaneAngle> Degree = new Unit<PlaneAngle>(1.0, "°", "deg");
            public static readonly IUnit<PlaneAngle> Radian = new Unit<PlaneAngle>(1.0 / 180.0 * Math.PI, Degree, "rad");
            public static readonly IUnit<PlaneAngle> Gradian = new Unit<PlaneAngle>(9.0 / 10.0, Degree, "grad");
            public static readonly IUnit<PlaneAngle> Minute = new Unit<PlaneAngle>(1.0 / 60.0, Degree, "'", "min");
            public static readonly IUnit<PlaneAngle> Second = new Unit<PlaneAngle>(1.0 / 60.0, Minute, "\"", "sec");
        }

        public sealed class Frequency : IPhysicalQuantity
        {
            public static readonly IUnit<Frequency> Hertz = new Unit<Frequency>(1.0, "Hz");
        }

        public sealed class Charge : IPhysicalQuantity
        {
            public static readonly IUnit<Charge> Coulomb = new Unit<Charge>(1.0, "C");
        }

        public sealed class Current : IPhysicalQuantity
        {
            public static readonly IUnit<Current> Ampere = new Unit<Current>(1.0, "A");
        }

        public sealed class Time : IPhysicalQuantity
        {
            public static readonly IUnit<Time> Second = new Unit<Time>(1.0, "s");
            public static readonly IUnit<Time> Minute = new Unit<Time>(60, Second, "min");
            public static readonly IUnit<Time> Hour = new Unit<Time>(60, Minute, "h");
            public static readonly IUnit<Time> Day = new Unit<Time>(24, Hour, "d");
            public static readonly IUnit<Time> Week = new Unit<Time>(7, Day, "week");
            public static readonly IUnit<Time> Month = new Unit<Time>(30, Day, "month");
            public static readonly IUnit<Time> Year = new Unit<Time>(12, Month, "year");
            public static readonly IUnit<Time> Decade = new Unit<Time>(10, Year, "decade");
            public static readonly IUnit<Time> Century = new Unit<Time>(100, Year, "century");
            public static readonly IUnit<Time> Millenium = new Unit<Time>(100, Year, "millenium");
        }
    }
}