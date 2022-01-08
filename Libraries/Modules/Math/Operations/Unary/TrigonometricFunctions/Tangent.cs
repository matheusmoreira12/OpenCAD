using System;

namespace OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions
{
    public class Tangent<TOperand, TResult> : NAryOperation<TOperand, TResult>
    {
        public Tangent(Func<TOperand, TResult> func) : base(func) { }

        public override OperationType OperationType => OperationType.Tangent;
    }
}
