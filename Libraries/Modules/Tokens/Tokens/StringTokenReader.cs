﻿using System;
using System.Collections.Generic;

namespace OpenCAD.Modules.Tokens
{
    public class StringTokenReader<TToken> : IDisposable where TToken : StringToken
    {
        /// <summary>
        /// Creates a token reader from the specified tokens.
        /// </summary>
        /// <param name="tokens">The list of tokens.</param>
        /// <param name="startIndex"></param>
        public StringTokenReader(IReadOnlyList<StringToken> tokens, int startIndex = 0)
        {
            Tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            CurrentIndex = StartIndex = startIndex;
            Parent = null;
        }

        private void saveIndexToParent() => Parent.CurrentIndex = CurrentIndex;

        private bool shouldSaveIndexToParent => !indexDiscarded && !(Parent is null);

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
        public StringTokenReader<TToken> Derive(int offset = 0)
            => new StringTokenReader<TToken>(Tokens, CurrentIndex + offset) { Parent = this };

        private bool indexDiscarded = false;

        /// <summary>
        /// Gets all the tokens being read.
        /// </summary>
        public readonly IReadOnlyList<StringToken> Tokens;

        /// <summary>
        /// Gets the parent of this reader.
        /// </summary>
        public StringTokenReader<TToken> Parent { get; private set; }

        /// <summary>
        /// Gets the start index for this reader.
        /// </summary>
        public readonly int StartIndex;

        /// <summary>
        /// Gets or sets the current index for this reader.
        /// </summary>
        public int CurrentIndex { get; private set; }

        /// <summary>
        /// Gets the current token being read.
        /// </summary>
        public StringToken CurrentToken => CurrentIndex > 0 && CurrentIndex < Tokens.Count - 1 ? Tokens[CurrentIndex] : null;

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (shouldSaveIndexToParent)
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
