using OpenCAD.Modules.Math.Exceptions;
using System;
using System.Linq;

namespace OpenCAD.Modules.Math.Operations
{
    public abstract class NAryOperation
    {
        public static object GetAndExecute(OperationType operationType, params object[] operands)
        {
            var operandTypes = operands.Select(operand => operand.GetType()).ToArray();
            return executeExact() ?? executeInexact() ?? throw new OperationNotFoundException();

            object executeExact()
            {
                var exactOperation = OperationManager.GetExact(operationType, operandTypes);
                return exactOperation?.Execute(operands);
            }

            object executeInexact()
            {
                NAryOperation operation;
                ValueConversion[] operandConversions;
                if (tryFindOperationAndParameterConversions(out operation, out operandConversions))
                {
                    var convertedOperands = convertOperands(operands, operandConversions);
                    return operation.Execute(convertedOperands);
                }
                return null;
            }

            bool tryFindOperationAndParameterConversions(out NAryOperation operation, out ValueConversion[] conversions)
            {
                (operation, conversions) = OperationManager.GetAll(operationType)
                    .AsParallel()
                    .Select((operation) =>
                        {
                            ValueConversion[] conversions;
                            if (tryGetOperandConversions(operation.OperandTypes, out conversions))
                                return (operation, conversions);
                            return default;
                        })
                    .FirstOrDefault(t => t.operation != null);
                return operation != null;
            }

            bool tryGetOperandConversions(Type[] destOperandTypes, out ValueConversion[] conversions)
            {
                conversions = operandTypes
                    .Zip(destOperandTypes, (sourceType, destType) => (sourceType, destType))
                    .Select(t => t.sourceType == t.destType ? ValueConversion.Circular
                        : ValueConversionManager.GetExact(t.sourceType, t.destType))
                    .TakeWhile(conversion => conversion != null)
                    .ToArray();

                return conversions.Length == destOperandTypes.Length;
            }

            object[] convertOperands(object[] operands, ValueConversion[] operandConversions)
                => operands
                    .Zip(operandConversions, (value, conversion) => (value, conversion))
                    .Select(p => p.conversion.Convert(p.value))
                    .ToArray();
        }

        /// <summary>
        /// Gets the operand types for this operation.
        /// </summary>
        public abstract Type[] OperandTypes { get; }

        /// <summary>
        /// Gets the result type of this operation. 
        /// </summary>
        public abstract Type ResultType { get; }

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

        public override object Execute(params object[] operands) => executor((T1)operands[0]);

        public TR Execute(T1 op1) => executor(op1);

        public override Type[] OperandTypes => new[] { typeof(T1) };

        public override Type ResultType => typeof(TR);

        private readonly Func<T1, TR> executor;
    }

    public abstract class NAryOperation<T1, T2, TR> : NAryOperation
    {
        /// <summary>
        /// Creates a new n-ary operator.
        /// </summary>
        /// <param name="executor">The executor of the new n-ary operator.</param>
        protected NAryOperation(Func<T1, T2, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public override object Execute(params object[] operands) => executor((T1)operands[0], (T2)operands[1]);

        public TR Execute(T1 op1, T2 op2) => executor(op1, op2);

        public override Type[] OperandTypes => new[] { typeof(T1), typeof(T2) };

        public override Type ResultType => typeof(TR);

        private readonly Func<T1, T2, TR> executor;
    }

    public abstract class NAryOperation<T1, T2, T3, TR> : NAryOperation
    {
        /// <summary>
        /// Creates a new n-ary operator.
        /// </summary>
        /// <param name="executor">The executor of the new n-ary operator.</param>
        protected NAryOperation(Func<T1, T2, T3, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public override object Execute(params object[] operands)
            => executor((T1)operands[0], (T2)operands[1], (T3)operands[2]);

        public TR Execute(T1 op1, T2 op2, T3 op3) => executor(op1, op2, op3);

        public override Type[] OperandTypes => new[] { typeof(T1), typeof(T2), typeof(T3) };

        public override Type ResultType => typeof(TR);

        private readonly Func<T1, T2, T3, TR> executor;
    }

    public abstract class NAryOperation<T1, T2, T3, T4, TR> : NAryOperation
    {
        /// <summary>
        /// Creates a new n-ary operator.
        /// </summary>
        /// <param name="executor">The executor of the new n-ary operator.</param>
        protected NAryOperation(Func<T1, T2, T3, T4, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public override object Execute(params object[] operands)
            => executor((T1)operands[0], (T2)operands[1], (T3)operands[2], (T4)operands[3]);

        public TR Execute(T1 op1, T2 op2, T3 op3, T4 op4) => executor(op1, op2, op3, op4);

        public override Type[] OperandTypes => new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

        public override Type ResultType => typeof(TR);

        private readonly Func<T1, T2, T3, T4, TR> executor;
    }
}