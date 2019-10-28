using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.Utils
{

    public abstract class StringTokenifier
    {
        protected StringTokenifier(string content)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public string Content { get; }

        protected IEnumerable<StringToken> readAll(StringScanner scanner)
        {
            StringToken token;

            while (readToken(scanner, out token))
                yield return token;
        }

        protected abstract bool readToken(StringScanner scanner, out StringToken result);

        public StringToken[] Tokenify()
        {
            StringScanner scanner = new StringScanner(Content);

            return readAll(scanner).ToArray();
        }
    }
}
