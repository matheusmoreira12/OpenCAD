using System;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.DocumentFoundation;
using OpenCAD.OpenCADFormat.MetaInformation;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    public abstract class Library: Document
    {
        [XmlAttribute]
        public string Name = "*";

        [XmlAttribute]
        public string Description = "*";
    }
}