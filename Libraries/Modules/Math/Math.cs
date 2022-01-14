using OpenCAD.Modules.Math.Operations;
using OpenCAD.Modules.Math.Operations.Binary;
using OpenCAD.Modules.Math.Operations.Unary;
using OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions;
using OpenCAD.Modules.Math.ValueConversion;
using OpenCAD.Modules.Math.ValueConversion.DefaultConverters;

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
            ValueConverterManager.RegisterMany(
                new DecimalToDoubleConverter(),
                new ULongToDoubleConverter(),
                new LongToDoubleConverter(),
                new IntToDoubleConverter(),
                new UShortToDoubleConverter(),
                new ShortToDoubleConverter(),
                new ByteToDoubleConverter(),
                new FloatToDoubleConverter()
            );
        }
        #endregion

        public static object Negate(object a) => NAryOperation.GetAndExecute(OperationType.Negation, a);

        public static object Modulus(object a) => NAryOperation.GetAndExecute(OperationType.Modulus, a);

        public static object SquareRoot(object a) => NAryOperation.GetAndExecute(OperationType.SquareRoot, a);

        public static object Add(object a, object b) => NAryOperation.GetAndExecute(OperationType.Addition, a, b);

        public static object Subtract(object a, object b) => NAryOperation.GetAndExecute(OperationType.Subtraction, a, b);

        public static object Multiply(object a, object b) => NAryOperation.GetAndExecute(OperationType.Multiplication, a, b);

        public static object Divide(object a, object b) => NAryOperation.GetAndExecute(OperationType.Division, a, b);

        public static object Exponentiate(object a, object b) => NAryOperation.GetAndExecute(OperationType.Exponentiation, a, b);

        public static object NthRoot(object a, object b) => NAryOperation.GetAndExecute(OperationType.NthRoot, a, b);

        public static object Sine(object a) => NAryOperation.GetAndExecute(OperationType.Sine, a);

        public static object Cosine(object a) => NAryOperation.GetAndExecute(OperationType.Cosine, a);

        public static object Tangent(object a) => NAryOperation.GetAndExecute(OperationType.Tangent, a);

        public static object Cosecant(object a) => NAryOperation.GetAndExecute(OperationType.Cosecant, a);

        public static object Secant(object a) => NAryOperation.GetAndExecute(OperationType.Secant, a);

        public static object Cotangent(object a) => NAryOperation.GetAndExecute(OperationType.Cotangent, a);
    }
}