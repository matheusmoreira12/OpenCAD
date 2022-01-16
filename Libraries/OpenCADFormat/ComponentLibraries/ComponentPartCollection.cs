using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    public sealed class ComponentPartCollection : Collection<ComponentPart>
    {
        public ComponentPartCollection()
        {
        }

        public ComponentPartCollection(IList<ComponentPart> list) : base(list)
        {
        }
    }
}
