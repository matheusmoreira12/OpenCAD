using System;

namespace OpenCAD.Modules.Math.Operations.Unary
{
    public sealed class Modulus<T1, TR> : NAryOperation<T1, TR>
    {
        public Modulus(Func<T1, TR> func) : base(func) { }

        public override OperationType OperationType => OperationType.Modulus;
    }
}