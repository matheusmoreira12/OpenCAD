namespace OpenCAD.APIs.Math
{
    public sealed class MathFormatToken : MathExpressionToken
    {
        public MathFormatToken(MathFormatTokenType type)
        {
            Type = type;
        }

        public MathFormatTokenType Type { get; }
    }
}
