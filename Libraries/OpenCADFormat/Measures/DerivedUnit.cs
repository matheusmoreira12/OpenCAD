using System;

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
        }

        public DerivedUnit(string name, string symbol, DerivedUnitExpression expression)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = null;
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public DerivedUnit(string name, string symbol, string uiSymbol, DerivedUnitExpression expression)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            _uiSymbol = uiSymbol ?? throw new ArgumentNullException(nameof(uiSymbol));
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public DerivedUnitExpression Expression { get; }

        public override string Name => _name;
        private readonly string _name;

        public override Quantity Quantity => throw new NotImplementedException();

        public override string Symbol => _symbol;
        private readonly string _symbol;

        public override string UISymbol => _uiSymbol;
        private readonly string _uiSymbol;

        public override Unit Collapse()
        {
            throw new NotImplementedException();
        }
    }
}
