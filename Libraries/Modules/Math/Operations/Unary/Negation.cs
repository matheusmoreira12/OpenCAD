using System;

namespace OpenCAD.Modules.Math.Operations.Unary
{
    public sealed class Negation<T1, TR> : NAryOperation<T1, TR>
    {
        public Negation(Func<T1, TR> func) : base(func) { }

        public override OperationType OperationType => OperationType.Negation;
    }
}