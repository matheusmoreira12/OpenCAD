using System;

namespace OpenCAD.APIs.Math.Operations
{
    public class Subtraction<T1, T2, TR> : MathOperation<T1, T2, TR>
    {
        public Subtraction(Func<T1, T2, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Subtraction;
    }
}