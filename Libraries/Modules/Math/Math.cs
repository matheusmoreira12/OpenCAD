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

        #region Default Double Operations
        static void registerDefaultOperations()
        {
            MathOperationManager.RegisterMany(new NAryOperation[] {
                #region Arithmetic Unary Operators
                new Negation<double, double>(negateDouble),
                new Modulus<double, double>(getModulusDouble),
                #endregion

                #region Arithmetic Binary Operators
                new Addition<double, double, double>(sumDoubles),
                new Subtraction<double, double, double>(subtractDoubles),
                new Multiplication<double, double, double>(multiplyDoubles),
                new Division<double, double, double>(divideDoubles),
                new Exponentiation<double, double, double>(exponentiateDouble),
                new NthRoot<double, double, double>(getNthRootDouble),
                #endregion
                
                #region Trigonometric Unary Operators
                new Sine<double, double>(getSineDouble),
                new Cosine<double, double>(getCosineDouble),
                new Tangent<double, double>(getTangentDouble),
                new Cosecant<double, double>(getCosecantDouble),
                new Secant<double, double>(getSecantDouble),
                new Cotangent<double, double>(getCotangentDouble),
                #endregion
            });
        }

        #region Arithmetic Unary Operators
        private static double negateDouble(double a) => -a;
        private static double getModulusDouble(double a) => a > 0 ? a : -a;
        #endregion

        #region Arithmetic Binary Operators
        private static double sumDoubles(double a, double b) => a + b;
        private static double subtractDoubles(double a, double b) => a - b;
        private static double multiplyDoubles(double a, double b) => a * b;
        private static double divideDoubles(double a, double b) => a / b;
        private static double exponentiateDouble(double a, double b) => System.Math.Pow(a, b);
        private static double getNthRootDouble(double a, double b) => System.Math.Pow(a, 1 / b);
        #endregion

        #region Trigonometric Unary Operators
        private static double getSineDouble(double a) => System.Math.Sin(a);
        private static double getCosineDouble(double a) => System.Math.Cos(a);
        private static double getTangentDouble(double a) => System.Math.Tan(a);
        private static double getCosecantDouble(double a) => 1 / System.Math.Sin(a);
        private static double getSecantDouble(double a) => 1 / System.Math.Cos(a);
        private static double getCotangentDouble(double a) => 1 / System.Math.Tan(a);
        #endregion
        #endregion

        public static object Negate<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Negation, new[] { typeof(T1) })?.Execute(a);

        public static object Modulus<T1>(T1 a)
            => MathOperationManager.Get(OperationType.Modulus, new[] { typeof(T1) })?.Execute(a);

        public static object SquareRoot<T1>(T1 a)
            => MathOperationManager.Get(OperationType.SquareRoot, new[] { typeof(T1) })?.Execute(a);

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

        public static object NthRoot<T1, T2>(T1 a, T2 b)
            => MathOperationManager.Get(OperationType.NthRoot, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);

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
    }
}