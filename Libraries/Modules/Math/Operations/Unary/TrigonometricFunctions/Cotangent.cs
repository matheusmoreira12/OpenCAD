using System;

namespace OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions
{
    public class Cotangent<TOperand, TResult> : NAryOperation<TOperand, TResult>
    {
        public Cotangent(Func<TOperand, TResult> func) : base(func) { }

        public override OperationType OperationType => OperationType.Cotangent;
    }
}
