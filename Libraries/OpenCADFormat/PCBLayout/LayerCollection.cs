using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.PCBLayout
{
    public sealed class LayerCollection : Collection<Layer>
    {
        public LayerCollection()
        {
        }

        public LayerCollection(IList<Layer> list) : base(list)
        {
        }
    }
}
