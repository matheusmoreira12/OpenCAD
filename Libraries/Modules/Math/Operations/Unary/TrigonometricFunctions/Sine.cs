using System;

namespace OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions
{
    public class Sine<TOperand, TResult> : NAryOperation<TOperand, TResult>
    {
        public Sine(Func<TOperand, TResult> func) : base(func) { }

        public override OperationType OperationType => OperationType.Sine;
    }
}
