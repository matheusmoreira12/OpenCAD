using System;

namespace OpenCAD.APIs.Math.Operations
{
    public class Exponentiation<T1, T2, TR> : MathOperation<T1, T2, TR>
    {
        public Exponentiation(Func<T1, T2, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Exponentiation;
    }
}