using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using OpenCAD.OpenCADFormat.Drawing;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    public class ComponentSymbol
    {
        [XmlAttribute]
        public DrawingGroup Drawing;

        [XmlAttribute]
        public ComponentPinCollection Pins;

        public ComponentSymbol()
        {
            Drawing = new DrawingGroup();

            Pins = new ComponentPinCollection();
        }
    }
}