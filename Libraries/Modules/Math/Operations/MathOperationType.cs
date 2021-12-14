using OpenCAD.DataTypes;

namespace OpenCAD.Modules.Math.Operations
{
    public sealed partial class MathOperationType : ExpandableEnum<MathOperationType>
    {
        #region Unary Operators
        public static readonly MathOperationType Positivation = new MathOperationType(0);
        public static readonly MathOperationType Negation = new MathOperationType();
        #endregion

        #region Binary Operators
        public static readonly MathOperationType Addition = new MathOperationType();
        public static readonly MathOperationType Subtraction = new MathOperationType();
        public static readonly MathOperationType Multiplication = new MathOperationType();
        public static readonly MathOperationType Division = new MathOperationType();
        public static readonly MathOperationType Exponentiation = new MathOperationType();
        #endregion

        public MathOperationType(int value) : base(value) { }
        public MathOperationType() { }
    }
}