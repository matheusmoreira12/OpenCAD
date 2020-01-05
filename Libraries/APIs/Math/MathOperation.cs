using System;
using System.Linq;

namespace OpenCAD.APIs.Math
{
    public enum MathOperationType { Addition, Negation, Multiplication, Exponentiation }

    public abstract class MathOperation
    {
        public static Type[] GetOperandTypes(MathOperation operation)
        {
            var typeArgs = operation.GetType().GenericTypeArguments;
            return typeArgs.Take(typeArgs.Length - 1).ToArray();
        }

        public static Type GetResultType(MathOperation operation)
        {
            var typeArgs = operation.GetType().GenericTypeArguments;
            return typeArgs.Last();
        }

        public abstract MathOperationType OperationType { get; }

        public abstract object Execute(params object[] operands);
    }

    public abstract class MathOperation<T1, TR> : MathOperation
    {
        protected MathOperation(Func<T1, TR> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<T1, TR> func { get; }

        public override object Execute(params object[] operands) => func((T1)operands[0]);
    }

    public abstract class MathOperation<T1, T2, TR> : MathOperation
    {
        protected MathOperation(Func<T1, T2, TR> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<T1, T2, TR> func { get; }

        public override object Execute(params object[] operands) => func((T1)operands[0],
            (T2)operands[1]);
    }

    public abstract class MathOperation<T1, T2, T3, TR> : MathOperation
    {
        protected MathOperation(Func<T1, T2, T3, TR> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<T1, T2, T3, TR> func { get; }

        public override object Execute(params object[] operands) => func((T1)operands[0],
            (T2)operands[1], (T3)operands[2]);
    }

    public abstract class MathOperation<T1, T2, T3, T4, TR> : MathOperation
    {
        protected MathOperation(Func<T1, T2, T3, T4, TR> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<T1, T2, T3, T4, TR> func { get; }

        public override object Execute(params object[] operands) => func((T1)operands[0],
            (T2)operands[1], (T3)operands[2], (T4)operands[3]);
    }
}