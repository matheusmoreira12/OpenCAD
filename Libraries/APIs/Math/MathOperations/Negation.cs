using System;

namespace OpenCAD.APIs.Math
{
    public class Negation<T1, TR> : MathOperation<T1, TR>
    {
        public Negation(Func<T1, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Negation;
    }

    public class Negation<T1, T2, TR> : MathOperation<T1, T2, TR>
    {
        public Negation(Func<T1, T2, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Negation;
    }

    public class Negation<T1, T2, T3, TR> : MathOperation<T1, T2, T3, TR>
    {
        public Negation(Func<T1, T2, T3, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Negation;
    }

    public class Negation<T1, T2, T3, T4, TR> : MathOperation<T1, T2, T3, T4, TR>
    {
        public Negation(Func<T1, T2, T3, T4, TR> func) : base(func) { }

        public override MathOperationType OperationType => MathOperationType.Negation;
    }
}