using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.SchematicCapture
{
    public sealed class NetCollection : Collection<Net>
    {
        public NetCollection()
        {
        }

        public NetCollection(IList<Net> list) : base(list)
        {
        }
    }
}