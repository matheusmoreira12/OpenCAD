using OpenCAD.Modules.Tokens;
using OpenCAD.Utils;
using System;

namespace OpenCAD.Modules.Math.Expressions.Tokens
{
    public class MathExpressionTokenifier : StringTokenifier<StringToken>
    {
        public MathExpressionTokenifier(string content) : base(content)
        {
        }

        protected override bool readToken(StringScanner scanner, out StringToken result)
        {
            return readIdentifier(scanner, out result) ||
                readNumber(scanner, out result) ||
                readOperator(scanner, out result) ||
                readWhitespace(scanner, out result) ||
                readEndOfString(scanner, out result);
        }

        private bool readIdentifier(StringScanner scanner, out StringToken result)
        {
            using (var token = scanner.SaveIndex())
            {
                string identififier;

                if (StringUtils.ReadIdentifier(scanner, out identififier))
                {
                    result = new MathNameToken(identififier);
                    return true;
                }

                scanner.RestoreIndex(token);

                result = default;
                return false;
            }
        }

        private bool readNumber(StringScanner scanner, out StringToken result)
        {
            using (var token = scanner.SaveIndex())
            {
                string numberStr;

                if (StringUtils.ReadDecimalString(scanner, out numberStr))
                {
                    double number = double.Parse(numberStr);

                    result = new MathNumberToken(number);
                    return true;
                }

                scanner.RestoreIndex(token);

                result = default;
                return false;
            }
        }

        private bool readOperator(StringScanner scanner, out StringToken result)
        {
            using (var token = scanner.SaveIndex())
            {
                switch (scanner.CurrentChar)
                {
                    case '+':
                        scanner.Increment();

                        result = new MathOperatorToken(MathOperatorTokenType.Add);
                        return true;

                    case '-':
                        scanner.Increment();

                        result = new MathOperatorToken(MathOperatorTokenType.Subtract);
                        return true;

                    case '*':
                        scanner.Increment();

                        result = new MathOperatorToken(MathOperatorTokenType.Multiply);
                        return true;

                    case '/':
                        scanner.Increment();

                        result = new MathOperatorToken(MathOperatorTokenType.Divide);
                        return true;

                    case '^':
                        scanner.Increment();

                        result = new MathOperatorToken(MathOperatorTokenType.Power);
                        return true;

                    case '(':
                        scanner.Increment();

                        result = new MathOperatorToken(MathOperatorTokenType.OpenBracket);
                        return true;

                    case ')':
                        scanner.Increment();

                        result = new MathOperatorToken(MathOperatorTokenType.CloseBracket);
                        return true;

                    case '=':
                        scanner.Increment();

                        result = new MathOperatorToken(MathOperatorTokenType.Equals);
                        return true;

                    case '>':
                        scanner.Increment();

                        switch (scanner.CurrentChar)
                        {
                            case '=':
                                scanner.Increment();

                                result = new MathOperatorToken(MathOperatorTokenType.GreaterThanOrEquals);
                                break;

                            default:
                                result = new MathOperatorToken(MathOperatorTokenType.GreaterThan);
                                break;
                        }

                        return true;

                    case '<':
                        scanner.Increment();

                        switch (scanner.CurrentChar)
                        {
                            case '=':
                                scanner.Increment();

                                result = new MathOperatorToken(MathOperatorTokenType.SmallerThanOrEquals);
                                break;

                            default:
                                result = new MathOperatorToken(MathOperatorTokenType.SmallerThan);
                                break;
                        }

                        return true;
                }

                scanner.RestoreIndex(token);

                result = default;
                return false;
            }
        }

        private bool readWhitespace(StringScanner scanner, out StringToken result)
        {
            using (var token = scanner.SaveIndex())
            {
                while (char.IsWhiteSpace(scanner.CurrentChar))
                    scanner.Increment();

                if (scanner.GetRelativeIndex(token) > 0)
                {
                    result = new MathFormatToken(MathFormatTokenType.WhiteSpace);
                    return true;
                }

                result = default;
                return false;
            }
        }

        private bool readEndOfString(StringScanner scanner, out StringToken result)
        {
            if (!scanner.EndReached)
                throw new FormatException($"Unexpected token \"{scanner.CurrentChar}\" in " +
                    $"input string at position {scanner.CurrentIndex}.");

            result = default;
            return false;
        }
    }
}
