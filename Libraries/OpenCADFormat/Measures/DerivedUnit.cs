using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class DerivedUnit : Unit
    {
        public DerivedUnit(DerivedUnitExpression expression)
        {
            Name = null;
            Symbol = null;
            UISymbol = null;
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public DerivedUnit(string name, string symbol, DerivedUnitExpression expression)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = null;
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public DerivedUnit(string name, string symbol, string uiSymbol, DerivedUnitExpression expression)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uiSymbol ?? throw new ArgumentNullException(nameof(uiSymbol));
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public DerivedUnitExpression Expression { get; }

        public override string Name { get; }

        public override Quantity Quantity => throw new NotImplementedException();

        public override double StandardAmount => throw new NotImplementedException();

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public override MetricSystem MetricSystem => throw new NotImplementedException();

        public override Unit Collapse()
        {
            throw new NotImplementedException();
        }
    }
}
