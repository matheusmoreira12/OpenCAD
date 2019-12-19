using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class DerivedUnit : Unit
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

        private DerivedUnitExpression getExpression(BaseUnit baseUnit, MetricPrefix prefix, double exponent) => 
            new DerivedUnitExpression(new DerivedUnitExpressionMember(
                baseUnit ?? throw new ArgumentNullException(nameof(baseUnit)), prefix, exponent));


        public DerivedUnitExpression Expression { get; }

        public override string Name => _name ?? generateName();
        private string _name;

        private string generateName() => null;

        public override Quantity Quantity => null;

        public override string Symbol => _symbol ?? generateSymbol();
        private string _symbol;

        private string generateSymbol() => null;

        public override string UISymbol => _uiSymbol ?? generateUISymbol();
        private string _uiSymbol;

        private string generateUISymbol() => null;

        public bool IsNamed;

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
    }
}
