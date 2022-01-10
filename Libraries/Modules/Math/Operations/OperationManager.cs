using System.Linq;
using System.Collections.Generic;
using System;

namespace OpenCAD.Modules.Math.Operations
{
    public static class OperationManager
    {
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
        /// Registers many operations at once.
        /// </summary>
        /// <param name="operations">The operations to be registered.</param>
        public static void RegisterMany(params NAryOperation[] operations) => RegisterMany(operations);

        /// <summary>
        /// Registers many operations at once.
        /// </summary>
        /// <param name="operations">The operations to be registered.</param>
        public static void RegisterMany(IList<NAryOperation> operations) => operations.AsParallel().ForAll(operation => Register(operation));

        /// <summary>
        /// Unregisters an operation.
        /// </summary>
        /// <param name="operation">The operation to be unregistered.</param>
        public static void Unregister(NAryOperation operation) => RegisteredOperations.Remove(operation);

        /// <summary>
        /// Unregisters many operations at once.
        /// </summary>
        /// <param name="operations">The operations to be unregistered.</param>
        public static void UnregisterMany(params NAryOperation[] operations) => UnregisterMany(operations);

        /// <summary>
        /// Unregisters many operations at once.
        /// </summary>
        /// <param name="operations">The operations to be unregistered.</param>
        public static void UnregisterMany(IList<NAryOperation> operations) => operations.AsParallel().ForAll(operation => Unregister(operation));

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

        /// <summary>
        /// Checks if there is an operation registered which matches exactly the specified criteria.
        /// </summary>
        /// <param name="operationType">The operation type to be checked.</param>
        /// <param name="operandTypes">The operand types to be checked.</param>
        /// <param name="resultType">The operand types to be checked, or null for matching any result type.</param>
        /// <returns>True if there is an operation registered which matches exactly the specified criteria, false if no match is found.</returns>
        public static bool IsRegistered(NAryOperation operation) => GetExact(operation.OperationType, operation.OperandTypes) != null;
    }
}