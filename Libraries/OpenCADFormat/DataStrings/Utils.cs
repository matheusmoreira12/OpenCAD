using System.Linq;

using OpenCAD.Utils;

namespace OpenCAD.OpenCADFormat.DataStrings
{
    internal static class Utils
    {
        public static string EncloseString(string content)
        {
            return string.Format("{0}{1}{0}", GlobalConsts.STRING_ENCLOSING_CHAR, content);
        }

        public static bool ReadIdentifier(StringScanner scanner, out string identifier)
        {
            using (var token = scanner.SaveIndex())
            {
                if (StringUtils.CharIsLetter(scanner.CurrentChar))
                {
                    scanner.Increment();

                    while (StringUtils.WORD_CHARSET.Contains(scanner.CurrentChar))
                        scanner.Increment();

                    if (scanner.CurrentIndex > scanner.GetIndex(token))
                    {
                        identifier = scanner.GetString(token);
                        return true;
                    }
                }

                scanner.RestoreIndex(token);
                identifier = null;
                return false;
            }
        }

        public static bool ReadDataStringSeparator(StringScanner scanner)
        {
            if (scanner.CurrentChar == GlobalConsts.SEPARATOR_CHARACTER)
            {
                scanner.Increment();

                ReadDataStringWhitespace(scanner);

                return true;
            }

            return false;
        }

        public static bool ReadDataStringWhitespace(StringScanner scanner)
        {
            int initialIndex = scanner.CurrentIndex;

            while (GlobalConsts.WHITESPACE_CHARACTERS.Contains(scanner.CurrentChar))
                scanner.Increment();

            return scanner.CurrentIndex - initialIndex > 0;
        }
    }

}