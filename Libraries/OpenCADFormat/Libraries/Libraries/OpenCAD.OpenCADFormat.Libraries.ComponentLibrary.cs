using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    [XmlRoot(ElementName = "ComponentLibrary")]
    public class ComponentLibrary : Library
    {
        [Serializable]
        public class Component
        {
            [Serializable]
            public class Part
            {
                [XmlArray]
                [XmlArrayItem(ElementName = "Parameter")]
                public ParameterList Parameters;

                [XmlElement]
                public SchematicCapture Symbol;

                public Part()
                {
                    Parameters = new ParameterList {
                        new Parameter{ Key = "Order", Value = "?", IsRequired = true },
                        new Parameter{ Key = "Comment", Value = "" },
                    };
                }
            }

            [XmlArray]
            [XmlArrayItem(ElementName = "Parameter")]
            public ParameterList Parameters;

            [XmlArray]
            [XmlArrayItem(ElementName = "Part")]
            public List<Part> Parts;

            public List<FootprintLibrary.Footprint> Footprints;

            public Component()
            {
                Parameters = new ParameterList {
                    new Parameter{ Key = "Reference Designator", Value = "*?", IsRequired = true },
                    new Parameter{ Key = "Name", Value = "*", IsRequired = true },
                    new Parameter{ Key = "Value", Value = "*", IsRequired = true },
                    new Parameter{ Key = "Manufacturer", Value = "" },
                    new Parameter{ Key = "Datasheet", Value = "" },
                    new Parameter{ Key = "Comment", Value = "" },
                };

                Parts = new List<Part>
                {
                    new Part(),
                };

                Footprints = new List<FootprintLibrary.Footprint>();
            }
        }

        [XmlArray]
        [XmlArrayItem(ElementName = "Component")]
        public List<Component> Components;

        public ComponentLibrary()
        {
            Components = new List<Component>();
        }
    }
}