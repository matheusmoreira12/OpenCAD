using System;

namespace OpenCAD.Modules.Math.Operations.Unary
{
    public sealed class Positivation<T1, TR> : NAryOperation<T1, TR>
    {
        public Positivation(Func<T1, TR> func) : base(func) { }

        public override OperationType OperationType => OperationType.Positivation;
    }
}