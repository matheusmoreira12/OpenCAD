using OpenCAD.Modules.Math.Exceptions;
using OpenCAD.Modules.Math.ValueConversion;
using System;
using System.Linq;

namespace OpenCAD.Modules.Math.Operations
{
    public abstract class NAryOperation
    {
        public static object GetAndExecute(OperationType operationType, params object[] operands)
        {
            var operandTypes = operands.Select(operand => operand.GetType()).ToArray();
            return getAndExecuteExact() ?? getAndExecuteInexact() ?? throw new OperationNotFoundException();

            object getAndExecuteExact()
            {
                var exactOperation = OperationManager.GetExact(operationType, operandTypes);
                return exactOperation?.Execute(operands);
            }

            object getAndExecuteInexact()
            {
                NAryOperation operation;
                Func<object, object>[] operandConversionPredicates;
                if (tryFindOperationAndParameterConversionPredicates(out operation, out operandConversionPredicates))
                {
                    var convertedOperands = convertOperands(operandConversionPredicates);
                    return operation.Execute(convertedOperands);
                }
                return null;

                bool tryFindOperationAndParameterConversionPredicates(out NAryOperation operation, out Func<object, object>[] predicates)
                {
                    (operation, predicates) = OperationManager.GetAll(operationType)
                        .AsParallel()
                        .Select((operation) => (operation, conversions: getOperandConversionPredicatesOrNull(operation.OperandTypes)))
                        .FirstOrDefault(t => t.conversions != null);
                    return operation != null;

					Func<object, object>[] getOperandConversionPredicatesOrNull(Type[] destOperandTypes)
                    {
                        var converters = operandTypes
                            .Zip(destOperandTypes, (input, output) => (input, output))
                            .Select(types => ValueConverter.TryGetConversionPredicate(types.input, types.output, out var predicate) ? predicate : null)
                            .TakeWhile(conversion => conversion != null)
                            .ToArray();

                        return converters.Length == destOperandTypes.Length ? converters : null;
                    }
                }

                object[] convertOperands(Func<object, object>[] operandConversions)
                    => operands
                        .Zip(operandConversions, (value, conversionPredicate) => (value, conversionPredicate))
                        .Select(pair => pair.conversionPredicate(pair.value))
                        .ToArray();
            }
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
        /// Gets the type of this operation.
        /// </summary>
        public abstract OperationType OperationType { get; }

        /// <summary>
        /// Gets the arity of this operation.
        /// </summary>
        public abstract int Arity { get; }

		/// <summary>
		/// Executes this operation on the specified parameters
		/// </summary>
		/// <param name="operands"></param>
		/// <returns>The result of the operation.</returns>
		public abstract object Execute(params object[] operands);
	}

	public abstract class NAryOperation<T1, TR> : NAryOperation
    {
		/// <summary>
		/// Creates a new n-ary operation.
		/// </summary>
		/// <param name="executor">The executor of the new n-ary operation.</param>
		protected NAryOperation(Func<T1, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public override object Execute(params object[] operands) => executor((T1)operands[0]);

        public TR Execute(T1 op1) => executor(op1);

        public override Type[] OperandTypes => new[] { typeof(T1) };

        public override Type ResultType => typeof(TR);

		public override int Arity => 1;

		private readonly Func<T1, TR> executor;
    }

    public abstract class NAryOperation<T1, T2, TR> : NAryOperation
    {
		/// <summary>
		/// Creates a new n-ary operation.
		/// </summary>
		/// <param name="executor">The executor of the new n-ary operation.</param>
		protected NAryOperation(Func<T1, T2, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public override object Execute(params object[] operands) => executor((T1)operands[0], (T2)operands[1]);

        public TR Execute(T1 op1, T2 op2) => executor(op1, op2);

        public override Type[] OperandTypes => new[] { typeof(T1), typeof(T2) };

        public override Type ResultType => typeof(TR);

		public override int Arity => 2;

		private readonly Func<T1, T2, TR> executor;
    }

    public abstract class NAryOperation<T1, T2, T3, TR> : NAryOperation
    {
		/// <summary>
		/// Creates a new n-ary operation.
		/// </summary>
		/// <param name="executor">The executor of the new n-ary operation.</param>
		protected NAryOperation(Func<T1, T2, T3, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public override object Execute(params object[] operands)
            => executor((T1)operands[0], (T2)operands[1], (T3)operands[2]);

        public TR Execute(T1 op1, T2 op2, T3 op3) => executor(op1, op2, op3);

        public override Type[] OperandTypes => new[] { typeof(T1), typeof(T2), typeof(T3) };

        public override Type ResultType => typeof(TR);

		public override int Arity => 3;

		private readonly Func<T1, T2, T3, TR> executor;
    }

    public abstract class NAryOperation<T1, T2, T3, T4, TR> : NAryOperation
    {
		/// <summary>
		/// Creates a new n-ary operation.
		/// </summary>
		/// <param name="executor">The executor of the new n-ary operation.</param>
		protected NAryOperation(Func<T1, T2, T3, T4, TR> executor)
        {
            this.executor = executor ?? throw new ArgumentNullException(nameof(executor));
        }

        public override object Execute(params object[] operands)
            => executor((T1)operands[0], (T2)operands[1], (T3)operands[2], (T4)operands[3]);

        public TR Execute(T1 op1, T2 op2, T3 op3, T4 op4) => executor(op1, op2, op3, op4);

        public override Type[] OperandTypes => new[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

        public override Type ResultType => typeof(TR);

		public override int Arity => 4;

		private readonly Func<T1, T2, T3, T4, TR> executor;
    }
}