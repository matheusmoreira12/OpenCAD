using OpenCAD.APIs.Math.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.APIs.Math.Expressions.Solving
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
