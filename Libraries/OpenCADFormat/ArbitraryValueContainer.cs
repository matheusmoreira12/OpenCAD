using System;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat
{
    [Serializable]
    public struct ArbitraryValueContainer
    {
        public static ArbitraryValueContainer Empty = new ArbitraryValueContainer();

        [XmlAttribute]
        public bool IsValueSet { get; }

        public ArbitraryValueContainer(object heldValue) : this()
        {
            HeldValue = heldValue ?? throw new ArgumentNullException(nameof(heldValue));
            IsValueSet = true;
        }

        [XmlAnyElement]
        public object HeldValue { get; }
    }
}