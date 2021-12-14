using System;

namespace OpenCAD.Modules.Math.Operations.Unary
{
    internal class Negation<T1, TR> : UnaryOperation<T1, TR>
    {
        public Negation(Func<T1, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Negation;
    }
}