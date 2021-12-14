using System;

namespace OpenCAD.Modules.Math.Operations.Unary
{
    internal class Positivation<T1, TR> : UnaryOperation<T1, TR>
    {
        public Positivation(Func<T1, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Positivation;
    }
}