using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenCAD.APIs.Measures.Conversion
{
    public class TreeItemCollection<T> : Collection<TreeItem<T>>
    {
        public TreeItemCollection(Tree<T> parent)
        {
            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        private Tree<T> parent;

        private void abandonItem(TreeItem<T> item) => item.Parent = null;

        private void adoptItem(TreeItem<T> item)
        {
            if (item.Parent != null)
                item.Parent.Children.Remove(item);
            item.Parent = parent;
        }

        protected override void SetItem(int index, TreeItem<T> item)
        {
            abandonItem(this[index]);

            base.SetItem(index, item);

            adoptItem(item);
        }

        protected override void InsertItem(int index, TreeItem<T> item)
        {
            base.InsertItem(index, item);

            adoptItem(item);
        }

        protected override void RemoveItem(int index)
        {
            abandonItem(this[index]);

            base.RemoveItem(index);
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
                abandonItem(item);

            base.ClearItems();
        }

        public void AddRange(IList<TreeItem<T>> items)
        {
            foreach (var item in items.ToList())
                Add(item);
        }
    }
}