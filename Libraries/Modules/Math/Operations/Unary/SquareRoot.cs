using System;

namespace OpenCAD.Modules.Math.Operations.Unary
{
    public sealed class SquareRoot<T1, TR> : NAryOperation<T1, TR>
    {
        public SquareRoot(Func<T1, TR> func) : base(func) { }

        public override OperationType OperationType => OperationType.SquareRoot;
    }
}