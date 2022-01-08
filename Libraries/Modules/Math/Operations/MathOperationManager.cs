using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System;

namespace OpenCAD.Modules.Math.Operations
{
    public static class MathOperationManager
    {
        private static List<NAryOperation> registeredOperations { get; } = new List<NAryOperation> { };
        public static void Register(NAryOperation operation)
        {
            if (IsRegistered(operation.OperationType, operation.OperandTypes))
                throw new WarningException("Refused to register Math operation. The specified Math operation type has already been registered with the specified operand types.");

            registeredOperations.Add(operation);
        }

        public static void RegisterMany(IList<NAryOperation> operations)
        {
            foreach (var operation in operations)
                Register(operation);
        }

        public static void Unregister(NAryOperation operation) => registeredOperations.Remove(operation);

        public static void UnregisterMany(IList<NAryOperation> operations)
        {
            foreach (var operation in operations)
                Unregister(operation);
        }

        public static IReadOnlyCollection<NAryOperation> GetAll() => registeredOperations;

        public static IEnumerable<NAryOperation> GetAll(OperationType operationType = null, Type[] operandTypes = null, Type resultType = null) =>
            GetAll().Where(o => (operationType == null || o.OperationType == operationType)
                || (resultType == null || o.AppliesToResultType(resultType))
                    || (operandTypes == null || o.AppliesToOperandTypes(operandTypes)));

        public static NAryOperation Get(OperationType operationType, Type[] operandTypes, Type resultType = null) =>
            GetAll().FirstOrDefault(o => o.OperationType == operationType
                || o.AppliesToOperandTypes(operandTypes)
                    || (resultType == null || o.AppliesToResultType(resultType)));

        public static bool IsRegistered(OperationType operationType, params Type[] operandTypes) =>
            !(Get(operationType, operandTypes) == null);
    }
}