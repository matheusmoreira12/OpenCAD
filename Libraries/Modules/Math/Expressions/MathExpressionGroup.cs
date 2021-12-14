using System;

namespace OpenCAD.Modules.Math.Expressions
{
    public class MathExpressionGroup : MathExpressionMember
    {
        public MathExpressionMember[] Members;

        public MathExpressionGroup(MathExpressionMember[] members)
        {
            Members = members ?? throw new ArgumentNullException(nameof(members));
        }
    }
}
