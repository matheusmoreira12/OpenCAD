using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using MathAPI = OpenCAD.APIs.Math;

namespace OpenCAD.APIs.Measures
{
    public sealed class DerivedUnit : Unit
    {
        private static DerivedUnitDimension generateDimensionFromBaseUnit(BaseUnit baseUnit, MetricPrefix prefix, double exponent) =>
            new DerivedUnitDimension(new DerivedUnitDimensionMember(baseUnit
                ?? throw new ArgumentNullException(nameof(baseUnit)), prefix, exponent));

        public DerivedUnit(DerivedUnitDimension dimension)
        {
            Dimension = dimension ?? throw new ArgumentNullException(nameof(dimension));
        }

        public DerivedUnit(BaseUnit baseUnit, double exponent)
        {
            Dimension = generateDimensionFromBaseUnit(baseUnit, null, exponent);
        }

        public DerivedUnit(BaseUnit baseUnit, MetricPrefix prefix, double exponent)
        {
            Dimension = generateDimensionFromBaseUnit(baseUnit, prefix, exponent);
        }

        /// <summary>
        /// Gets the dimension of this derived unit.
        /// </summary>
        public DerivedUnitDimension Dimension { get; }

        /// <summary>
        /// Gets the metric system this derived unit belongs to.
        /// </summary>
        public override MetricSystem MetricSystem
        {
            get
            {
                var baseMetricSystems = Dimension.Members.Select(m => m.BaseUnit.MetricSystem).ToList();
                bool hasManyBaseMetricSystems = baseMetricSystems.Any(ms => ms != baseMetricSystems[0]);
                if (hasManyBaseMetricSystems)
                    return null;
                else
                    return baseMetricSystems[0];
            }
        }

        /// <summary>
        /// Gets the quantity this derived unit measures.
        /// </summary>
        public override Quantity Quantity => Dimension.Members.Select(m =>
            (Quantity)MathAPI::Math.Power(m.BaseUnit.Quantity, m.Exponent)).Aggregate((qa, q) => qa * q);

        /// <summary>
        /// Gets the name of this derived unit.
        /// </summary>
        public override string Name => null;

        /// <summary>
        /// Gets the symbol of this derived unit.
        /// </summary>
        public override string Symbol
        {
            get
            {
                {
                    bool hasMultipleBases = Dimension.Members.Length > 1;
                    var builder = new StringBuilder();

                    if (hasMultipleBases)
                        builder.Append("(");

                    for (int i = 0; i < Dimension.Members.Length; i++)
                    {
                        if (i > 0)
                            builder.Append("*");

                        var member = Dimension.Members[i];
                        builder.Append($"{member.Prefix?.Symbol}{member.BaseUnit.Symbol}^{member.Exponent}");
                    }

                    if (hasMultipleBases)
                        builder.Append(")");

                    return builder.ToString();
                }
            }
        }

        /// <summary>
        /// Gets the UI symbol of this derived unit.
        /// </summary>
        public override string UISymbol => null;

        /// <summary>
        /// Gets this derived unit in its collapsed form.
        /// </summary>
        /// <returns>This derived unit in its collapsed form.</returns>
        public override Unit Collapse()
        {
            var members = Dimension.Members
                .Select(m => (bu: m.BaseUnit.Collapse(), p: m.Prefix, e: m.Exponent))
                .Where(mt => mt.bu != null)
                .GroupBy(mt => (mt.bu, mt.p))
                .Select(g => new DerivedUnitDimensionMember(g.Key.bu, g.Key.p, g.Sum(m => m.e)))
                .Where(m => m.Exponent != 0)
                .ToArray();
            DerivedUnitDimensionMember firstMember;
            if (members.Length == 0)
                return null;
            else if (members.Length == 1 && (firstMember = members.First()).Exponent == 1)
                return firstMember.BaseUnit;
            else
                return new DerivedUnit(new DerivedUnitDimension(members));
        }

        public override void Dispose() { }
    }
}
