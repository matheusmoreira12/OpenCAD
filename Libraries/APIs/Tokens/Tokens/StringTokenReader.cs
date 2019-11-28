using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCAD.APIs.Tokens
{
    public class StringTokenReader : IDisposable
    {
        private StringTokenReader(StringTokenReader parent, int offset)
        {
            Tokens = parent.Tokens;
            Parent = parent ?? throw new ArgumentNullException(nameof(parent));
            CurrentIndex = StartIndex = (parent.CurrentIndex + offset);
        }

        /// <summary>
        /// Creates a token reader from the specified tokens.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        /// <param name="startIndex"></param>
        public StringTokenReader(IReadOnlyList<StringToken> tokens, int startIndex = 0)
        {
            Tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            CurrentIndex = StartIndex = 0;
            Parent = null;
        }

        private void saveIndexToParent()
        {
            if (indexDiscarded) return;

            if (Parent is null) return;

            Parent.CurrentIndex = CurrentIndex;
        }

        /// <summary>
        /// Discards the resulting index;
        /// </summary>
        public void DiscardIndex() => indexDiscarded = true;

        /// <summary>
        /// Goes to the next token.
        /// </summary>
        /// <returns>The resulting index.</returns>
        public int Next() => CurrentIndex++;

        /// <summary>
        /// Goes to the previous token.
        /// </summary>
        /// <returns>The resulting index.</returns>
        public int Previous() => CurrentIndex--;

        /// <summary>
        /// Skips the specified number of tokens.
        /// </summary>
        /// <param name="steps">The number of tokens to skip.</param>
        /// <returns>The resulting index.</returns>
        public int Skip(int steps) => CurrentIndex += steps;

        /// <summary>
        /// Jumps to an specific token.
        /// </summary>
        /// <param name="index">The index of the token.</param>
        public void Jump(int index) => CurrentIndex = index;

        /// <summary>
        /// Branches out into a different reader.
        /// </summary>
        /// <returns></returns>
        public StringTokenReader Derive(int offset = 0) => new StringTokenReader(this, offset);

        private bool indexDiscarded = false;

        /// <summary>
        /// Gets all the tokens being read.
        /// </summary>
        public IReadOnlyList<StringToken> Tokens { get; }

        /// <summary>
        /// Gets the parent of this reader.
        /// </summary>
        public StringTokenReader Parent { get; }

        /// <summary>
        /// Gets the start index for this reader.
        /// </summary>
        public int StartIndex { get; }

        /// <summary>
        /// Gets or sets the current index for this reader.
        /// </summary>
        public int CurrentIndex;

        /// <summary>
        /// Gets the current token being read.
        /// </summary>
        public StringToken CurrentToken
        {
            get
            {
                if (CurrentIndex < 0) return null;

                if (CurrentIndex >= Tokens.Count) return null;

                return Tokens[CurrentIndex];
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    saveIndexToParent();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
