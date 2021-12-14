using OpenCAD.Modules.Math.Operations;
using System;

namespace OpenCAD.Modules.Math.Expressions.Solving
{
    public abstract class MathOperatorSolverNode : MathSolverNode
    {
        protected MathOperatorSolverNode(MathOperation operation, MathOperatorSolverNode leftSide, MathOperatorSolverNode rightSide)
        {
            Operation = operation ?? throw new ArgumentNullException(nameof(operation));
            LeftSide = leftSide ?? throw new ArgumentNullException(nameof(leftSide));
            RightSide = rightSide ?? throw new ArgumentNullException(nameof(rightSide));
        }

        public MathOperation Operation { get; }
        public MathOperatorSolverNode LeftSide { get; }
        public MathOperatorSolverNode RightSide { get; }
    }
}
