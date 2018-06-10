using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    public abstract class Library
    {
        [XmlArray("Parameters")]
        [XmlArrayItem(ElementName = "Parameter")]
        public ParameterList Parameters;

        public Library()
        {
            Parameters = new ParameterList {
                new Parameter{ Key = "Name", Value = "*", IsRequired = true },
                new Parameter{ Key = "Description", Value = "*", IsRequired = true },
                new Parameter{ Key = "Comment", Value = "" },
            };
        }
    }
}