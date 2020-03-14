﻿using System;
using System.Linq;
using System.Text;

using MathAPI = OpenCAD.APIs.Math;

namespace OpenCAD.APIs.Measures
{
    public sealed class DerivedUnit : Unit, IEquatable<DerivedUnit>
    {
        public DerivedUnit(DerivedUnitDimension expression)
        {
            _name = null;
            _symbol = null;
            _uiSymbol = null;
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            IsNamed = false;
        }

        public DerivedUnit(string name, string symbol, DerivedUnitDimension expression)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = null;
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            IsNamed = true;
        }

        public DerivedUnit(string name, string symbol, string uiSymbol, DerivedUnitDimension expression)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = uiSymbol;
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            IsNamed = true;
        }

        public DerivedUnit(BaseUnit baseUnit, double exponent)
        {
            _name = null;
            _symbol = null;
            _uiSymbol = null;
            Expression = getExpression(baseUnit, null, exponent);
            IsNamed = false;
        }

        public DerivedUnit(string name, string symbol, BaseUnit baseUnit, double exponent)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = null;
            Expression = getExpression(baseUnit, null, exponent);
            IsNamed = true;
        }

        public DerivedUnit(string name, string symbol, string uiSymbol, BaseUnit baseUnit, double exponent)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = uiSymbol;
            Expression = getExpression(baseUnit, null, exponent);
            IsNamed = true;
        }

        public DerivedUnit(BaseUnit baseUnit, MetricPrefix prefix, double exponent)
        {
            _name = null;
            _symbol = null;
            _uiSymbol = null;
            Expression = getExpression(baseUnit, prefix, exponent);
            IsNamed = false;
        }

        public DerivedUnit(string name, string symbol, BaseUnit baseUnit, MetricPrefix prefix, double exponent)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = null;
            Expression = getExpression(baseUnit, prefix, exponent);
            IsNamed = true;
        }

        public DerivedUnit(string name, string symbol, string uiSymbol, BaseUnit baseUnit, MetricPrefix prefix, double exponent)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = uiSymbol;
            Expression = getExpression(baseUnit, prefix, exponent);
            IsNamed = true;
        }

        private DerivedUnitDimension getExpression(BaseUnit baseUnit, MetricPrefix prefix, double exponent) =>
            new DerivedUnitDimension(new DerivedUnitDimensionMember(baseUnit
                ?? throw new ArgumentNullException(nameof(baseUnit)), prefix, exponent));

        public DerivedUnitDimension Expression { get; }

        public override string Name => _name ?? generateName();
        private string _name;

        private string generateName() => null;

        public override Quantity Quantity => Expression.Members.Select(m =>
            (Quantity)MathAPI::Math.Power(m.BaseUnit.Quantity, m.Exponent)).Aggregate((qa, q) => qa * q);

        public override string Symbol => _symbol ?? generateSymbol();
        private string _symbol;

        private string generateSymbol()
        {
            bool hasMultipleBases = Expression.Members.Length > 1;
            var builder = new StringBuilder();

            if (hasMultipleBases)
                builder.Append("(");

            for (int i = 0; i < Expression.Members.Length; i++)
            {
                if (i > 0)
                    builder.Append("*");

                var member = Expression.Members[i];
                builder.Append($"{member.Prefix?.Symbol}{member.BaseUnit.Symbol}^{member.Exponent}");
            }

            if (hasMultipleBases)
                builder.Append(")");

            return builder.ToString();
        }

        public override string UISymbol => _uiSymbol ?? generateUISymbol();
        private string _uiSymbol;

        private string generateUISymbol() => null;

        public bool IsNamed;

        public override Unit Collapse()
        {
            var members = Expression.Members
                .GroupBy(m => (baseUnit: m.BaseUnit.Collapse(), prefix: m.Prefix))
                .Select(g => new DerivedUnitDimensionMember(g.Key.baseUnit, g.Key.prefix, g.Sum(m => m.Exponent)))
                .Where(m => m.Exponent != 0)
                .ToArray();
            var expression = new DerivedUnitDimension(members);
            return new DerivedUnit(expression);
        }

        bool IEquatable<DerivedUnit>.Equals(DerivedUnit other)
        {
            return (this as IEquatable<Unit>).Equals(other)
                && (Expression as IEquatable<DerivedUnitDimension>).Equals(other.Expression);
        }
    }
}
