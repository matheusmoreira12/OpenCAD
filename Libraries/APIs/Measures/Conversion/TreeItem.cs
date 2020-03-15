using System;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures.UnitConversion
{

    public sealed class TreeItem<T> : Tree<T>
    {
        public TreeItem(UnitConversion value) : base()
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public TreeItem(UnitConversion value, IList<TreeItem<T>> children) : base(children)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public UnitConversion Value { get; }

        public Tree<T> Parent
        {
            get
            {
                Tree<T> parent;
                if (StoredParents.TryGetValue(this, out parent))
                    return parent;
                else
                    return null;
            }
        }
    }
}