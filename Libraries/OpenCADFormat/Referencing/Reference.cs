using System;

namespace OpenCAD.OpenCADFormat.Libraries
{
    public sealed class Reference<T>
    {
        public static Reference<T> NotAssigned = new Reference<T>(null, false);

        public static Reference<T> FromPath(string path) => new Reference<T>(path, true);

        private Reference(string path, bool isAssociated)
        {
            Path = path ?? throw new ArgumentNullException(nameof(path));
            IsAssociated = isAssociated;
        }

        private readonly string Path;

        public readonly bool IsAssociated;

        public T Resolve => default; //TODO: Resolve references
    }
}