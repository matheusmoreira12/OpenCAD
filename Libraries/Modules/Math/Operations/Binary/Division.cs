using System;

namespace OpenCAD.Modules.Math.Operations.Binary
{
    public sealed class Division<T1, T2, TR> : NAryOperation<T1, T2, TR>
    {
        public Division(Func<T1, T2, TR> func) : base(func) { }

        public override OperationType OperationType => OperationType.Division;
    }
}