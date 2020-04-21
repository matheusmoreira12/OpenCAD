using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;

namespace OpenCAD.APIs.Measures.Conversion
{
    public class Tree<T> : IEnumerable<TreeItem<T>>
    {
        protected static Dictionary<TreeItem<T>, Tree<T>> StoredParents { get; }
            = new Dictionary<TreeItem<T>, Tree<T>> { };

        public Tree()
        {
            _Children = new List<TreeItem<T>> { };
        }

        public Tree(IList<TreeItem<T>> children)
        {
            children = new List<TreeItem<T>>(children);
        }

        public override string ToString()
        {
            /*var builder = new StringBuilder();
            if (this is TreeItem<T>)
            {
                var treeItem = (TreeItem<T>)this;
                builder.AppendLine(treeItem.ToString() ?? "(empty)");
            }
            foreach (var child in Children)
            {
                builder.AppendLine("\t");
                builder.Append($"{child?.ToString() ?? "(empty)"} ");
            }
            return builder.ToString();
            */
            return null;
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
            _Children.Add(child ?? throw new ArgumentNullException(nameof(child)));
            StoredParents[child] = this;
        }

        public void RemoveChild(TreeItem<T> child)
        {
            if (StoredParents.ContainsKey(child))
                throw new InvalidOperationException("The specified tree item already " +
                    "has a parent.");
            _Children.Remove(child ?? throw new ArgumentNullException(nameof(child)));
            StoredParents[child] = null;
        }

        IEnumerator<TreeItem<T>> IEnumerable<TreeItem<T>>.GetEnumerator() => _Children
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TreeItem<T>>)this)
            .GetEnumerator();

        public TreeItem<T>[] Children => _Children.ToArray();
        protected List<TreeItem<T>> _Children { get; }
    }
}