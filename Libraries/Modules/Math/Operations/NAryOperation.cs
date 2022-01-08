using System;
using System.Linq;

namespace OpenCAD.Modules.Math.Operations
{
    public abstract class NAryOperation
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

        public abstract object Execute(params object[] operands);

        public abstract OperationType OperationType { get; }
    }

    public abstract class NAryOperation<T1, TR> : NAryOperation
    {
        protected NAryOperation(Func<T1, TR> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<T1, TR> func { get; }

        public override object Execute(params object[] operands) => func((T1)operands[0]);
    }

    public abstract class NAryOperation<T1, T2, TR> : NAryOperation
    {
        protected NAryOperation(Func<T1, T2, TR> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<T1, T2, TR> func { get; }

        public override object Execute(params object[] operands)
        {
            return func((T1)(dynamic)operands[0], (T2)(dynamic)operands[1]);
        }
    }

    public abstract class NAryOperation<T1, T2, T3, TR> : NAryOperation
    {
        protected NAryOperation(Func<T1, T2, T3, TR> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<T1, T2, T3, TR> func { get; }

        public override object Execute(params object[] operands) => func((T1)(dynamic)operands[0],
            (T2)(dynamic)operands[1], (T3)(dynamic)operands[2]);
    }

    public abstract class NAryOperation<T1, T2, T3, T4, TR> : NAryOperation
    {
        protected NAryOperation(Func<T1, T2, T3, T4, TR> func)
        {
            this.func = func ?? throw new ArgumentNullException(nameof(func));
        }

        private Func<T1, T2, T3, T4, TR> func { get; }

        public override object Execute(params object[] operands) => func((T1)(dynamic)operands[0],
            (T2)(dynamic)operands[1], (T3)(dynamic)operands[2], (T4)(dynamic)operands[3]);
    }
}