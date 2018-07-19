using System;

using OpenCAD.OpenCADFormat.DataTypes;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IPhysicalQuantity
    {
        string Symbol { get; }
    }

    public interface IQuantity<M> where M : IPhysicalQuantity, new()
    {
        BigFloat StandardAmount { get; }
    }

    public sealed class Quantity<M> : IQuantity<M> where M : IPhysicalQuantity, new()
    {
        public BigFloat StandardAmount { get; private set; }

        public Quantity(BigFloat standardAmount)
        {
            StandardAmount = standardAmount;
        }
    }

    namespace Quantities
    {
        public sealed class Length : IPhysicalQuantity
        {
            public static readonly IUnit<Length> Meter = new Unit<Length>("m", 1);
            /*public static readonly IUnit<Length> Mile = new Unit<Length>("mi", 1760, Yard);
            public static readonly IUnit<Length> Furlong = new Unit<Length>("fur", 220, Yard);
            public static readonly IUnit<Length> Chain = new Unit<Length>("ch", 22, Yard);
            public static readonly IUnit<Length> Yard = new Unit<Length>("yd", 3, Foot);
            public static readonly IUnit<Length> Foot = new Unit<Length>("ft", .001, Inch);
            public static readonly IUnit<Length> Inch = new Unit<Length>("in", .254, Meter);
            public static readonly IUnit<Length> Mil = new Unit<Length>("mil", .001, Inch);*/

            public string Symbol { get; } = "L";

            public UnitCollection<Length> SupportedUnits { get; } = new UnitCollection<Length>(
                Utils.GetSupportedUnits<Length>());
        }

        public sealed class PlaneAngle : IPhysicalQuantity
        {
            public static readonly IUnit<PlaneAngle> Degree = new Unit<PlaneAngle>("\x00B0", 1.0);
            public static readonly IUnit<PlaneAngle> Radian = new Unit<PlaneAngle>("rad", 1.0 / 180.0 * Math.PI, Degree);
            public static readonly IUnit<PlaneAngle> Gradian = new Unit<PlaneAngle>("grad", 9.0 / 10.0, Degree);
            public static readonly IUnit<PlaneAngle> Minute = new Unit<PlaneAngle>("'", 1.0 / 60.0, Degree);
            public static readonly IUnit<PlaneAngle> Second = new Unit<PlaneAngle>("\"", 1.0 / 60.0, Minute);

            public string Symbol { get; } = "L";

            public UnitCollection<PlaneAngle> SupportedUnits { get; } = new UnitCollection<PlaneAngle>(
                Utils.GetSupportedUnits<PlaneAngle>());
        }

        public sealed class Frequency : IPhysicalQuantity
        {
            public static readonly IUnit<Frequency> Hertz = new Unit<Frequency>("Hz", 1.0);

            public string Symbol { get; } = "f";

            public UnitCollection<Frequency> SupportedUnits { get; } = new UnitCollection<Frequency>(
                Utils.GetSupportedUnits<Frequency>());
        }

        public sealed class Charge : IPhysicalQuantity
        {
            public static readonly IUnit<Charge> Coulomb = new Unit<Charge>("C", 1.0);

            public string Symbol { get; } = "";

            public UnitCollection<Charge> SupportedUnits { get; } = new UnitCollection<Charge>(
                Utils.GetSupportedUnits<Charge>());
        }

        public sealed class Current : IPhysicalQuantity
        {
            public static readonly IUnit<Current> Ampere = new Unit<Current>("A", 1.0);

            public string Symbol { get; } = "I";

            public UnitCollection<Current> SupportedUnits { get; } = new UnitCollection<Current>(
                Utils.GetSupportedUnits<Current>());
        }

        public sealed class Time : IPhysicalQuantity
        {
            public static readonly IUnit<Time> Second = new Unit<Time>("s", 1.0);
            public static readonly IUnit<Time> Minute = new Unit<Time>("min", 60, Second);
            public static readonly IUnit<Time> Hour = new Unit<Time>("h", 60, Minute);
            public static readonly IUnit<Time> Day = new Unit<Time>("d", 24, Hour);
            public static readonly IUnit<Time> Week = new Unit<Time>("week", 7, Day);
            public static readonly IUnit<Time> Month = new Unit<Time>("month", 30, Day);
            public static readonly IUnit<Time> Year = new Unit<Time>("year", 12, Month);
            public static readonly IUnit<Time> Decade = new Unit<Time>("decade", 10, Year);
            public static readonly IUnit<Time> Century = new Unit<Time>("century", 100, Year);
            public static readonly IUnit<Time> Millenium = new Unit<Time>("millenium", 100, Year);

            public string Symbol { get; } = "t";

            public UnitCollection<Time> SupportedUnits { get; } = new UnitCollection<Time>(
                Utils.GetSupportedUnits<Time>());
        }
    }
}