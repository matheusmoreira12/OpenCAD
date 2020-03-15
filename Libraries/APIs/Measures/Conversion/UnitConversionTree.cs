using System;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures.UnitConversion
{
    public class UnitConversionTree
    {
        public UnitConversionTree()
        {
            Children = new List<UnitConversionTreeItem> { };
        }

        public UnitConversionTree(IList<UnitConversionTreeItem> children)
        {
            Children = new List<UnitConversionTreeItem> (children);
        }

        public List<UnitConversionTreeItem> Children { get; }

        public IEnumerable<UnitConversionTreeItem> TraverseBranches()
        {
            if (this is UnitConversionTreeItem)
                yield return (UnitConversionTreeItem)this;

            foreach (var branch in Children)
            {
                var traversedBranches = branch.TraverseBranches();
                foreach (var subBranch in traversedBranches)
                    yield return subBranch;
            }
        }
    }

    public sealed class UnitConversionTreeItem: UnitConversionTree
    {
        public UnitConversionTreeItem(UnitConversion item): base()
        {
            Value = item ?? throw new ArgumentNullException(nameof(item));
        }

        public UnitConversionTreeItem(UnitConversion item, IList<UnitConversionTreeItem> children): base(children)
        {
            Value = item ?? throw new ArgumentNullException(nameof(item));
        }

        public UnitConversion Value { get; }
    }
}