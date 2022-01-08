using System;

namespace OpenCAD.Modules.Math.Operations.Binary
{
    public sealed class Subtraction<T1, T2, TR> : NAryOperation<T1, T2, TR>
    {
        public Subtraction(Func<T1, T2, TR> func) : base(func) { }

        public override OperationType OperationType => OperationType.Subtraction;
    }
}