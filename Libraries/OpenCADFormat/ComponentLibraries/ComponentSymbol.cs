using OpenCAD.OpenCADFormat.Drawing;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.ComponentLibraries
{
    public sealed class ComponentSymbol
    {
        public ComponentSymbol(DrawingGroup drawing, IList<ComponentPin> pins)
        {
            Drawing = drawing ?? throw new ArgumentNullException(nameof(drawing));
            Pins = new ComponentPinCollection(pins ?? throw new ArgumentNullException(nameof(pins)));
        }

        public DrawingGroup Drawing;

        public ComponentPinCollection Pins;
    }
}