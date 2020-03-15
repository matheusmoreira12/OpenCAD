using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace OpenCAD.APIs.Measures.UnitConversion
{
    public class Tree<T> : IEnumerable<TreeItem<T>>
    {
        protected static Dictionary<TreeItem<T>, Tree<T>> StoredParents { get; }
            = new Dictionary<TreeItem<T>, Tree<T>> { };

        public Tree()
        {
            Children = new ObservableCollection<TreeItem<T>> { };
            Children.CollectionChanged += this.Children_OnCollectionChanged;
        }

        private void Children_OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add ||
                e.Action == NotifyCollectionChangedAction.Replace)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is TreeItem<T>)
                        StoredParents[(TreeItem<T>)item] = this;
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove ||
                e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is TreeItem<T>)
                        StoredParents.Remove((TreeItem<T>)item);
                }
            }
        }

        public Tree(IList<TreeItem<T>> children)
        {
            Children = new ObservableCollection<TreeItem<T>>(children);
        }

        public override string ToString()
        {
            var writer = new StringBuilder();
            if (this is TreeItem<T>)
            {
                var treeItem = (TreeItem<T>)this;
                writer.AppendLine($"{treeItem.Value}");
            }
            foreach (var child in Children)
                writer.AppendLine(child.ToString());
            return writer.ToString();
        }

        public TreeItem<T> ToTreeItem(UnitConversion value)
        {
            if (this is TreeItem<T>)
                return (TreeItem<T>)this;
            else
                return new TreeItem<T>(value, Children.ToArray());
        }

        IEnumerator<TreeItem<T>> IEnumerable<TreeItem<T>>.GetEnumerator() => Children
            .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable<TreeItem<T>>)this)
            .GetEnumerator();

        public ObservableCollection<TreeItem<T>> Children { get; }
    }

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

        public IEnumerable<Tree<T>> GetParents()
        {
            if (this is TreeItem<T>)
            {
                var treeItem = (TreeItem<T>)this;
                if (!(treeItem.Parent is null))
                {
                    yield return treeItem.Parent;
                    if (treeItem.Parent is TreeItem<T>)
                    {
                        var treeItemParent = (TreeItem<T>)Parent;
                        foreach (var parent in treeItemParent.GetParents())
                            yield return parent;
                    }
                }
            }
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