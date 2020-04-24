using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenCAD.APIs.Measures.Conversion
{
    public class Tree<T>
    {
        public Tree()
        {
            Children = new TreeItemCollection<T>(this);
        }

        public Tree(IList<TreeItem<T>> children) : this()
        {
            Children.AddRange(children);
        }

        public TreeItem<T> ToTreeItem(UnitConversion value)
        {
            if (this is TreeItem<T>)
                return (TreeItem<T>)this;
            else
                return new TreeItem<T>(value, Children);
        }

        public TreeItemCollection<T> Children { get; }
    }
}