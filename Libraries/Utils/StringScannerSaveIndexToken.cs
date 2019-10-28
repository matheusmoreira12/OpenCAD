using System;

namespace OpenCAD.Utils
{
    public sealed class StringScannerSaveIndexToken : IDisposable
    {
        private bool disposedValue = false;
        private StringScanner parentScanner;

        public void Dispose()
        {
            if (!disposedValue)
                parentScanner.SavedIndexTokenDisposed(this);

            disposedValue = true;
        }

        internal StringScannerSaveIndexToken(StringScanner parentScanner)
        {
            this.parentScanner = parentScanner;

            parentScanner.SavedIndexTokenCreated(this);
        }
    }
}