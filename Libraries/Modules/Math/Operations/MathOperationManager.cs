using OpenCAD.Modules.Math.Operations.Binary;
using OpenCAD.Modules.Math.Operations.Unary;
using System;
using System.Collections.Generic;

namespace OpenCAD.Modules.Math.Operations
{
    public static class MathOperationManager
    {
        static MathOperationManager()
        {
            registerDefaultOperations();
        }

        static void registerDefaultOperations()
        {
            RegisterMany(new MathOperation[] {
                new Positivation<double, double>(positivateDouble),
                new Negation<double, double>(negateDouble),
                new Addition<double, double, double>(sumDoubles),
                new Subtraction<double, double, double>(subtractDoubles),
                new Multiplication<double, double, double>(multiplyDoubles),
                new Division<double, double, double>(divideDoubles),
                new Exponentiation<double, double, double>(exponentiateDouble),
            });
        }

        #region Default Double Operators
        private static double positivateDouble(double a) => a;
        private static double negateDouble(double a) => -a;
        private static double sumDoubles(double a, double b) => a + b;
        private static double subtractDoubles(double a, double b) => a - b;
        private static double multiplyDoubles(double a, double b) => a * b;
        private static double divideDoubles(double a, double b) => a / b;
        private static double exponentiateDouble(double a, double b) => System.Math.Pow(a, b);
        #endregion

        private static List<MathOperation> registeredOperations { get; } = new List<MathOperation> { };

        public static void Register(MathOperation operation)
        {
            if (IsRegistered(operation.OperationType, operation.OperandTypes))
                throw new InvalidOperationException("An operation of the same type and with the same operand types is already registered.");

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