using System;

public enum MathOperationType { Addition, Negation, Multiplication, Exponentiation }

public abstract class MathOperation
{
    public abstract MathOperationType OperationType { get; }

    public abstract object Execute(params object[] operands);
}

public class MathOperation<TOperand, TResult> : MathOperation
{
    public MathOperation(Func<TOperand, TResult> @operator, MathOperationType operationType)
    {
        this.@operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        OperationType = operationType;
    }

    public TResult Execute(TOperand operand) => @operator(operand);

    public override object Execute(params object[] operands) => Execute((TOperand)operands[0]);

    public Func<TOperand, TResult> @operator { get; }

    public override MathOperationType OperationType { get; }
}

public class MathOperation<TOperandA, TOperandB, TResult> : MathOperation
{
    public MathOperation(Func<TOperandA, TOperandB, TResult> @operator, MathOperationType operationType)
    {
        this.@operator = @operator ?? throw new ArgumentNullException(nameof(@operator));
        OperationType = operationType;
    }

    public TResult Execute(TOperandA operandA, TOperandB operandB) => @operator(operandA, operandB);

    public override object Execute(params object[] operands) => 
        Execute((TOperandA)operands[0], (TOperandB)operands[1]);

    public Func<TOperandA, TOperandB, TResult> @operator { get; }

    public override MathOperationType OperationType { get; }
}