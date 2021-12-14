using System;

namespace OpenCAD.Modules.Math.Operations
{
    public abstract class TrigonometricFunction<TOperand, TResult> : MathOperation<TOperand, TResult>
    {
        protected TrigonometricFunction(Func<TOperand, TResult> func) : base(func) { }
    }
}
