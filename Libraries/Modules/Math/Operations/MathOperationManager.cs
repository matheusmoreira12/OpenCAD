using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace OpenCAD.Modules.Math.Operations
{
    public static class MathOperationManager
    {
        private static WarningException GetAlreadyRegisteredWarning(NAryOperation operation)
        {
            var operationType = operation.OperationType;
            var operandTypes = operation.OperandTypes;
            var operandTypeNames = operandTypes.Select(type => type.Name);
            string operandTypesStr = $"{string.Join(", ", operandTypeNames)}";
            string message = $"Refused to register Math operation. The {operation.OperationType} operation has already been registered with operand type(s) {operandTypesStr}.";
            return new WarningException(message);
        }

        public static void Register(NAryOperation operation)
        {
            if (IsRegistered(operation.OperationType, operation.OperandTypes))
                throw GetAlreadyRegisteredWarning(operation);

            RegisteredOperations.Add(operation);
        }

        public static void RegisterMany(IList<NAryOperation> operations)
        {
            foreach (var operation in operations)
                Register(operation);
        }

        public static void Unregister(NAryOperation operation) => RegisteredOperations.Remove(operation);

        public static void UnregisterMany(IList<NAryOperation> operations)
        {
            foreach (var operation in operations)
                Unregister(operation);
        }

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
        public static IEnumerable<NAryOperation> GetAll(OperationType operationType = null, Type[] operandTypes = null, Type resultType = null) =>
            RegisteredOperations.AsParallel().Where(o => (operationType == null || o.OperationType == operationType)
                && (resultType == null || o.AppliesToResultType(resultType))
                    && (operandTypes == null || o.AppliesToOperandTypes(operandTypes)));

        //TODO: Implement an optimizer for finding suitable operations
        public static NAryOperation Get(OperationType operationType, Type[] operandTypes, Type resultType = null)
            => GetExact(operationType, operandTypes, resultType)
                ?? GetAll(operationType, operandTypes, resultType).FirstOrDefault();

        /// <summary>
        /// Gets the registered operation that matches exactly the specified criteria, or null if no match is found.
        /// </summary>
        /// <param name="operationType">The desired operation type.</param>
        /// <param name="operandTypes">The desired operand types.</param>
        /// <param name="resultType">The desired result type, or null for matching any result type.</param>
        /// <returns>The registered operation that matches exactly the specified criteria.</returns>
        public static NAryOperation GetExact(OperationType operationType, Type[] operandTypes, Type resultType = null) =>
            RegisteredOperations.AsParallel().FirstOrDefault(o => o.OperationType == operationType
                && o.OperandTypes.SequenceEqual(operandTypes)
                    && (resultType == null || o.ResultType == resultType));

        /// <summary>
        /// Checks if there is an operation registered which matches exactly the specified criteria.
        /// </summary>
        /// <param name="operationType">The operation type to be checked.</param>
        /// <param name="operandTypes">The operand types to be checked.</param>
        /// <param name="resultType">The operand types to be checked, or null for matching any result type.</param>
        /// <returns>True if there is an operation registered which matches exactly the specified criteria, false if no match is found.</returns>
        public static bool IsRegistered(OperationType operationType, params Type[] operandTypes) =>
            GetExact(operationType, operandTypes) != null;
    }
}