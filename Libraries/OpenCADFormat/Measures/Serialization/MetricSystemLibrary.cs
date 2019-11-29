using OpenCAD.OpenCADFormat.DocumentFoundation;
using System.IO;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Measures.Serialization
{
    [XmlRoot("MetricSystemLibrary")]
    public class MetricSystemLibrary: Document
    {
        public static MetricSystemLibrary Load(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                var serializer = new XmlSerializer(typeof(MetricSystemLibrary));
                return (MetricSystemLibrary)serializer.Deserialize(stream);
            }
        }

        [XmlElement("MetricSystem")]
        public MetricSystemNode[] MetricSystems = new MetricSystemNode[0];

        public void Save(string path)
        {
            using (var stream = new FileStream(path, FileMode.CreateNew))
            {
                var serializer = new XmlSerializer(typeof(MetricSystemLibrary));
                serializer.Serialize(stream, this);
            }
        }
    }
}