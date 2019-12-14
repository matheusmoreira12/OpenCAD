using System;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class BaseUnit : Unit, IMultipliable<BaseUnit, DerivedUnit>, IMultipliable<DerivedUnit, DerivedUnit>, IExponentiable<double, DerivedUnit>
    {
        public BaseUnit(string name, Quantity quantity, string symbol, string uISymbol = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Quantity = quantity ?? throw new ArgumentNullException(nameof(quantity));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
            UISymbol = uISymbol;
        }

        public override Unit Collapse() => this;

        public override string Name { get; }

        public override Quantity Quantity { get; }

        public override string Symbol { get; }

        public override string UISymbol { get; }

        public DerivedUnit Multiply(BaseUnit value)
        {
            var thisMember = new DerivedUnitExpressionMember(this, 1);
            var valueMember = new DerivedUnitExpressionMember(value, 1);
            var expression = new DerivedUnitExpression(thisMember, valueMember);
            return new DerivedUnit(expression);
        }

        public DerivedUnit Multiply(DerivedUnit value)
        {
            var thisMember = new DerivedUnitExpressionMember(this, 1);
            var expression = new DerivedUnitExpression(new[] { thisMember }.Concat(value.Expression.Members).ToArray());
            return new DerivedUnit(expression);
        }

        public DerivedUnit Exponentiate(double exponent)
        {
            var thisMember = new DerivedUnitExpressionMember(this, exponent);
            var expression = new DerivedUnitExpression(thisMember);
            return new DerivedUnit(expression);
        }
    }
}