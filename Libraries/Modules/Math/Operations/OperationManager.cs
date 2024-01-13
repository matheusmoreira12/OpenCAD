using System.Linq;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using OpenCAD.Modules.Math.Operations.Binary;
using OpenCAD.Modules.Math.Operations.Unary.TrigonometricFunctions;
using OpenCAD.Modules.Math.Operations.Unary;

namespace OpenCAD.Modules.Math.Operations
{
	public static class OperationManager
	{
		static OperationManager()
		{
			registerDefaultOperations();
		}

		#region Default Double Operations
		static void registerDefaultOperations()
		{
			RegisterMany(
			#region Arithmetic Unary Operators
				new Negation<double, double>(d => -d),
				new Modulus<double, double>(d => d >= 0 ? d : -d),
				new SquareRoot<double, double>(System.Math.Sqrt),
			#endregion

			#region Trigonometric Unary Operators
				new Sine<double, double>(System.Math.Sin),
				new Cosine<double, double>(System.Math.Cos),
				new Tangent<double, double>(System.Math.Tan),
				new Cosecant<double, double>(d => 1 / System.Math.Sin(d)),
				new Secant<double, double>(d => 1 / System.Math.Cos(d)),
				new Cotangent<double, double>(d => 1 / System.Math.Tan(d)),
			#endregion

			#region Arithmetic Binary Operators
				new Addition<double, double, double>((d, e) => d + e),
				new Subtraction<double, double, double>((d, e) => d - e),
				new Multiplication<double, double, double>((d, e) => d * e),
				new Division<double, double, double>((d, e) => d / e),
				new Exponentiation<double, double, double>(System.Math.Pow),
				new NthRoot<double, double, double>((d, e) => System.Math.Pow(d, 1 / e))
			#endregion
			);
		}
		#endregion


		/// <summary>
		/// Registers many operations at once.
		/// </summary>
		/// <param name="operations">The operations to be registered.</param>
		public static void RegisterMany(params NAryOperation[] operations)
		{
			foreach (var operation in operations) Register(operation);
		}

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
		public static void UnregisterMany(params NAryOperation[] operations)
		{
			foreach (var operation in operations) Unregister(operation);
		}

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
			=> RegisteredOperations.Where(o => (operationType == null || o.OperationType == operationType)
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
			=> RegisteredOperations.FirstOrDefault(o => o.OperationType == operationType
				&& o.OperandTypes.SequenceEqual(operandTypes));

		/// <summary>
		/// Checks if there is an operation registered which matches exactly the specified criteria.
		/// </summary>
		/// <param name="operation">The operation being checked.</param>
		/// <returns>True if there is an operation registered which matches exactly the specified criteria, false if no match is found.</returns>
		public static bool IsRegistered(NAryOperation operation) => GetExact(operation.OperationType, operation.OperandTypes) != null;
	}
}