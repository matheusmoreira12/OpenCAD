using System;

namespace OpenCAD.APIs.Math
{
    public class MathExpressionMonomial : MathExpressionMember
    {
        public MathExpressionMonomial(double multiplier, string variableName, double power)
        {
            Multiplier = multiplier;
            VariableName = variableName ?? throw new ArgumentNullException(nameof(variableName));
            Power = power;
        }

        public double Multiplier { get; }
        public string VariableName { get; }
        public double Power { get; }
    }
}
