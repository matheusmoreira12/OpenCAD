using OpenCAD.Modules.Math.Operations;

namespace OpenCAD.Modules.Math
{
    public static partial class Math
    {
        public static object Positivate(this object a)
            => MathOperationManager.Get(MathOperationType.Negation, a.GetType())?.Execute(a);

        public static object Negate(object a)
            => MathOperationManager.Get(MathOperationType.Negation, a.GetType())?.Execute(a);

        public static object Add(object a, object b)
            => MathOperationManager.Get(MathOperationType.Addition, a.GetType(), b.GetType())?.Execute(a, b);

        public static object Subtract(object a, object b) =>
            MathOperationManager.Get(MathOperationType.Subtraction, a.GetType(), b.GetType()).Execute(a, b);

        public static object Multiply(object a, object b)
            => MathOperationManager.Get(MathOperationType.Multiplication, a.GetType(), b.GetType())?.Execute(a, b);

        public static object Divide(object a, object b)
            => MathOperationManager.Get(MathOperationType.Division, a.GetType(), b.GetType())?.Execute(a, b);

        public static object Exponentiate(object a, object b)
            => MathOperationManager.Get(MathOperationType.Exponentiation, a.GetType(), b.GetType())?.Execute(a, b);
    }
}