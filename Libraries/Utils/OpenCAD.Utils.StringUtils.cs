using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.Utils
{
    public static class StringUtils
    {
        public static readonly char[] NUMERAL_CHARSET = GetCharRange('0', '9').ToArray();
        public static readonly char[] DECIMAL_SIGN_CHARACTERS = new[] { '+', '-' };
        public static readonly char[] DECIMAL_CHARSET = NUMERAL_CHARSET.Concat(new[] { DECIMAL_EXPONENT_CHARACTER,
                DECIMAL_SEPARATOR }).Concat(DECIMAL_SIGN_CHARACTERS).ToArray();
        public static readonly char[] LOWER_LETTERS_CHARSET = GetCharRange('a', 'z').ToArray();
        public static readonly char[] UPPER_LETTERS_CHARSET = GetCharRange('A', 'Z').ToArray();
        public static readonly char[] LETTERS_CHARSET = LOWER_LETTERS_CHARSET.Concat(UPPER_LETTERS_CHARSET).ToArray();
        public static readonly char[] WORD_STRICT_CHARSET = LETTERS_CHARSET.Concat(new char[] { '_' }).ToArray();
        public static readonly char[] WORD_CHARSET = WORD_STRICT_CHARSET.Concat(NUMERAL_CHARSET).ToArray();

        public const char DECIMAL_SEPARATOR = '.';
        public const char DECIMAL_EXPONENT_CHARACTER = 'e';

        public static IEnumerable<char> GetCharRange(int start, int end)
        {
            return Enumerable.Range(start, end - start + 1).Select(i => (char)i);
        }

        public static bool CharIsNumeral(char c)
        {
            return NUMERAL_CHARSET.Contains(c);
        }

        public static bool CharIsDecimal(char c)
        {
            return DECIMAL_CHARSET.Contains(c);
        }

        public static bool CharIsLetter(char c)
        {
            return LETTERS_CHARSET.Contains(c);
        }

        public static bool CharIsUpperLetter(char c)
        {
            return UPPER_LETTERS_CHARSET.Contains(c);
        }

        public static bool CharIsLowerLetter(char c)
        {
            return LOWER_LETTERS_CHARSET.Contains(c);
        }

        public static bool CharIsWord(char c)
        {
            return WORD_CHARSET.Contains(c);
        }

        public static bool CharIsStrictWord(char c)
        {
            return WORD_STRICT_CHARSET.Contains(c);
        }

        public static bool ReadIdentifier(StringScanner scanner, out string identifier)
        {
            using (var token = scanner.SaveIndex())
            {

                if (char.IsLetter(scanner.CurrentChar) ||
                    scanner.CurrentChar == '_')
                {
                    scanner.Increment();

                    while (char.IsLetterOrDigit(scanner.CurrentChar) ||
                        scanner.CurrentChar == '_')
                        scanner.Increment();

                    identifier = scanner.GetString(token);
                    return true;
                }

                scanner.RestoreIndex(token);
                identifier = null;
                return false;
            }
        }

        public static bool ReadDecimalString(StringScanner scanner, out string decimalStr,
            out bool isFloatingPoint, out bool hasExponent)
        {
            using (var token = scanner.SaveIndex())
            {
                isFloatingPoint = false;
                hasExponent = false;

                if (skipNumberChars(scanner))
                {
                    if (scanner.CurrentChar == '.')
                    {
                        scanner.Increment();

                        isFloatingPoint = true;

                        skipNumberChars(scanner);
                    }

                    if (scanner.CurrentChar == 'e' ||
                        scanner.CurrentChar == 'E')
                    {
                        scanner.Increment();

                        hasExponent = true;

                        if (scanner.CurrentChar == '+' ||
                            scanner.CurrentChar == '-')
                        {
                            scanner.Increment();

                            isFloatingPoint = true;
                        }

                        if (!skipNumberChars(scanner))
                            throw new FormatException();
                    }

                    decimalStr = scanner.GetString(token);
                    return true;
                }

                scanner.RestoreIndex(token);

                decimalStr = null;
                return false;
            }
        }

        private static bool skipNumberChars(StringScanner scanner)
        {
            var result = false;

            while (char.IsNumber(scanner.CurrentChar))
            {
                scanner.Increment();
                result = true;
            }

            return result;
        }

        public static bool ReadDecimalString(StringScanner scanner, out string decimalStr)
        {
            bool isFloatingPoint, hasExponent;

            return ReadDecimalString(scanner, out decimalStr, out isFloatingPoint, out hasExponent);
        }
    }
}