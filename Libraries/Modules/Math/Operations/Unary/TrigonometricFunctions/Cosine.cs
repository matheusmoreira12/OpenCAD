using System;

namespace OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions
{
    public class Cosine<TOperand, TResult> : NAryOperation<TOperand, TResult>
    {
        public Cosine(Func<TOperand, TResult> func) : base(func) { }

        public override OperationType OperationType => OperationType.Cosine;
    }
}
