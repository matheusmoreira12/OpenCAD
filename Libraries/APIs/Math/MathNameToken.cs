namespace OpenCAD.APIs.Math
{
    public sealed class MathNameToken : MathExpressionToken
    {
        public MathNameToken(string value)
        {
            Value = value;
        }

        protected MathNameToken()
        {
        }

        public string Value { get; }
    }
}
