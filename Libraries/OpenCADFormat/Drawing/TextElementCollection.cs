using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public class TextElementCollection : Collection<object>
    {
        public TextElementCollection(TextElement parent, IList<object> items) : base(items)
        {
            Parent = parent;
        }

        public TextElementCollection(TextElement parent)
        {
            Parent = parent;
        }

        public void AddRange(IEnumerable<TextElement> items)
        {
            foreach (var item in items)
                Add(item);
        }

        public void InsertRange(int index, IEnumerable<TextElement> items)
        {
            foreach (var item in items)
            {
                Insert(index, item);

                index++;
            }
        }

        protected override void InsertItem(int index, object item)
        {
            base.InsertItem(index, item);

            (item as TextElement)?.SetParent(Parent);
        }

        protected override void RemoveItem(int index)
        {
            object item = this[index];

            (item as TextElement)?.UnsetParent();

            base.RemoveItem(index);
        }

        public TextElement Parent { get; private set; }

        internal void CollapseAll()
        {
            for (int i = Count - 1; i > 0; i--)
            {
                TextElement oldItem = this[i] as TextElement;

                if (oldItem == null) continue;

                TextElement newItem = oldItem.Collapse();

                if (newItem == null)
                    RemoveAt(i);
                else
                    SetItem(i, newItem);
            }
        }
    }
}