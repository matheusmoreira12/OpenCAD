using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Math.Operations
{
    public static class MathOperationManager
    {
        static MathOperationManager()
        {
            //Register default operations
            RegisterMany(new MathOperation[] {
                new Addition<double, double, double>(sumDoubles),
                new Subtraction<double, double, double>(subtractDoubles),
                new Multiplication<double, double, double>(multiplyDoubles),
                new Division<double, double, double>(exponentiateDouble)
            });
        }

        private static double sumDoubles(double a, double b) => a + b;
        private static double subtractDoubles(double a, double b) => a - b;
        private static double multiplyDoubles(double a, double b) => a * b;
        private static double divideDoubles(double a, double b) => a / b;
        private static double exponentiateDouble(double a, double b) => System.Math.Pow(a, b);

        private static List<MathOperation> registeredOperations { get; } = new List<MathOperation> { };

        public static void Register(MathOperation operation)
        {
            if (IsRegistered(operation.OperationType, operation.OperandTypes))
                throw new InvalidOperationException("An operation with the same OperationType and OperandTypes is already registered.");

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
            registeredOperations.FindAll(op => op.AppliesToOperandTypes(operandTypes));

        public static MathOperation Get(MathOperationType operationType, params Type[] operandTypes) =>
            GetAll(operandTypes).Find(o => o.OperationType == operationType);

        public static bool IsRegistered(MathOperationType operationType, params Type[] operandTypes) =>
            !(Get(operationType, operandTypes) == null);
    }
}