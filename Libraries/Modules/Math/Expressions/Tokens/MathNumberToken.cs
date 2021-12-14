﻿namespace OpenCAD.Modules.Math.Expressions.Tokens
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
