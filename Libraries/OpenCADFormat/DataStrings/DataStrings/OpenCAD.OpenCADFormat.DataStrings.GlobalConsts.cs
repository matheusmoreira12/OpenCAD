using System.Linq;

using OpenCAD.Utils;

namespace OpenCAD.OpenCADFormat.DataStrings
{

    internal static class GlobalConsts
    {
        private static char[] getCharRange(char start, char end)
        {
            return Enumerable.Range(start, end - start + 1).Select(i => (char)i).ToArray();
        }

        public static readonly char[] NUMERIC_CHARSET = getCharRange('0', '9');
        public static readonly char[] BINARY_CHARSET = new[] { '0', '1' };
        public static readonly char[] OCTAL_CHARSET = getCharRange('1', '8');
        public static readonly char[] HEXADECIMAL_CHARSET = NUMERIC_CHARSET.Concat(getCharRange('a', 'f')).ToArray();
        public static readonly char[] ALPHANUMERIC_CHARSET = StringUtils.LETTERS_CHARSET.Concat(StringUtils.NUMERAL_CHARSET).ToArray();
        public static readonly char[] WHITESPACE_CHARACTERS = new[] { ' ', '\n' };

        public const char SEPARATOR_CHARACTER = ';';
        public const char FUNC_PARAMS_OPENING_CHAR = '(';
        public const char FUNC_PARAMS_CLOSING_CHAR = ')';
        public const char PARAM_NAME_SEPARATOR_CHAR = ':';
        public const char STRING_ENCLOSING_CHAR = '\'';
    }

}