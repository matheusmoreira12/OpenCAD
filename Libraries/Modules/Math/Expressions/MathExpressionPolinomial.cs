using System;

namespace OpenCAD.Modules.Math.Expressions
{
    public class MathExpressionPolinomial : MathExpressionMember
    {
        public MathExpressionPolinomial(MathExpressionMonomial[] monomials)
        {
            Monomials = monomials ?? throw new ArgumentNullException(nameof(monomials));
        }

        public MathExpressionMonomial[] Monomials { get; }
    }
}
