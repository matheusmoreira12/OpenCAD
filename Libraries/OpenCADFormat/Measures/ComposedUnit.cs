using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
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

        public override string UISymbol => string.Join("*", BaseUnits.Select(bu => bu.UISymbol ?? bu.Symbol));

        public override MetricSystem MetricSystem => null;
    }
}