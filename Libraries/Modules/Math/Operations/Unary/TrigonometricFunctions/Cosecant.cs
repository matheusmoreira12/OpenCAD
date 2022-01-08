using System;

namespace OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions
{
    public class Cosecant<TOperand, TResult> : NAryOperation<TOperand, TResult>
    {
        public Cosecant(Func<TOperand, TResult> func) : base(func) { }

        public override OperationType OperationType => OperationType.Cosecant;
    }
}
