using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public class TextElementCollection : Collection<TextElement>
    {
        public TextElementCollection(TextElement parent, IList<TextElement> items): base(items)
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

        protected override void InsertItem(int index, TextElement item)
        {
            base.InsertItem(index, item);

            item.SetParent(Parent);
        }

        protected override void RemoveItem(int index)
        {
            this[index].UnsetParent();

            base.RemoveItem(index);
        }

        public TextElement Parent { get; private set; }

        internal void CollapseAll()
        {
            for (int i = Count - 1; i > 0; i--)
            {
                TextElement newItem = this[i].Collapse();

                if (newItem == null)
                    RemoveAt(i);
                else
                    SetItem(i, newItem);
            }
        }
    }
}