using System;

namespace OpenCAD.APIs.Math.Operations
{
    public class Negation<T1, TR> : MathOperation<T1, TR>
    {
        public Negation(Func<T1, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Negation;
    }
}