using System;

namespace OpenCAD.Modules.Math.Operations.Binary
{
    public sealed class NthRoot<T1, T2, TR> : NAryOperation<T1, T2, TR>
    {
        public NthRoot(Func<T1, T2, TR> func) : base(func) { }

        public override OperationType OperationType => OperationType.NthRoot;
    }
}