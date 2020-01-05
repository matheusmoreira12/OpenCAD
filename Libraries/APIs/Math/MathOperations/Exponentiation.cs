using System;

namespace OpenCAD.APIs.Math
{
    public class Exponentiation<T1, TR> : MathOperation<T1, TR>
    {
        public Exponentiation(Func<T1, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Exponentiation;
    }

    public class Exponentiation<T1, T2, TR> : MathOperation<T1, T2, TR>
    {
        public Exponentiation(Func<T1, T2, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Exponentiation;
    }

    public class Exponentiation<T1, T2, T3, TR> : MathOperation<T1, T2, T3, TR>
    {
        public Exponentiation(Func<T1, T2, T3, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Exponentiation;
    }

    public class Exponentiation<T1, T2, T3, T4, TR> : MathOperation<T1, T2, T3, T4, TR>
    {
        public Exponentiation(Func<T1, T2, T3, T4, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Exponentiation;
    }
}