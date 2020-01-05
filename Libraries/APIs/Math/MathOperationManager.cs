using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Math
{
    public static class MathOperationManager
    {
        private static List<MathOperation> registeredOperations { get; } = new List<MathOperation> { };

        public static void Register(MathOperation operation)
        {
            if (Has(operation.OperationType, MathOperation.GetOperandTypes(operation)))
                throw new InvalidOperationException("");

            registeredOperations.Add(operation);
        }

        public static void RegisterMany(IList<MathOperation> operations)
        {
            foreach (var operation in operations)
                Register(operation);
        }

        public static void Unregister(MathOperation operation) => registeredOperations.Remove(operation);

        public static void UnregisterMany(IList<MathOperation> operations)
        {
            foreach (var operation in operations)
                Unregister(operation);
        }

        public static IEnumerable<MathOperation> GetAll() => registeredOperations;

        public static List<MathOperation> GetAll(Type[] operandTypes) =>
            registeredOperations.FindAll(o => Enumerable.SequenceEqual(MathOperation.GetOperandTypes(o), 
                operandTypes));

        public static MathOperation Get(MathOperationType operationType, params Type[] operandTypes) =>
            GetAll(operandTypes).Find(o => o.OperationType == operationType);

        public static bool Has(MathOperationType operationType, params Type[] operandTypes) =>
            !(Get(operationType, operandTypes) is null);
    }
}