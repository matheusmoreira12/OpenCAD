using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
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
}