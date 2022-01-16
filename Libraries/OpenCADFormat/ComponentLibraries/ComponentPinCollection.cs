using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    public class ComponentPinCollection : Collection<ComponentPin>
    {
        public ComponentPinCollection()
        {
        }

        public ComponentPinCollection(IList<ComponentPin> list) : base(list)
        {
        }
    }
}
