using System;
using System.Collections.Generic;

namespace OpenCAD.APIs.Measures.UnitConversion
{
    public class UnitConversionTree
    {
        public UnitConversionTree()
        {
            Branches = new List<UnitConversionBranch> { };
        }

        public UnitConversionTree(IList<UnitConversionBranch> children)
        {
            Branches = new List<UnitConversionBranch> (children);
        }

        public List<UnitConversionBranch> Branches { get; }

        public IEnumerable<UnitConversionBranch> TraverseBranches()
        {
            foreach (var branch in Branches)
            {
                yield return branch;

                var subBranches = branch.TraverseBranches();
                foreach (var subBranch in subBranches)
                    yield return subBranch;
            }
        }
    }

    public sealed class UnitConversionBranch: UnitConversionTree
    {
        public UnitConversionBranch(UnitConversion item): base()
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public UnitConversionBranch(UnitConversion item, IList<UnitConversionBranch> children): base(children)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
        }

        public UnitConversion Item { get; }
    }
}