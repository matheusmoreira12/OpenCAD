namespace OpenCAD.OpenCADFormat.Measures
{
    public static class Math
    {
        public static object Power(object a, object b) =>
            MathOperationManager.Get(MathOperationType.Exponentiation, a.GetType(), b.GetType())?.Execute(a, b);

        public static object Multiply(object a, object b) =>
            MathOperationManager.Get(MathOperationType.Multiplication, a.GetType(), b.GetType())?.Execute(a, b);

        public static object Divide(object a, object b) => Multiply(a, Power(b, -1.0));

        public static object Add(object a, object b) =>
            MathOperationManager.Get(MathOperationType.Addition, a.GetType(), b.GetType())?.Execute(a, b);

        public static object Negate(object a) =>
            MathOperationManager.Get(MathOperationType.Negation, a.GetType())?.Execute(a);

        public static object Subtract(object a, object b) => Add(a, Negate(b));

        public static TResult Power<TOperandA, TOperandB, TResult>(TOperandA a, TOperandB b) =>
            (TResult)Power(a, b);

        public static TResult Square<TOperand, TResult>(TOperand value) => (TResult)Power(value, 2.0);

        public static TResult Cube<TOperand, TResult>(TOperand value) => (TResult)Power(value, 3.0);

        public static TResult SquareRoot<TOperand, TResult>(TOperand value) => (TResult)Power(value, 1.0 / 2.0);

        public static TResult CubicRoot<TOperand, TResult>(TOperand value) => (TResult)Power(value, 1.0 / 3.0);

        public static TResult Invert<TOperand, TResult>(TOperand value) => (TResult)Power(value, -1.0);

        public static TResult Multiply<TOperandA, TOperandB, TResult>(TOperandA a, TOperandB b) =>
            (TResult)Multiply(a, b);

        public static TResult Divide<TOperandA, TOperandB, TResult>(TOperandA a, TOperandB b) =>
            (TResult)Divide(a, b);

        public static TResult Add<TOperandA, TOperandB, TResult>(TOperandA a, TOperandB b) =>
            (TResult)Add(a, b);

        public static TResult Negate<TOperand, TResult>(TOperand a) => (TResult)Negate(a);
    }
}
