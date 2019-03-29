using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    [XmlRoot]
    public class ComponentLibrary : Library
    {

        [XmlArray]
        [XmlArrayItem("Component")]
        public List<Component> Components;

        public ComponentLibrary()
        {
            Components = new List<Component>();
        }
    }
}