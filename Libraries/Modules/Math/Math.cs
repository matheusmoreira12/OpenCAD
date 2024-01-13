using OpenCAD.Modules.Math.Operations;
using OpenCAD.Modules.Math.Operations.Binary;
using OpenCAD.Modules.Math.Operations.Unary;
using OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions;
using OpenCAD.Modules.Math.ValueConversion;
using OpenCAD.Modules.Math.ValueConversion.DefaultConverters;

namespace OpenCAD.Modules.Math
{
    public static class Math
    {
        static Math()
        {
        }

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