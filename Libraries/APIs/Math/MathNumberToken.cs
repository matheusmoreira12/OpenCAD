namespace OpenCAD.APIs.Math
{
    public sealed class MathNumberToken : MathExpressionToken
    {
        public MathNumberToken(double value)
        {
            Value = value;
        }

        public double Value { get; }
    }
}
