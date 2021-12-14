using System;

namespace OpenCAD.Modules.Math.Operations
{
    public abstract class BinaryOperation<TOperand1, TOperand2, TResult> : MathOperation<TOperand1, TOperand2, TResult>
    {
        protected BinaryOperation(Func<TOperand1, TOperand2, TResult> func) : base(func) { }
    }
}
