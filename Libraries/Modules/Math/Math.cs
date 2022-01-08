using OpenCAD.Modules.Math.Operations;
using OpenCAD.Modules.Math.Operations.Binary;
using OpenCAD.Modules.Math.Operations.Unary;
using OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions;

namespace OpenCAD.Modules.Math
{
    public static partial class Math
    {
        static Math()
        {
            registerDefaultOperations();
        }

        static void registerDefaultOperations()
        {
            MathOperationManager.RegisterMany(new NAryOperation[] {
                new Positivation<double, double>(positivateDouble),
                new Negation<double, double>(negateDouble),
                new Sine<double, double>(getSineDouble),
                new Cosine<double, double>(getCosineDouble),
                new Tangent<double, double>(getTangentDouble),
                new Cosecant<double, double>(getCosecantDouble),
                new Secant<double, double>(getSecantDouble),
                new Cotangent<double, double>(getCotangentDouble),
                new Addition<double, double, double>(sumDoubles),
                new Subtraction<double, double, double>(subtractDoubles),
                new Multiplication<double, double, double>(multiplyDoubles),
                new Division<double, double, double>(divideDoubles),
                new Exponentiation<double, double, double>(exponentiateDouble),
            });
        }

        #region Default Double Operators
        private static double positivateDouble(double a) => a;
        private static double negateDouble(double a) => -a;
        private static double getSineDouble(double a) => System.Math.Sin(a);
        private static double getCosineDouble(double a) => System.Math.Cos(a);
        private static double getTangentDouble(double a) => System.Math.Tan(a);
        private static double getCosecantDouble(double a) => 1 / System.Math.Sin(a);
        private static double getSecantDouble(double a) => 1 / System.Math.Cos(a);
        private static double getCotangentDouble(double a) => 1 / System.Math.Tan(a);
        private static double sumDoubles(double a, double b) => a + b;
        private static double subtractDoubles(double a, double b) => a - b;
        private static double multiplyDoubles(double a, double b) => a * b;
        private static double divideDoubles(double a, double b) => a / b;
        private static double exponentiateDouble(double a, double b) => System.Math.Pow(a, b);
        #endregion
        public static TR Positivate<T1, TR>(T1 a)
            => (TR)MathOperationManager.Get(OperationType.Positivation, new [] { typeof(T1) }, typeof(TR))?.Execute(a);

        public static TR Negate<T1, TR>(T1 a)
            => (TR)MathOperationManager.Get(OperationType.Negation, new[] { typeof(T1) }, typeof(TR))?.Execute(a);

        public static TR Sine<T1, TR>(T1 a)
            => (TR)MathOperationManager.Get(OperationType.Sine, new[] { typeof(T1) }, typeof(TR))?.Execute(a);

        public static TR Cosine<T1, TR>(T1 a)
            => (TR)MathOperationManager.Get(OperationType.Cosine, new[] { typeof(T1) }, typeof(TR))?.Execute(a);

        public static TR Tangent<T1, TR>(T1 a)
            => (TR)MathOperationManager.Get(OperationType.Tangent, new[] { typeof(T1) }, typeof(TR))?.Execute(a);

        public static TR Cosecant<T1, TR>(T1 a)
            => (TR)MathOperationManager.Get(OperationType.Cosecant, new[] { typeof(T1) }, typeof(TR))?.Execute(a);

        public static TR Secant<T1, TR>(T1 a)
            => (TR)MathOperationManager.Get(OperationType.Secant, new[] { typeof(T1) }, typeof(TR))?.Execute(a);

        public static TR Cotangent<T1, TR>(T1 a)
            => (TR)MathOperationManager.Get(OperationType.Cotangent, new[] { typeof(T1) }, typeof(TR))?.Execute(a);

        public static TR Add<T1, T2, TR>(T1 a, T2 b)
            => (TR)MathOperationManager.Get(OperationType.Addition, new[] {
                typeof(T1),
                typeof(T2),
            }, typeof(TR))?.Execute(a, b);

        public static TR Subtract<T1, T2, TR>(T1 a, T2 b)
            => (TR)MathOperationManager.Get(OperationType.Subtraction, new[] {
                typeof(T1),
                typeof(T2),
            }, typeof(TR)).Execute(a, b);

        public static TR Multiply<T1, T2, TR>(T1 a, T2 b)
            => (TR)MathOperationManager.Get(OperationType.Multiplication, new[] {
                typeof(T1),
                typeof(T2),
            }, typeof(TR))?.Execute(a, b);

        public static TR Divide<T1, T2, TR>(T1 a, T2 b)
            => (TR)MathOperationManager.Get(OperationType.Division, new[] {
                typeof(T1),
                typeof(T2),
            }, typeof(TR))?.Execute(a, b);

        public static TR Exponentiate<T1, T2, TR>(T1 a, T2 b)
            => (TR)MathOperationManager.Get(OperationType.Exponentiation, new[] {
                typeof(T1),
                typeof(T2),
            }, typeof(TR))?.Execute(a, b);

        public static object Positivate<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Positivation, new[] { typeof(T1) })?.Execute(a);

        public static object Negate<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Negation, new[] { typeof(T1) })?.Execute(a);

        public static object Sine<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Sine, new[] { typeof(T1) })?.Execute(a);

        public static object Cosine<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Cosine, new[] { typeof(T1) })?.Execute(a);

        public static object Tangent<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Tangent, new[] { typeof(T1) })?.Execute(a);

        public static object Cosecant<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Cosecant, new[] { typeof(T1) })?.Execute(a);

        public static object Secant<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Secant, new[] { typeof(T1) })?.Execute(a);

        public static object Cotangent<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Cotangent, new[] { typeof(T1) })?.Execute(a);

        public static object Add<T1, T2>(T1 a, T2 b)
            => MathOperationManager.Get(OperationType.Addition, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);

        public static object Subtract<T1, T2>(T1 a, T2 b) =>
            MathOperationManager.Get(OperationType.Subtraction, new[] {
                typeof(T1),
                typeof(T2),
            }).Execute(a, b);

        public static object Multiply<T1, T2>(T1 a, T2 b)
            => MathOperationManager.Get(OperationType.Multiplication, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);

        public static object Divide<T1, T2>(T1 a, T2 b)
            => MathOperationManager.Get(OperationType.Division, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);

        public static object Exponentiate<T1, T2>(T1 a, T2 b)
            => MathOperationManager.Get(OperationType.Exponentiation, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);
    }
}