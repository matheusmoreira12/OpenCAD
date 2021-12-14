namespace OpenCAD.Modules.Math.Expressions.Tokens
{
    public sealed class MathOperatorToken : MathExpressionToken
    {
        public MathOperatorToken(MathOperatorTokenType type)
        {
            Type = type;
        }

        public MathOperatorTokenType Type { get; }
    }
}
