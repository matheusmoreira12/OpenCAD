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
        public static readonly char[] WORD_CHARSET = NUMERAL_CHARSET.Concat(LETTERS_CHARSET).Concat(new char[] { '_' }).ToArray();

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

        public static bool ReadIdentifier(StringScanner scanner, out string identifier)
        {
            using (var token = scanner.SaveIndex())
            {

                if (CharIsLetter(scanner.CurrentChar))
                {
                    scanner.Increment();

                    while (CharIsWord(scanner.CurrentChar))
                        scanner.Increment();

                    identifier = scanner.GetString(token);
                    return true;
                }

                scanner.RestoreIndex(token);
                identifier = null;
                return false;
            }
        }

        private static bool skipNumeralChars(StringScanner scanner)
        {
            var result = false;

            while (CharIsNumeral(scanner.CurrentChar))
            {
                scanner.Increment();
                result = true;
            }

            return result;
        }

        private static bool skipSignChars(StringScanner scanner)
        {
            var result = false;

            while (DECIMAL_SIGN_CHARACTERS.Contains(scanner.CurrentChar))
            {
                scanner.Increment();
                result = true;
            }

            return result;
        }

        public static bool ReadDecimalString(StringScanner scanner, out string decimalStr,
            out bool isFloatingPoint, out bool hasExponent)
        {
            using (var t = scanner.SaveIndex())
            {
                isFloatingPoint = false;
                hasExponent = false;

                if (CharIsDecimal(scanner.CurrentChar))
                {
                    scanner.Increment();

                    skipNumeralChars(scanner);

                    if (scanner.CurrentChar == DECIMAL_SEPARATOR)
                    {
                        scanner.Increment();

                        isFloatingPoint = true;

                        skipNumeralChars(scanner);
                    }

                    if (scanner.CurrentChar == DECIMAL_EXPONENT_CHARACTER)
                    {
                        scanner.Increment();

                        if (skipSignChars(scanner))
                            isFloatingPoint = true;

                        if (skipNumeralChars(scanner))
                            hasExponent = true;
                        else
                            throw new InvalidOperationException($"Unexpected token \"{scanner.CurrentChar}\". " +
                                "Expected an exponent quantifier instead.");
                    }

                    decimalStr = scanner.GetString(t);
                    return true;
                }

                decimalStr = null;
                scanner.RestoreIndex(t);
                return false;
            }
        }
    }
}