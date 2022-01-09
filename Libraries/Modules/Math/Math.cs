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
                new Negation<double, double>(d => -d),
                new Modulus<double, double>(d => d >= 0 ? d : -d),
                new SquareRoot<double, double>(d => System.Math.Sqrt(d)),
                #endregion
                
                #region Trigonometric Unary Operators
                new Sine<double, double>(d => System.Math.Sin(d)),
                new Cosine<double, double>(d => System.Math.Cos(d)),
                new Tangent<double, double>(d => System.Math.Tan(d)),
                new Cosecant<double, double>(d => 1 / System.Math.Sin(d)),
                new Secant<double, double>(d => 1 / System.Math.Cos(d)),
                new Cotangent<double, double>(d => 1 / System.Math.Tan(d)),
                #endregion

                #region Arithmetic Binary Operators
                new Addition<double, double, double>((d, e) => d + e),
                new Subtraction<double, double, double>((d, e) => d - e),
                new Multiplication<double, double, double>((d, e) => d * e),
                new Division<double, double, double>((d, e) => d / e),
                new Exponentiation<double, double, double>((d, e) => System.Math.Pow(d, e)),
                new NthRoot<double, double, double>((d, e) => System.Math.Pow(d, 1 / e)),
                #endregion
            });
        }
        #endregion

        public static object Negate<T1>(T1 a)
            => MathOperationManager.GetExact(OperationType.Negation, new[] { typeof(T1) })?.Execute(a);

        public static object Modulus<T1>(T1 a)
            => MathOperationManager.GetExact(OperationType.Modulus, new[] { typeof(T1) })?.Execute(a);

        public static object SquareRoot<T1>(T1 a)
            => MathOperationManager.GetExact(OperationType.SquareRoot, new[] { typeof(T1) })?.Execute(a);

        public static object Add<T1, T2>(T1 a, T2 b)
            => MathOperationManager.GetExact(OperationType.Addition, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);

        public static object Subtract<T1, T2>(T1 a, T2 b) =>
            MathOperationManager.GetExact(OperationType.Subtraction, new[] {
                typeof(T1),
                typeof(T2),
            }).Execute(a, b);

        public static object Multiply<T1, T2>(T1 a, T2 b)
            => MathOperationManager.GetExact(OperationType.Multiplication, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);

        public static object Divide<T1, T2>(T1 a, T2 b)
            => MathOperationManager.GetExact(OperationType.Division, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);

        public static object Exponentiate<T1, T2>(T1 a, T2 b)
            => MathOperationManager.GetExact(OperationType.Exponentiation, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);

        public static object NthRoot<T1, T2>(T1 a, T2 b)
            => MathOperationManager.GetExact(OperationType.NthRoot, new[] {
                typeof(T1),
                typeof(T2),
            })?.Execute(a, b);

        public static object Sine<T1>(T1 a)
            => MathOperationManager.GetExact(OperationType.Sine, new[] { typeof(T1) })?.Execute(a);

        public static object Cosine<T1>(T1 a)
            => MathOperationManager.GetExact(OperationType.Cosine, new[] { typeof(T1) })?.Execute(a);

        public static object Tangent<T1>(T1 a)
            => MathOperationManager.GetExact(OperationType.Tangent, new[] { typeof(T1) })?.Execute(a);

        public static object Cosecant<T1>(T1 a)
            => MathOperationManager.GetExact(OperationType.Cosecant, new[] { typeof(T1) })?.Execute(a);

        public static object Secant<T1>(T1 a)
            => MathOperationManager.GetExact(OperationType.Secant, new[] { typeof(T1) })?.Execute(a);

        public static object Cotangent<T1>(T1 a)
            => MathOperationManager.GetExact(OperationType.Cotangent, new[] { typeof(T1) })?.Execute(a);
    }
}