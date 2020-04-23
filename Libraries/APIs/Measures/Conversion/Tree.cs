using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace OpenCAD.APIs.Measures.Conversion
{
    public class Tree<T>
    {
        protected static Dictionary<TreeItem<T>, Tree<T>> Parents { get; }
            = new Dictionary<TreeItem<T>, Tree<T>> { };

        public Tree()
        {
            children = new List<TreeItem<T>> { };
        }

        public Tree(IList<TreeItem<T>> children)
        {
            this.children = new List<TreeItem<T>>(children);
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var child in Children)
            {
                builder.AppendLine("conversion tree:");
                builder.AppendLine(child.ToString());
            }
            return builder.ToString();
        }

        public TreeItem<T> ToTreeItem(UnitConversion value)
        {
            if (this is TreeItem<T>)
                return (TreeItem<T>)this;
            else
                return new TreeItem<T>(value, Children);
        }

        public void AddChild(TreeItem<T> child)
        {
            if (Parents.ContainsKey(child))
                throw new InvalidOperationException("The specified tree item already " +
                    "has a parent.");
            else
            {
                children.Add(child ?? throw new ArgumentNullException(nameof(child)));
                Parents[child] = this;
            }
        }

        public void RemoveChild(TreeItem<T> child)
        {
            children.Remove(child ?? throw new ArgumentNullException(nameof(child)));
            Parents[child] = null;
        }

        public TreeItem<T>[] Children => children.ToArray();
        protected List<TreeItem<T>> children { get; }
    }
}