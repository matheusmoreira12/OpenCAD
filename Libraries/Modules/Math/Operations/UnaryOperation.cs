using System;

namespace OpenCAD.Modules.Math.Operations
{
    public abstract class UnaryOperation<TOperand, TResult> : MathOperation<TOperand, TResult>
    {
        protected UnaryOperation(Func<TOperand, TResult> func) : base(func) { }
    }
}
