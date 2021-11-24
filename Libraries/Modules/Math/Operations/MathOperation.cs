using System;
using System.Linq;

namespace OpenCAD.APIs.Math.Operations
{
    public abstract class MathOperation
    {
        public Type[] OperandTypes
        {
            get
            {
                if (_OperandTypes == null)
                {
                    Type[] typeArgs = GetType().GenericTypeArguments;
                    Type[] operandTypes = typeArgs.Take(typeArgs.Length - 1).ToArray();
                    return _OperandTypes = operandTypes;
                }
                else
                    return _OperandTypes;
            }
        }

        private Type[] _OperandTypes = null;

        public Type ResultType
        {
            get
            {
                if (_ResultType == null)
                {
                    var typeArgs = GetType().GenericTypeArguments;
                    return typeArgs.Last();
                }
                else
                    return _ResultType;
            }
        }

        private Type _ResultType = null;

        public bool AppliesToOperandTypes(Type[] targetOperandTypes)
        {
            Type[] operandTypes = OperandTypes;
            if (operandTypes.Length != targetOperandTypes.Length)
                return false;
            else
            {
                for (int i = 0; i < operandTypes.Length; i++)
                {
                    if (!targetOperandTypes[i].IsCastableTo(operandTypes[i]))
                        return false;
                }

                return true;
            }
        }

        public bool AppliesToResultType(Type targetResultType) =>
            ResultType.IsCastableTo(targetResultType);

        public abstract MathOperationType OperationType { get; }

        public abstract object Execute(params object[] operands);
    }

    public abstract class MathOperation<TOperand1, TResult> : MathOperation
    {
        protected MathOperation(Func<TOperand1, TResult> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<TOperand1, TResult> func { get; }

        public override object Execute(params object[] operands) => func((TOperand1)operands[0]);
    }

    public abstract class MathOperation<TOperand1, TOperand2, TResult> : MathOperation
    {
        protected MathOperation(Func<TOperand1, TOperand2, TResult> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<TOperand1, TOperand2, TResult> func { get; }

        public override object Execute(params object[] operands)
        {
            return func((TOperand1)(dynamic)operands[0], (TOperand2)(dynamic)operands[1]);
        }
    }

    public abstract class MathOperation<TOperand1, TOperand2, TOperand3, TResult> : MathOperation
    {
        protected MathOperation(Func<TOperand1, TOperand2, TOperand3, TResult> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<TOperand1, TOperand2, TOperand3, TResult> func { get; }

        public override object Execute(params object[] operands) => func((TOperand1)(dynamic)operands[0],
            (TOperand2)(dynamic)operands[1], (TOperand3)(dynamic)operands[2]);
    }

    public abstract class MathOperation<TOperand1, TOperand2, TOperand3, TOperand4, TResult> : MathOperation
    {
        protected MathOperation(Func<TOperand1, TOperand2, TOperand3, TOperand4, TResult> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<TOperand1, TOperand2, TOperand3, TOperand4, TResult> func { get; }

        public override object Execute(params object[] operands) => func((TOperand1)(dynamic)operands[0],
            (TOperand2)(dynamic)operands[1], (TOperand3)(dynamic)operands[2], (TOperand4)(dynamic)operands[3]);
    }
}