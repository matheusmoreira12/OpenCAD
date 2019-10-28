using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Unit
    {
        public static BaseUnit Derive(string name, Unit original, double conversionAmount, string symbol, string uiSymbol = null) => 
            new BaseUnit(name, original.Quantity, original.StandardAmount * conversionAmount, symbol, uiSymbol);

        public static Unit Parse(string value)
        {
            IEnumerable<Unit> supportedUnits = Utils.GetSupportedUnits();

            foreach (var unit in supportedUnits)
                if (unit.Symbol == value)
                    return unit;

            throw new KeyNotFoundException("Unable to parse unit. The provided unit/prefix found no matches.");
        }

        public static bool TryParse(string value, out Unit result)
        {
            try
            {
                result = Parse(value);

                return true;
            }
            catch
            {
                result = default;

                return false;
            }
        }

        public Unit()
        {
            Units.SupportedUnits.Add(this);
        }

        public static Unit operator *(Unit a, Unit b) => UnitMath.Multiply(a, b);

        public static Unit operator *(Unit a, MetricPrefix b) => UnitMath.Multiply(a, b);

        public static Unit operator /(Unit a, Unit b) => UnitMath.Divide(a, b);

        public static Unit operator !(Unit a) => UnitMath.Invert(a);

        public abstract Unit Collapse();

        public abstract string Name { get; }

        public abstract Quantity Quantity { get; }

        public abstract double StandardAmount { get; }

        public abstract string Symbol { get; }

        public abstract string UISymbol { get; }

        public abstract MetricSystem MetricSystem { get; }

        public Unit Derive(string name, double conversionAmount, string symbol, string uiSymbol = null) =>
            Derive(name, this, conversionAmount, symbol, uiSymbol);
    }

    public sealed class BaseUnit : Unit
    {
        public BaseUnit(string name, Quantity quantity, double standardAmount, string symbol, string uISymbol = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            StandardAmount = standardAmount;
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uISymbol ?? symbol;
        }

        public override Unit Collapse() => this;

        public override string Name { get; }

        public override Quantity Quantity { get; }

        public override double StandardAmount { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public override MetricSystem MetricSystem { get; }
    }

    public sealed class PrefixedUnit : Unit
    {
        internal PrefixedUnit(Unit baseUnit, MetricPrefix prefix)
        {
            BaseUnit = baseUnit ?? throw new ArgumentNullException(nameof(baseUnit));
            Prefix = prefix ?? throw new ArgumentNullException(nameof(prefix));
        }

        public Unit BaseUnit { get; }

        public MetricPrefix Prefix { get; }

        public override string Name => $"{Prefix.Name} {BaseUnit.Name}";

        public override Quantity Quantity => BaseUnit.Quantity;

        public override double StandardAmount => Prefix.Multiplier * BaseUnit.StandardAmount;

        public override string Symbol => $"{Prefix.Symbol}{BaseUnit.Symbol}";

        public override string UISymbol => $"{Prefix.UISymbol}{BaseUnit.UISymbol}";

        public override Unit Collapse() => this;

        public override MetricSystem MetricSystem => BaseUnit.MetricSystem;
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
                return new ComposedUnit((baseUnit as ComposedUnit).BaseUnits.Select(bu => bu.Power(Exponent).Collapse()).ToArray());

            return new ExponentiatedUnit(baseUnit, Exponent);
        }

        public Unit BaseUnit { get; }

        public double Exponent { get; }

        public override string Name => null;

        public override Quantity Quantity => null;

        public override double StandardAmount => Math.Pow(BaseUnit.StandardAmount, Exponent);

        public override string Symbol => $"{BaseUnit.UISymbol}^{Exponent}";

        public override string UISymbol => $"{BaseUnit.Symbol}^{Exponent}";

        public override MetricSystem MetricSystem => BaseUnit.MetricSystem;
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

        public override MetricSystem MetricSystem => null;
    }
}