using System;
using System.Linq;

using MathAPI = OpenCAD.APIs.Math;
using OpenCAD.APIs.Math;

namespace OpenCAD.APIs.Measures
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    public abstract class Quantity : IEquatable<Quantity>
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public static Quantity operator *(Quantity a, Quantity b) => (Quantity)MathAPI::Math.Multiply(a, b);

        public static Quantity operator /(Quantity a, Quantity b) => (Quantity)MathAPI::Math.Divide(a, b);

        public static Quantity operator !(Quantity a) => MathAPI::Math.Invert<Quantity, Quantity>(a);

        static Quantity()
        {
            //Multiplication Operators
            Func<DerivedQuantity, DerivedQuantity, DerivedQuantity> multiplyDerivedQuantities = (a, b) =>
            {
                var expression = new DerivedQuantityDimension(a.Dimension.Members
                    .Concat(b.Dimension.Members).ToArray());
                return new DerivedQuantity(expression);
            };

            Func<BaseQuantity, BaseQuantity, DerivedQuantity> multiplyBaseQuantities = (a, b) =>
            {
                var derivedA = new DerivedQuantity(a, 1);
                var derivedB = new DerivedQuantity(b, 1);
                return multiplyDerivedQuantities(derivedA, derivedB);
            };

            Func<BaseQuantity, DerivedQuantity, DerivedQuantity> multiplyBaseByDerivedQuantities = (a, b) =>
            {
                var derivedA = new DerivedQuantity(a, 1);
                return multiplyDerivedQuantities(derivedA, b);
            };

            Func<DerivedQuantity, BaseQuantity, DerivedQuantity> multiplyDerivedByBaseQuantities = (a, b) =>
            {
                var derivedB = new DerivedQuantity(b, 1);
                return multiplyDerivedQuantities(a, derivedB);
            };

            //Exponentiation Operators
            Func<BaseQuantity, double, DerivedQuantity> exponentiateBaseQuantity = (a, b) => new DerivedQuantity(a, b);

            Func<DerivedQuantity, double, DerivedQuantity> exponentiateDerivedQuantity = (a, b) =>
            {
                var members = a.Dimension.Members.Select(m => new DerivedQuantityDimensionMember(m.BaseQuantity,
                    m.Exponent * b)).ToArray();
                var expression = new DerivedQuantityDimension(members);
                return new DerivedQuantity(expression);
            };

            MathOperationManager.RegisterMany(new MathOperation[] {
                new Multiplication<BaseQuantity, BaseQuantity, DerivedQuantity>(multiplyBaseQuantities),
                new Multiplication<DerivedQuantity, DerivedQuantity, DerivedQuantity>(multiplyDerivedQuantities),
                new Multiplication<BaseQuantity, DerivedQuantity, DerivedQuantity>(multiplyBaseByDerivedQuantities),
                new Multiplication<DerivedQuantity, BaseQuantity, DerivedQuantity>(multiplyDerivedByBaseQuantities),
                new Exponentiation<BaseQuantity, double, DerivedQuantity>(exponentiateBaseQuantity),
                new Exponentiation<DerivedQuantity, double, DerivedQuantity>(exponentiateDerivedQuantity)
            });
        }

        public static Quantity Parse(string value)
        {
            Quantity result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static bool TryParse(string value, out Quantity result) =>
            tryParseBySymbol(value, out result) || tryParseByUISymbol(value, out result) || tryParseByName(value, out result);

        private static bool tryParseBySymbol(string symbol, out Quantity result)
        {
            result = MetricSystemManager.GetAllQuantities().FirstOrDefault(q => q.Symbol == symbol);
            if (result == null)
                return false;

            return true;
        }

        private static bool tryParseByUISymbol(string uiSymbol, out Quantity result)
        {
            result = MetricSystemManager.GetAllQuantities().FirstOrDefault(q => q.UISymbol == uiSymbol);
            if (result == null)
                return false;

            return true;
        }

        private static bool tryParseByName(string name, out Quantity result)
        {
            result = MetricSystemManager.GetAllQuantities().FirstOrDefault(q => q.Name == name);
            if (result == null)
                return false;

            return true;
        }

        public abstract string Name { get; }

        public abstract string Symbol { get; }

        public abstract string UISymbol { get; }

        public Unit Unit { get; }

        public MetricSystem MetricSystem { get; internal set; } = null;

        public abstract Quantity Collapse();

        private DerivedQuantity convertToDerivedUnit(Quantity quantity)
        {
            if (quantity is BaseQuantity)
                return new DerivedQuantity((BaseQuantity)quantity, 1);
            else if (quantity is DerivedQuantity)
                return (DerivedQuantity)quantity;

            return null;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Quantity))
                return false;
            else
                return ((IEquatable<Quantity>)this).Equals((Quantity)obj);
        }

        bool IEquatable<Quantity>.Equals(Quantity other)
        {
            Func<bool> metricSystemMatches = () => Utils.NullableEquals(MetricSystem,
                other?.MetricSystem);
            return Name == other?.Name && metricSystemMatches();
        }
    }
}