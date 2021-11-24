using OpenCAD.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.APIs.Tokens
{

    public abstract class StringTokenifier<TToken> where TToken: StringToken
    {
        protected StringTokenifier(string content)
        {
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        public string Content { get; }

        protected IEnumerable<TToken> readAll(StringScanner scanner)
        {
            TToken token;

            while (readToken(scanner, out token))
                yield return token;
        }

        protected abstract bool readToken(StringScanner scanner, out TToken result);

        public TToken[] Tokenify()
        {
            StringScanner scanner = new StringScanner(Content);

            return readAll(scanner).ToArray();
        }
    }
}
