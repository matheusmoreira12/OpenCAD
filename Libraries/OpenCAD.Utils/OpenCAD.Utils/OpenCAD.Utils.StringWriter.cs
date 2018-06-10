using System;
using System.Collections.Generic;

namespace OpenCAD.Utils
{
    public sealed class StringWriterSaveContentToken : IDisposable
    {
        private bool disposedValue = false;
        private StringWriter parentWriter;

        void IDisposable.Dispose()
        {
            if (!disposedValue)
                parentWriter.SavedContentTokenDisposed(this);

            disposedValue = true;
        }

        internal StringWriterSaveContentToken(StringWriter parentWriter)
        {
            this.parentWriter = parentWriter;

            parentWriter.SavedContentTokenCreated(this);
        }
    }

    public class StringWriter
    {
        /// <summary>
        /// Gets or sets the current content string.
        /// </summary>
        public string Content;

        private Dictionary<StringWriterSaveContentToken, string> savedContentTokens =
            new Dictionary<StringWriterSaveContentToken, string> { };

        /// <summary>
        /// Saves a copy of the current content and returns a saved content token.
        /// </summary>
        /// <returns>The resulting saved content token object.</returns>
        public StringWriterSaveContentToken SaveContent()
        {
            return new StringWriterSaveContentToken(this);
        }

        /// <summary>
        /// Gets the saved content from the specified saved content token.
        /// </summary>
        /// <param name="token">The saved content token object.</param>
        /// <returns>The saved string.</returns>
        public string GetSavedContent(StringWriterSaveContentToken token)
        {
            return savedContentTokens[token];
        }

        /// <summary>
        /// Restores the saved content from the specified saved content token.
        /// </summary>
        /// <param name="token">The saved content token object.</param>
        public void RestoreContent(StringWriterSaveContentToken token)
        {
            Content = GetSavedContent(token);
        }

        internal void SavedContentTokenCreated(StringWriterSaveContentToken token)
        {
            savedContentTokens.Add(token, Content);
        }

        internal void SavedContentTokenDisposed(StringWriterSaveContentToken token)
        {
            savedContentTokens.Remove(token);
        }

        public StringWriter(string initialContent)
        {
            Content = initialContent;
        }
    }
}