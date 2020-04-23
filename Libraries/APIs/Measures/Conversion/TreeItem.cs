using System;
using System.Collections.Generic;
using System.Text;

namespace OpenCAD.APIs.Measures.Conversion
{

    public sealed class TreeItem<T> : Tree<T>
    {
        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine("conversion: ");
            builder.Append(Value.ToString());
            builder.AppendLine(base.ToString());
            return builder.ToString();
        }

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
                if (Parents.TryGetValue(this, out parent))
                    return parent;
                else
                    return null;
            }
        }
    }
}