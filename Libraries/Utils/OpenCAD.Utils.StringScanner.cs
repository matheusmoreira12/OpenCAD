using System.Collections.Generic;

namespace OpenCAD.Utils
{

    public class StringScanner
    {
        /// <summary>
        /// The text content being scanned.
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// The scan position inside the string.
        /// </summary>
        public int CurrentIndex { get; private set; }

        /// <summary>
        /// The scan position at which the scan will start.
        /// </summary>
        public int StartIndex { get; private set; }

        private Dictionary<StringScannerSaveIndexToken, int> savedIndexTokens = new Dictionary<StringScannerSaveIndexToken, int> { };

        /// <summary>
        /// Saves the current index and returns the saved index token.
        /// </summary>
        /// <returns>The saved index token object.</returns>
        public StringScannerSaveIndexToken SaveIndex()
        {
            return new StringScannerSaveIndexToken(this);
        }

        /// <summary>
        /// Gets the index of the specified saved index token.
        /// </summary>
        /// <param name="token">The saved index token object.</param>
        public int GetIndex(StringScannerSaveIndexToken token)
        {
            return savedIndexTokens[token];
        }

        /// <summary>
        /// Gets the index relative to the specified saved index token.
        /// </summary>
        /// <param name="token">The saved index token object.</param>
        public int GetRelativeIndex(StringScannerSaveIndexToken token)
        {
            return CurrentIndex - GetIndex(token);
        }

        /// <summary>
        /// Restores the saved index from the specified saved index token.
        /// </summary>
        /// <param name="token">The saved index token object.</param>
        public void RestoreIndex(StringScannerSaveIndexToken token)
        {
            CurrentIndex = GetIndex(token);
        }

        /// <summary>
        /// Gets the string, starting at the specified saved index token, trimming start and end by the specified amount.
        /// </summary>
        /// <param name="token">The saved index token object.</param>
        /// <param name="trimStart">The amount to trim the start of the string.</param>
        /// <param name="trimEnd">The amount to trim the end of the string.</param>
        /// <returns></returns>
        public string GetString(StringScannerSaveIndexToken token, int trimStart = 0, int trimEnd = 0)
        {
            int start = GetIndex(token) + trimStart;

            return Content.Substring(start, CurrentIndex - start - trimEnd);
        }

        internal void SavedIndexTokenCreated(StringScannerSaveIndexToken token)
        {
            savedIndexTokens.Add(token, CurrentIndex);
        }

        internal void SavedIndexTokenDisposed(StringScannerSaveIndexToken token)
        {
            savedIndexTokens.Remove(token);
        }

        /// <summary>
        /// Increments the scan position by 1.
        /// </summary>
        public void Increment()
        {
            CurrentIndex++;
        }

        /// <summary>
        /// Decrements the scan position by 1.
        /// </summary>
        public void Decrement()
        {
            CurrentIndex++;
        }

        /// <summary>
        /// Gets a value indicating whether the start of the string has been reached.
        /// </summary>
        public bool StartReached => CurrentIndex <= 0;

        /// <summary>
        /// Gets a value indicating whether the end of the string has been reached.
        /// </summary>
        public bool EndReached => CurrentIndex >= Content.Length;

        /// <summary>
        /// Gets the current char being scanned
        /// </summary>
        public char CurrentChar
        {
            get
            {
                if (CurrentIndex < Content.Length)
                    return Content[CurrentIndex];
                else
                    return (char)0;
            }
        }

        /// <summary>
        /// Creates an instance of a string scanner.
        /// </summary>
        /// <param name="content">The content that is being scanned through.</param>
        /// <param name="startIndex">The index at which to start scanning.</param>
        public StringScanner(string content, int startIndex = 0)
        {
            Content = content;
            CurrentIndex = startIndex;
        }
    }
}