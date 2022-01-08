using System;

namespace OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions
{
    public class Secant<TOperand, TResult> : NAryOperation<TOperand, TResult>
    {
        public Secant(Func<TOperand, TResult> func) : base(func) { }

        public override OperationType OperationType => OperationType.Secant;
    }
}
