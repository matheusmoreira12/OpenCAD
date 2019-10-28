using System.Linq;
using OpenCAD.Utils;

namespace OpenCAD.APIs.Math
{
    public class MathExpression : MathExpressionGroup
    {
        internal static MathExpression FromTokens(StringToken[] tokens)
        {
            var reader = new StringTokenReader(tokens);

            ///TODO: Read math formula here

            return null;
        }

        public static MathExpression Parse(string value)
        {
            MathExpressionTokenifier tokenifier = new MathExpressionTokenifier(value);

            var tokens = tokenifier.Tokenify().Cast<MathExpressionToken>();

            return FromTokens(tokens.ToArray());
        }

        public MathExpression(MathExpressionMember[] members) : base(members)
        {
        }
    }
}
