using OpenCAD.DataTypes;

namespace OpenCAD.Modules.Math.Operations
{
    public partial class OperationType : ExpandableEnum<OperationType>
    {
        #region Arithmetic Unary Operations
        public static readonly OperationType Negation = new OperationType(0);
        public static readonly OperationType Modulus = new OperationType();
        public static readonly OperationType SquareRoot = new OperationType();
        #endregion

        #region Arithmetic Binary Operations
        public static readonly OperationType Addition = new OperationType();
        public static readonly OperationType Subtraction = new OperationType();
        public static readonly OperationType Multiplication = new OperationType();
        public static readonly OperationType Division = new OperationType();
        public static readonly OperationType Exponentiation = new OperationType();
        public static readonly OperationType NthRoot = new OperationType();
        #endregion

        #region Trigonometric Operations
        public static readonly OperationType Sine = new OperationType();
        public static readonly OperationType Cosine = new OperationType();
        public static readonly OperationType Tangent = new OperationType();
        public static readonly OperationType Cosecant = new OperationType();
        public static readonly OperationType Secant = new OperationType();
        public static readonly OperationType Cotangent = new OperationType();
        #endregion

        public OperationType() { }

        public OperationType(int id) : base(id) { }
    }
}
