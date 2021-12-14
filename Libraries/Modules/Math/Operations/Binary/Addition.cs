using System;

namespace OpenCAD.Modules.Math.Operations.Binary
{
    public class Addition<T1, T2, TR> : MathOperation<T1, T2, TR>
    {
        public Addition(Func<T1, T2, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Addition;
    }
}