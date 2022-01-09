using System;
using System.Linq;

namespace OpenCAD.Modules.Math.Operations
{
    public abstract class NAryOperation
    {
        private Type[] _OperandTypes = null;
        private Type _ResultType = null;

        /// <summary>
        /// Gets the operand types for this operation.
        /// </summary>
        public Type[] OperandTypes
        {
            get
            {
                if (_OperandTypes == null)
                {
                    Type[] typeArgs = GetType().GenericTypeArguments;
                    Type[] operandTypes = typeArgs.Take(typeArgs.Length - 1).ToArray();
                    return _OperandTypes = operandTypes;
                }
                else
                    return _OperandTypes;
            }
        }

        /// <summary>
        /// Gets the result type of this operation. 
        /// </summary>
        public Type ResultType
        {
            get
            {
                if (_ResultType == null)
                {
                    var typeArgs = GetType().GenericTypeArguments;
                    return typeArgs.Last();
                }
                else
                    return _ResultType;
            }
        }

        /// <summary>
        /// Executes this operation on the specified parameters
        /// </summary>
        /// <param name="operands"></param>
        /// <returns>The result of the operation.</returns>
        public abstract object Execute(params object[] operands);

        /// <summary>
        /// Gets the type of this operation.
        /// </summary>
        public abstract OperationType OperationType { get; }
    }

    public abstract class NAryOperation<T1, TR> : NAryOperation
    {
        /// <summary>
        /// Creates a new n-ary operator.
        /// </summary>
        /// <param name="executor">The executor of the new n-ary operator.</param>
        protected NAryOperation(Func<T1, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        private readonly Func<T1, TR> executor;

        public override object Execute(params object[] operands) => executor((T1)operands[0]);
    }

    public abstract class NAryOperation<T1, T2, TR> : NAryOperation
    {
        protected NAryOperation(Func<T1, T2, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        private readonly Func<T1, T2, TR> executor;

        public override object Execute(params object[] operands)
        {
            return executor((T1)(dynamic)operands[0], (T2)(dynamic)operands[1]);
        }
    }

    public abstract class NAryOperation<T1, T2, T3, TR> : NAryOperation
    {
        protected NAryOperation(Func<T1, T2, T3, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        private readonly Func<T1, T2, T3, TR> executor;

        public override object Execute(params object[] operands) => executor((T1)(dynamic)operands[0],
            (T2)(dynamic)operands[1], (T3)(dynamic)operands[2]);
    }

    public abstract class NAryOperation<T1, T2, T3, T4, TR> : NAryOperation
    {
        protected NAryOperation(Func<T1, T2, T3, T4, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        private readonly Func<T1, T2, T3, T4, TR> executor;

        public override object Execute(params object[] operands) => executor((T1)(dynamic)operands[0],
            (T2)(dynamic)operands[1], (T3)(dynamic)operands[2], (T4)(dynamic)operands[3]);
    }
}