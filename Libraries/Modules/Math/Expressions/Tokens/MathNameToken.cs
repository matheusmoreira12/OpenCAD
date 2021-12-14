namespace OpenCAD.Modules.Math.Expressions.Tokens
{
    public sealed class MathNameToken : MathExpressionToken
    {
        public MathNameToken(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }
}
