using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCAD.OpenCADFormat.Measures
{
    public class Unit
    {
        static Unit Derive(string name, Unit original, double conversionAmount, string symbol, string uiSymbol = null, bool isMetric = true) =>
            new Unit(name, original.Quantity, original.StandardAmount * conversionAmount, symbol, uiSymbol, isMetric);

        static Unit Exponentiate(Unit unit, double exponent) => new ExponentiatedUnit(unit, exponent);

        static Unit Invert(Unit unit) => Exponentiate(unit, -1);

        static Unit Multiply(Unit a, Unit b) => new ComposedUnit(a, b);

        static Unit Divide(Unit a, Unit b) => new ComposedUnit(a, Invert(b));

        public static Unit operator *(Unit a, Unit b) => Multiply(a, b);

        public static Unit operator /(Unit a, Unit b) => Divide(a, b);

        public Unit(string name, Quantity quantity, double standardAmount, string symbol, string uISymbol = null, bool isMetric = true)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            StandardAmount = standardAmount;
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uISymbol ?? symbol;
            IsMetric = isMetric;
        }

        public Unit Derive(string name, double conversionAmount, string symbol, string uiSymbol = null, bool isMetric = true) =>
            Derive(name, this, conversionAmount, symbol, uiSymbol, isMetric);

        public string Name { get; }

        public Quantity Quantity { get; }

        public double StandardAmount { get; }

        public string Symbol { get; }

        public string UISymbol { get; }

        public bool IsMetric { get; }
    }

    public sealed class ExponentiatedUnit : Unit
    {
        private static string getNewUISymbol(Unit baseUnit, double exponent) => $"{baseUnit.Symbol}^{exponent}";

        private static string getNewSymbol(Unit baseUnit, double exponent) => $"{baseUnit.UISymbol}^{exponent}";

        private static double getNewStandardAmount(Unit baseUnit, double exponent) => Math.Pow(baseUnit.StandardAmount, exponent);

        private static Quantity getNewQuantity(Unit baseUnit, double exponent) => null;

        private static string getNewName(Unit baseUnit, double exponent) => null;

        internal ExponentiatedUnit(Unit baseUnit, double exponent) : base(getNewName(baseUnit, exponent), getNewQuantity(baseUnit, exponent),
            getNewStandardAmount(baseUnit, exponent), getNewSymbol(baseUnit, exponent), getNewUISymbol(baseUnit, exponent), baseUnit.IsMetric)
        {
        }

        public Unit BaseUnit { get; }

        public double Exponent { get; }
    }

    public sealed class ComposedUnit : Unit
    {
        public ComposedUnit(params Unit[] baseUnits) : base(getNewName(baseUnits), getNewQuantity(baseUnits),
            getNewStandardAmount(baseUnits), getNewSymbol(baseUnits), getNewUISymbol(baseUnits), getNewIsMetric(baseUnits))
        {
        }

        private static bool getNewIsMetric(Unit[] baseUnits) => baseUnits.All(b => b.IsMetric);

        private static string getNewUISymbol(Unit[] baseUnits) => String.Join(" ", baseUnits.Select(b => b.Symbol));

        private static string getNewSymbol(Unit[] baseUnits) => String.Join(" ", baseUnits.Select(b => b.UISymbol));

        private static double getNewStandardAmount(Unit[] baseUnits) => baseUnits.Select(b => b.StandardAmount).Aggregate((a, b) => a * b);

        private static Quantity getNewQuantity(Unit[] baseUnits) => null;

        private static string getNewName(Unit[] baseUnits) => null;

        public Unit[] BaseUnits { get; }
    }
}