namespace OpenCAD.APIs.Math
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
