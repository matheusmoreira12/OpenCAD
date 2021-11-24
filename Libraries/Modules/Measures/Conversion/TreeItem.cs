using System;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures.Conversion
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

        public Tree<T> Parent { get; internal set; } = null;
    }
}