using System;
using System.Linq;

using MathAPI = OpenCAD.APIs.Math;
using OpenCAD.APIs.Math;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures
{
    public abstract class Quantity: IDisposable
    {
        #region Metric System Management
        /// <summary>
        /// Gets all the available quantitites.
        /// </summary>
        /// <returns>The available quantities.</returns>
        public static IEnumerable<Quantity> GetAll() 
            => MetricSystemManager.GetAllQuantities();
        #endregion

        #region Math API Integration
        //Multiplication Operators
        private static DerivedQuantity multiplyDerivedQuantities(DerivedQuantity a, DerivedQuantity b)
        {
            var expression = new DerivedQuantityDimension(a.Dimension.Members
                .Concat(b.Dimension.Members).ToArray());
            return new DerivedQuantity(expression);
        }

        private static DerivedQuantity multiplyBaseQuantities(BaseQuantity a, BaseQuantity b)
        {
            var derivedA = new DerivedQuantity(a, 1);
            var derivedB = new DerivedQuantity(b, 1);
            return multiplyDerivedQuantities(derivedA, derivedB);
        }

        private static DerivedQuantity multiplyBaseByDerivedQuantities(BaseQuantity a, DerivedQuantity b)
        {
            var derivedA = new DerivedQuantity(a, 1);
            return multiplyDerivedQuantities(derivedA, b);
        }

        private static DerivedQuantity multiplyDerivedByBaseQuantities(DerivedQuantity a, BaseQuantity b)
        {
            var derivedB = new DerivedQuantity(b, 1);
            return multiplyDerivedQuantities(a, derivedB);
        }

        //Exponentiation Operators
        private static DerivedQuantity exponentiateBaseQuantity(BaseQuantity a, double b)
            => new DerivedQuantity(a, b);

        private static DerivedQuantity exponentiateDerivedQuantity(DerivedQuantity a, double b)
        {
            var members = a.Dimension.Members.Select(m => new DerivedQuantityDimensionMember(m.BaseQuantity,
                m.Exponent * b)).ToArray();
            var expression = new DerivedQuantityDimension(members);
            return new DerivedQuantity(expression);
        }
        #endregion

        static Quantity()
        {
            MathOperationManager.RegisterMany(new MathOperation[] {
                new Multiplication<BaseQuantity, BaseQuantity, DerivedQuantity>(multiplyBaseQuantities),
                new Multiplication<DerivedQuantity, DerivedQuantity, DerivedQuantity>(multiplyDerivedQuantities),
                new Multiplication<BaseQuantity, DerivedQuantity, DerivedQuantity>(multiplyBaseByDerivedQuantities),
                new Multiplication<DerivedQuantity, BaseQuantity, DerivedQuantity>(multiplyDerivedByBaseQuantities),
                new Exponentiation<BaseQuantity, double, DerivedQuantity>(exponentiateBaseQuantity),
                new Exponentiation<DerivedQuantity, double, DerivedQuantity>(exponentiateDerivedQuantity)
            });
        }

        #region Arithmetic Operators
        public static Quantity operator *(Quantity a, Quantity b)
            => (Quantity)MathAPI::Math.Multiply(a, b);

        public static Quantity operator /(Quantity a, Quantity b)
            => (Quantity)MathAPI::Math.Divide(a, b);

        public static Quantity operator !(Quantity a)
            => MathAPI::Math.Invert<Quantity, Quantity>(a);
        #endregion

        #region Parsing
        public static Quantity Parse(string value)
        {
            Quantity result;
            if (TryParse(value, out result))
                return result;

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static bool TryParse(string value, out Quantity result)
            => tryParseBySymbol(value, out result) || tryParseByUISymbol(value, out result)
            || tryParseByName(value, out result);

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
        #endregion

        /// <summary>
        /// Gets the name of this quantity.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// Gets the symbol used internally by the application for representing this quantity.
        /// </summary>
        public abstract string Symbol { get; }

        /// <summary>
        /// Gets the symbol shown to the user by the UI for representing this quantity.
        /// </summary>
        public abstract string UISymbol { get; }

        /// <summary>
        /// Gets the unit of this quantity.
        /// </summary>
        public Unit Unit { get; }

        /// <summary>
        /// Gets the metric system this quantity belongs to.
        /// </summary>
        public MetricSystem MetricSystem { get; internal set; } = null;

        /// <summary>
        /// Gets this quantity in its collapsed form.
        /// </summary>
        /// <returns>This quantity in its collapsed form.</returns>
        public abstract Quantity Collapse();

        public abstract void Dispose();
    }
}