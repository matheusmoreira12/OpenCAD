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
                Func<object, object>[] operandConversions;
                if (tryFindOperationAndParameterConversions(out operation, out operandConversions))
                {
                    var convertedOperands = convertOperands(operandConversions);
                    return operation.Execute(convertedOperands);
                }
                return null;

                bool tryFindOperationAndParameterConversions(out NAryOperation operation, out Func<object, object>[] conversions)
                {
                    (operation, conversions) = OperationManager.GetAll(operationType)
                        .AsParallel()
                        .Select((operation) => (operation, conversions: getOperandConversionsOrNull(operation.OperandTypes)))
                        .FirstOrDefault(t => t.conversions != null);
                    return operation != null;

                    Func<object, object>[] getOperandConversionsOrNull(Type[] destOperandTypes)
                    {
                        var conversions = operandTypes
                            .Zip(destOperandTypes, (source, dest) => (source, dest))
                            .Select(types => getConversionOrNull(types.source, types.dest))
                            .TakeWhile(conversion => conversion != null)
                            .ToArray();

                        if (conversions.Length == destOperandTypes.Length)
                            return conversions;
                        return null;

                        Func<object, object> getConversionOrNull(Type sourceType, Type destType)
                        {
                            if (sourceType == destType)
                                return o => o;
                            ValueConverter converter;
                            if (ValueConverterManager.TryGetExact(sourceType, destType, out converter))
                                return converter.Convert;
                            if (ValueConverterManager.TryGetExact(destType, sourceType, out converter))
                                return converter.ConvertBack;
                            return null;
                        }
                    }
                }

                object[] convertOperands(Func<object, object>[] operandConversions)
                    => operands
                        .Zip(operandConversions, (value, conversion) => (value, conversion))
                        .Select(pair => pair.conversion(pair.value))
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