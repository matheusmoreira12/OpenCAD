using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Drawing
{
    /// <summary>
    /// Represents a group of drawing elements.
    /// </summary>
    [Serializable]
    public class DrawingGroup
    {
        /// <summary>
        /// Gets the drawing elements contained within this drawing group.
        /// </summary>
        [XmlArray("Elements")]
        [XmlElement("Arc", typeof(Arc))]
        [XmlElement("Ellipse", typeof(Ellipse))]
        [XmlElement("Image", typeof(Image))]
        [XmlElement("Line", typeof(Line))]
        [XmlElement("Polygon", typeof(Polygon))]
        [XmlElement("Polyline", typeof(Polyline))]
        [XmlElement("Rectangle", typeof(Polyline))]
        [XmlElement("Path", typeof(Path))]
        public List<DrawingElement> Elements { get; }

        /// <summary>
        /// Creates an empty drawing group.
        /// </summary>
        public DrawingGroup()
        {
            Elements = new List<DrawingElement>();
        }

        /// <summary>
        /// Creates a drawing group containing the specified drawing elements.
        /// </summary>
        /// <param name="elements">A list containing the drawing elements.</param>
        public DrawingGroup(IList<DrawingElement> elements)
        {
            Elements = new List<DrawingElement>(elements);
        }
    }
}
