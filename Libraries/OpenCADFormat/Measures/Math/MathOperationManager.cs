using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Measures
{
    public static class MathOperationManager
    {
        private static List<MathOperation> registeredOperations { get; } = new List<MathOperation> { };

        public static void Register(MathOperation operation) => registeredOperations.Add(operation);

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

        private static Type[] getOperandTypes(MathOperation operation)
        {
            Type[] typeArgs = operation.GetType().GenericTypeArguments;
            return typeArgs.Take(typeArgs.Length - 1).ToArray();
        }

        private static List<MathOperation> getByOperandTypes(params Type[] operandTypes) =>
            registeredOperations.FindAll(o => Enumerable.SequenceEqual(getOperandTypes(o), operandTypes));

        public static List<MathOperation> GetAll(Type operandType) => getByOperandTypes(operandType);

        public static MathOperation Get(MathOperationType operationType, Type operandType) =>
            GetAll(operandType).Find(o => o.OperationType == operationType);

        public static List<MathOperation> GetAll(Type operandTypeA, Type operantTypeB) =>
            getByOperandTypes(operandTypeA, operantTypeB);

        public static MathOperation Get(MathOperationType operationType, Type operandTypeA, Type operandTypeB) =>
            GetAll(operandTypeA, operandTypeB).Find(o => o.OperationType == operationType);
    }
}
