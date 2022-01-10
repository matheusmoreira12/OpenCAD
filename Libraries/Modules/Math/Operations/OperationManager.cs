using System.Linq;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using OpenCAD.Modules.Math.Exceptions;

namespace OpenCAD.Modules.Math.Operations
{
    public static class OperationManager
    {
        /// <summary>
        /// Registers many operations at once.
        /// </summary>
        /// <param name="operations">The operations to be registered.</param>
        public static void RegisterMany(params NAryOperation[] operations) => operations.AsParallel().ForAll(operation => Register(operation));

        /// <summary>
        /// Registers an operation.
        /// </summary>
        /// <param name="operation">The operation to be registerd.</param>
        public static void Register(NAryOperation operation)
        {
            if (IsRegistered(operation))
                return;
            RegisteredOperations.Add(operation);
        }

        /// <summary>
        /// Unregisters many operations at once.
        /// </summary>
        /// <param name="operations">The operations to be unregistered.</param>
        public static void UnregisterMany(params NAryOperation[] operations) => operations.AsParallel().ForAll(operation => Unregister(operation));

        /// <summary>
        /// Unregisters an operation.
        /// </summary>
        /// <param name="operation">The operation to be unregistered.</param>
        public static void Unregister(NAryOperation operation) => RegisteredOperations.Remove(operation);

        private static List<NAryOperation> RegisteredOperations { get; } = new List<NAryOperation> { };

        /// <summary>
        /// Gets all the registered operations.
        /// </summary>
        /// <returns>All the registered operations</returns>
        public static NAryOperation[] GetAll() => RegisteredOperations.ToArray();

        /// <summary>
        /// Gets all registered operations compatible with the specified criteria.
        /// </summary>
        /// <param name="operationType">The desired operation type, or null for matching any operation type.</param>
        /// <param name="operandTypes">The desired operand types, or null for matching any operand types.</param>
        /// <param name="resultType">The desired result type, or null for matching any result type.</param>
        /// <returns>All registered operations compatible with the specified criteria.</returns>
        public static IEnumerable<NAryOperation> GetAll(OperationType operationType = null, Type[] operandTypes = null, Type resultType = null)
            => RegisteredOperations.AsParallel().Where(o => (operationType == null || o.OperationType == operationType)
                && (resultType == null || o.ResultType == resultType)
                    && (operandTypes == null || o.OperandTypes.SequenceEqual(operandTypes)));

        /// <summary>
        /// Gets the registered operation that matches exactly the specified criteria, or null if no match is found.
        /// </summary>
        /// <param name="operationType">The desired operation type.</param>
        /// <param name="operandTypes">The desired operand types.</param>
        /// <param name="resultType">The desired result type, or null for matching any result type.</param>
        /// <returns>The registered operation that matches exactly the specified criteria.</returns>
        public static NAryOperation GetExact(OperationType operationType, Type[] operandTypes)
            => RegisteredOperations.AsParallel().FirstOrDefault(o => o.OperationType == operationType
                && o.OperandTypes.SequenceEqual(operandTypes));

        public static object Execute(OperationType operationType, params object[] operands)
        {
            var operandTypes = operands.Select(operand => operand.GetType()).ToArray();
            return operateExact() ?? operateInexact() ?? throw new OperationNotFoundException();

            object operateExact()
            {
                var exactOperation = GetExact(operationType, operandTypes);
                return exactOperation?.Execute(operands);
            }

            object operateInexact()
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

            bool tryFindOperationAndParameterConversions(out NAryOperation operation, out ValueConversion[] operandConversions)
            {
                bool foundMatch = false;
                (operation, operandConversions, foundMatch) = GetAll(operationType)
                    .Select((operation) =>
                        {
                            var conversions = operandTypes
                                .Zip(operation.OperandTypes, (sourceType, destType) => (sourceType, destType))
                                .Select(t => ValueConversionManager.GetExact(t.sourceType, t.destType))
                                .ToArray();
                            bool allParametersAreConvertible = conversions.All(conversion => conversion != null);
                            if (allParametersAreConvertible)
                                return (operation, conversions, true);
                            return default;
                        })
                    .FirstOrDefault(match => match.Item3);
                return foundMatch;
            }

            object[] convertOperands(object[] operands, ValueConversion[] operandConversions) => operands
                .Zip(operandConversions, (value, conversion) => (value, conversion))
                .Select(p => p.conversion.Convert(p.value))
                .ToArray();
        }

        /// <summary>
        /// Checks if there is an operation registered which matches exactly the specified criteria.
        /// </summary>
        /// <param name="operation">The operation being checked.</param>
        /// <returns>True if there is an operation registered which matches exactly the specified criteria, false if no match is found.</returns>
        public static bool IsRegistered(NAryOperation operation) => GetExact(operation.OperationType, operation.OperandTypes) != null;
    }
}