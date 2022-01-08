using System;

namespace OpenCAD.Modules.Math.Operations.Binary
{
    public sealed class Addition<T1, T2, TR> : NAryOperation<T1, T2, TR>
    {
        public Addition(Func<T1, T2, TR> func) : base(func) { }

        public override OperationType OperationType => OperationType.Addition;
    }
}