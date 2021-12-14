using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public abstract class TextElement: DrawingElement
    {
        public TextElement()
        {
            Children = new TextElementCollection(this);
        }

        public TextElement(IList<object> children)
        {
            Children = new TextElementCollection(this, children);
        }

        internal void SetParent(TextElement parent) => Parent = parent;
        internal void UnsetParent() => Parent = null;

        [XmlElement("SemiBold", typeof(TextSemiBoldElement))]
        [XmlElement("Bold", typeof(TextBoldElement))]
        [XmlElement("Black", typeof(TextBlackElement))]
        [XmlElement("Light", typeof(TextLightElement))]
        [XmlElement("Italic", typeof(TextItalicElement))]
        [XmlElement("Oblique", typeof(TextObliqueElement))]
        [XmlElement("Underline", typeof(TextUnderlineElement))]
        [XmlElement("StrikeThrough", typeof(TextStrikeThroughElement))]
        [XmlElement("Overline", typeof(TextOverlineElement))]
        [XmlElement("Subscript", typeof(TextSubscriptElement))]
        [XmlElement("Superscript", typeof(TextSuperscriptElement))]
        [XmlElement("Font", typeof(TextFontElement))]
        [XmlText(typeof(string))]
        public TextElementCollection Children;

        [XmlIgnore]
        public bool HasChildren => Children.Count > 0;

        public TextElement Collapse()
        {
            if (ShouldCollapse)
            {
                Children.CollapseAll();

                return null;
            }

            return this;
        }

        [XmlIgnore]
        protected virtual bool ShouldCollapse => !HasChildren;

        [XmlIgnore]
        public TextElement Parent { get; private set; }

    }
}