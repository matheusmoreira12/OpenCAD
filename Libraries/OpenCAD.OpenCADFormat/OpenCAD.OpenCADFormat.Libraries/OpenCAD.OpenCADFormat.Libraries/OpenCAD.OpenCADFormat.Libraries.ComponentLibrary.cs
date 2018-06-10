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
                [XmlArray("Parameters")]
                [XmlArrayItem(ElementName = "Parameter")]
                public ParameterList Parameters;

                public Part()
                {
                    Parameters = new ParameterList {
                        new Parameter{ Key = "Order", Value = "?", IsRequired = true },
                        new Parameter{ Key = "Comment", Value = "" },
                    };
                }
            }

            [XmlArray("Parameters")]
            [XmlArrayItem(ElementName = "Parameter")]
            public ParameterList Parameters;

            [XmlArray("Parts")]
            [XmlArrayItem(ElementName = "Part")]
            public List<Part> Parts;

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
            }
        }

        [XmlArray("Components")]
        [XmlArrayItem(ElementName = "Component")]
        public List<Component> Components;

        public ComponentLibrary()
        {
            Components = new List<Component>();
        }
    }
}