using System;

namespace OpenCAD.Modules.Math.Operations.Binary
{
    public class Exponentiation<TOperand1, TOperand2, TResult> : MathOperation<TOperand1, TOperand2, TResult>
    {
        public Exponentiation(Func<TOperand1, TOperand2, TResult> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Exponentiation;
    }
}
