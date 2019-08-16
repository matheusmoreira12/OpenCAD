using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Unit
    {
        static BaseUnit Derive(string name, Unit original, double conversionAmount, string symbol, string uiSymbol = null, bool isMetric = true) =>
            new BaseUnit(name, original.Quantity, original.StandardAmount * conversionAmount, symbol, uiSymbol, isMetric);

        static Unit Exponentiate(Unit unit, double exponent) => new ExponentiatedUnit(unit, exponent).Collapse();

        static Unit Invert(Unit unit) => Exponentiate(unit, -1);

        static Unit Multiply(Unit a, Unit b) => new ComposedUnit(a, b).Collapse();

        static Unit Divide(Unit a, Unit b)
        {
            if (a is null)
                return Invert(b);

            return Multiply(a, Invert(b));
        }

        public static Unit operator *(Unit a, Unit b) => Multiply(a, b);

        public static Unit operator /(Unit a, Unit b) => Divide(a, b);

        public Unit Exponentiate(double exponent) => Exponentiate(this, exponent);

        public Unit Invert() => Invert(this);

        public Unit Multiply(Unit other) => Multiply(this, other);

        public Unit Divide(Unit other) => Divide(this, other);

        public abstract Unit Collapse();

        public abstract string Name { get; }

        public abstract Quantity Quantity { get; }

        public abstract double StandardAmount { get; }

        public abstract string Symbol { get; }

        public abstract string UISymbol { get; }

        public abstract bool IsMetric { get; }

        public Unit Derive(string name, double conversionAmount, string symbol, string uiSymbol = null, bool isMetric = true) =>
            Derive(name, this, conversionAmount, symbol, uiSymbol, isMetric);
    }

    public class BaseUnit : Unit
    {
        public BaseUnit(string name, Quantity quantity, double standardAmount, string symbol, string uISymbol = null, bool isMetric = true)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            StandardAmount = standardAmount;
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uISymbol ?? symbol;
            IsMetric = isMetric;
        }

        public override Unit Collapse() => this;

        public override string Name { get; }

        public override Quantity Quantity { get; }

        public override double StandardAmount { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public override bool IsMetric { get; }
    }

    public sealed class ExponentiatedUnit : Unit
    {
        internal ExponentiatedUnit(Unit baseUnit, double exponent)
        {
            BaseUnit = baseUnit;
            Exponent = exponent;
        }

        public override Unit Collapse()
        {
            Unit baseUnit = BaseUnit.Collapse();

            if (baseUnit is ExponentiatedUnit)
                return new ExponentiatedUnit((baseUnit as ExponentiatedUnit).BaseUnit, (baseUnit as ExponentiatedUnit).Exponent * Exponent);

            if (baseUnit is ComposedUnit)
                return new ComposedUnit((baseUnit as ComposedUnit).BaseUnits.Select(bu => bu.Exponentiate(Exponent).Collapse()).ToArray());

            return new ExponentiatedUnit(baseUnit, Exponent);
        }

        public Unit BaseUnit { get; }

        public double Exponent { get; }

        public override string Name => null;

        public override Quantity Quantity => null;

        public override double StandardAmount => Math.Pow(BaseUnit.StandardAmount, Exponent);

        public override string Symbol => $"{BaseUnit.UISymbol}^{Exponent}";

        public override string UISymbol => $"{BaseUnit.Symbol}^{Exponent}";

        public override bool IsMetric => BaseUnit.IsMetric;
    }

    public sealed class ComposedUnit : Unit
    {
        internal ComposedUnit(params Unit[] baseUnits)
        {
            BaseUnits = baseUnits;
        }

        public override Unit Collapse()
        {
            var collapsedBaseUnits = BaseUnits.SelectMany(bu => (bu as ComposedUnit)?.BaseUnits ?? new[] { bu.Collapse() })
                .GroupBy(bu => (bu as ExponentiatedUnit)?.BaseUnit ?? bu)
                .Select(g => new ExponentiatedUnit(g.Key, g.Sum(bu => (bu as ExponentiatedUnit)?.Exponent ?? 1)))
                .ToArray();

            if (collapsedBaseUnits.Length == 1)
                return collapsedBaseUnits[0];

            return new ComposedUnit(collapsedBaseUnits);
        }

        public Unit[] BaseUnits { get; }

        public override string Name => null;

        public override Quantity Quantity => null;

        public override double StandardAmount => BaseUnits.Select(bu => bu.StandardAmount).Aggregate((a, b) => a * b);

        public override string Symbol => string.Join("*", BaseUnits.Select(bu => bu.Symbol));

        public override string UISymbol => string.Join("*", BaseUnits.Select(bu => bu.UISymbol));

        public override bool IsMetric => BaseUnits.All(bu => bu.IsMetric);
    }
}