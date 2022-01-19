using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    public class ComponentCollection : Collection<Component>
    {
        public ComponentCollection()
        {
        }

        public ComponentCollection(IList<Component> list) : base(list)
        {
        }
    }
}