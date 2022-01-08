using OpenCAD.DataTypes;

namespace OpenCAD.Modules.Math.Operations
{
    public partial class OperationType : ExpandableEnum<OperationType>
    {
        public static readonly OperationType Positivation = new OperationType(0);
        public static readonly OperationType Negation = new OperationType();
        public static readonly OperationType Addition = new OperationType();
        public static readonly OperationType Subtraction = new OperationType();
        public static readonly OperationType Multiplication = new OperationType();
        public static readonly OperationType Division = new OperationType();
        public static readonly OperationType Exponentiation = new OperationType();
        public static readonly OperationType Sine = new OperationType();
        public static readonly OperationType Cosine = new OperationType();
        public static readonly OperationType Tangent = new OperationType();
        public static readonly OperationType Cosecant = new OperationType();
        public static readonly OperationType Secant = new OperationType();
        public static readonly OperationType Cotangent = new OperationType();

        public OperationType() { }

        public OperationType(int id) : base(id) { }
    }
}
