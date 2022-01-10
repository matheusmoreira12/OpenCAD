using OpenCAD.Modules.Math.Exceptions;
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
            registerDefaultValueConversions();
        }

        #region Default Double Operations
        static void registerDefaultOperations()
        {
            OperationManager.RegisterMany(
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
                new NthRoot<double, double, double>((d, e) => System.Math.Pow(d, 1 / e))
            #endregion
            );
        }
        #endregion

        #region Default Conversions
        static void registerDefaultValueConversions()
        {
            ValueConversionManager.RegisterMany(
                new ValueConversion<decimal, double>(i => (double)i),
                new ValueConversion<ulong, double>(i => i),
                new ValueConversion<long, double>(i => i),
                new ValueConversion<int, double>(i => i),
                new ValueConversion<ushort, double>(i => i),
                new ValueConversion<short, double>(i => i),
                new ValueConversion<char, double>(i => i),
                new ValueConversion<byte, double>(i => i),
                new ValueConversion<float, double>(i => i),
                new ValueConversion<double, decimal>(d => (decimal)d),
                new ValueConversion<double, ulong>(d => (ulong)d),
                new ValueConversion<double, long>(d => (long)d),
                new ValueConversion<double, int>(d => (int)d),
                new ValueConversion<double, ushort>(d => (ushort)d),
                new ValueConversion<double, short>(d => (short)d),
                new ValueConversion<double, char>(d => (char)d),
                new ValueConversion<double, byte>(d => (byte)d),
                new ValueConversion<double, float>(d => (float)d)
            );
        }
        #endregion

        public static object Negate<T1>(T1 a)
            => OperationManager.GetExact(OperationType.Negation, new[] { typeof(T1) })?.Execute(a)
                ?? throw new OperationNotFoundException();

        public static object Modulus<T1>(T1 a)
            => OperationManager.GetExact(OperationType.Modulus, new[] { typeof(T1) })?.Execute(a)
                ?? throw new OperationNotFoundException();

        public static object SquareRoot<T1>(T1 a)
            => OperationManager.GetExact(OperationType.SquareRoot, new[] { typeof(T1) })?.Execute(a)
                ?? throw new OperationNotFoundException();

        public static object Add<T1, T2>(T1 a, T2 b)
            => OperationManager.GetExact(OperationType.Addition, new[] { typeof(T1), typeof(T2) })?.Execute(a, b)
                ?? throw new OperationNotFoundException();

        public static object Subtract<T1, T2>(T1 a, T2 b) =>
            OperationManager.GetExact(OperationType.Subtraction, new[] { typeof(T1), typeof(T2) }).Execute(a, b)
                ?? throw new OperationNotFoundException();

        public static object Multiply<T1, T2>(T1 a, T2 b)
            => OperationManager.GetExact(OperationType.Multiplication, new[] { typeof(T1), typeof(T2) })?.Execute(a, b)
                ?? throw new OperationNotFoundException();

        public static object Divide<T1, T2>(T1 a, T2 b)
            => OperationManager.GetExact(OperationType.Division, new[] { typeof(T1), typeof(T2) })?.Execute(a, b)
                ?? throw new OperationNotFoundException();

        public static object Exponentiate<T1, T2>(T1 a, T2 b)
            => OperationManager.GetExact(OperationType.Exponentiation, new[] { typeof(T1), typeof(T2) })?.Execute(a, b)
                ?? throw new OperationNotFoundException();

        public static object NthRoot<T1, T2>(T1 a, T2 b)
            => OperationManager.GetExact(OperationType.NthRoot, new[] { typeof(T1), typeof(T2) })?.Execute(a, b)
                ?? throw new OperationNotFoundException();

        public static object Sine<T1>(T1 a)
            => OperationManager.GetExact(OperationType.Sine, new[] { typeof(T1) })?.Execute(a)
                ?? throw new OperationNotFoundException();

        public static object Cosine<T1>(T1 a)
            => OperationManager.GetExact(OperationType.Cosine, new[] { typeof(T1) })?.Execute(a)
                ?? throw new OperationNotFoundException();

        public static object Tangent<T1>(T1 a)
            => OperationManager.GetExact(OperationType.Tangent, new[] { typeof(T1) })?.Execute(a)
                ?? throw new OperationNotFoundException();

        public static object Cosecant<T1>(T1 a)
            => OperationManager.GetExact(OperationType.Cosecant, new[] { typeof(T1) })?.Execute(a)
                ?? throw new OperationNotFoundException();

        public static object Secant<T1>(T1 a)
            => OperationManager.GetExact(OperationType.Secant, new[] { typeof(T1) })?.Execute(a)
                ?? throw new OperationNotFoundException();

        public static object Cotangent<T1>(T1 a)
            => OperationManager.GetExact(OperationType.Cotangent, new[] { typeof(T1) })?.Execute(a)
                ?? throw new OperationNotFoundException();
    }
}