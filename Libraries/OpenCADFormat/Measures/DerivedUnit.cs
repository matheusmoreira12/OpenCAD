using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class DerivedUnit : Unit, IMultipliable<BaseUnit, DerivedUnit>, IMultipliable<DerivedUnit, DerivedUnit>,
        IExponentiable<double, DerivedUnit>
    {
        public DerivedUnit(DerivedUnitExpression expression)
        {
            _name = null;
            _symbol = null;
            _uiSymbol = null;
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            IsNamed = false;
        }

        public DerivedUnit(string name, string symbol, DerivedUnitExpression expression)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = null;
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            IsNamed = true;
        }

        public DerivedUnit(string name, string symbol, string uiSymbol, DerivedUnitExpression expression)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = uiSymbol ?? throw new ArgumentNullException(nameof(uiSymbol));
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
            IsNamed = true;
        }

        public DerivedUnitExpression Expression { get; }

        public override string Name => _name ?? generateName();
        private string generateName()
        {
            var memberStrs = new List<string> { };
            foreach (var member in Expression.Members)
                memberStrs.Add($"({member.Prefix?.Name}{member.BaseUnit.Name})^{member.Exponent}");
            return string.Join("*", memberStrs);
        }

        private readonly string _name;

        public override Quantity Quantity => null;

        public override string Symbol => _symbol;
        private readonly string _symbol;

        public override string UISymbol => _uiSymbol;
        private readonly string _uiSymbol;

        public readonly bool IsNamed;

        public override Unit Collapse()
        {
            var members = Expression.Members
                .GroupBy(m => (baseUnit: m.BaseUnit.Collapse(), prefix: m.Prefix))
                .Select(g => new DerivedUnitExpressionMember(g.Key.baseUnit, g.Key.prefix, g.Sum(m => m.Exponent)))
                .Where(m => m.Exponent != 0)
                .ToArray();
            var expression = new DerivedUnitExpression(members);
            return new DerivedUnit(expression);
        }

        DerivedUnit IMultipliable<BaseUnit, DerivedUnit>.Multiply(BaseUnit value)
        {
            var valueMember = new DerivedUnitExpressionMember(value, 1);
            var expression = new DerivedUnitExpression(Expression.Members.Concat(new[] { valueMember }).ToArray());
            return new DerivedUnit(expression);
        }

        DerivedUnit IMultipliable<DerivedUnit, DerivedUnit>.Multiply(DerivedUnit value)
        {
            var expression = new DerivedUnitExpression(Expression.Members.Concat(value.Expression.Members).ToArray());
            return new DerivedUnit(expression);
        }

        DerivedUnit IExponentiable<double, DerivedUnit>.Exponentiate(double exponent)
        {
            var members = Expression.Members.Select(m => new DerivedUnitExpressionMember(m.BaseUnit, m.Prefix,
                m.Exponent * exponent)).ToArray();
            var expression = new DerivedUnitExpression(members);
            return new DerivedUnit(expression);
        }
    }
}
