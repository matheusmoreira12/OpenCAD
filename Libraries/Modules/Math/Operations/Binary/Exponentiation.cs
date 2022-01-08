using System;

namespace OpenCAD.Modules.Math.Operations.Binary
{
    public sealed class Exponentiation<TOperand1, TOperand2, TResult> : NAryOperation<TOperand1, TOperand2, TResult>
    {
        public Exponentiation(Func<TOperand1, TOperand2, TResult> func) : base(func) { }

        public override OperationType OperationType => OperationType.Exponentiation;
    }
}
