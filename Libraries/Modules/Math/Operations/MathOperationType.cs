using OpenCAD.DataTypes;

namespace OpenCAD.APIs.Math.Operations
{
    public sealed partial class MathOperationType : ExpandableEnum<MathOperationType>
    {
        //Unary operators

        //Binary operators
        public static readonly MathOperationType Addition = new MathOperationType(0);
        public static readonly MathOperationType Subtraction = new MathOperationType();
        public static readonly MathOperationType Multiplication = new MathOperationType();
        public static readonly MathOperationType Division = new MathOperationType();
        public static readonly MathOperationType Modulus = new MathOperationType();
        public static readonly MathOperationType Exponentiation = new MathOperationType();

        public MathOperationType(int value) : base(value) { }
        public MathOperationType() { }
    }
}