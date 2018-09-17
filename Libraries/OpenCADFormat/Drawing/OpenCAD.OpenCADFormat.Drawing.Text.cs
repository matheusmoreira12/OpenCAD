using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.Measures;

using Quantities = OpenCAD.OpenCADFormat.Measures.Quantities;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public abstract class TextElement
    {
        public TextElement()
        {
            Children = new TextElementCollection(this);
        }

        public TextElement(IList<TextElement> children)
        {
            Children = new TextElementCollection(this, children);
        }

        internal void SetParent(TextElement parent) => Parent = parent;
        internal void UnsetParent() => Parent = null;

        [XmlElement("b", Type = typeof(TextBoldElement))]
        [XmlElement("Bolder", Type = typeof(TextBolderElement))]
        [XmlElement("l", Type = typeof(TextLightElement))]
        [XmlElement("Ligher", Type = typeof(TextLighterElement))]
        [XmlElement("i", Type = typeof(TextItalicElement))]
        [XmlElement("o", Type = typeof(TextObliqueElement))]
        [XmlElement("BackSlanted", Type = typeof(TextBackSlantedElement))]
        [XmlElement("u", Type = typeof(TextUnderlineElement))]
        [XmlElement("s", Type = typeof(TextStrikeThroughElement))]
        [XmlElement("Overline", Type = typeof(TextOverlineElement))]
        [XmlElement("sup", Type = typeof(TextSuperscriptElement))]
        [XmlElement("sub", Type = typeof(TextSubscriptElement))]
        [XmlElement(typeof(TextBlock))]
        [XmlElement(typeof(Text))]
        public TextElementCollection Children;

        [XmlIgnore]
        public bool HasChildren => Children.Count > 0;

        public TextElement Collapse()
        {
            if (ShouldCollapse)
                return null;

            Children.CollapseAll();

            return this;
        }

        [XmlIgnore]
        protected virtual bool ShouldCollapse => !HasChildren;

        [XmlIgnore]
        public TextElement Parent { get; private set; }
    }

    [Serializable]
    public class TextBoldElement : TextElement
    {
        public TextBoldElement() { }

        public TextBoldElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextBolderElement : TextElement
    {
        public TextBolderElement() { }

        public TextBolderElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextLightElement : TextElement
    {
        public TextLightElement() { }

        public TextLightElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextLighterElement : TextElement
    {
        public TextLighterElement() { }

        public TextLighterElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextItalicElement : TextElement
    {
        public TextItalicElement() { }

        public TextItalicElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextObliqueElement : TextElement
    {
        public TextObliqueElement() { }

        public TextObliqueElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextBackSlantedElement : TextElement
    {
        public TextBackSlantedElement() { }

        public TextBackSlantedElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextUnderlineElement : TextElement
    {
        public TextUnderlineElement() { }

        public TextUnderlineElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextStrikeThroughElement : TextElement
    {
        public TextStrikeThroughElement() { }

        public TextStrikeThroughElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextOverlineElement : TextElement
    {
        public TextOverlineElement()
        {
        }

        public TextOverlineElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextSuperscriptElement : TextElement
    {
        public TextSuperscriptElement()
        {
        }

        public TextSuperscriptElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextSubscriptElement : TextElement
    {
        public TextSubscriptElement()
        {
        }

        public TextSubscriptElement(IList<TextElement> children) : base(children)
        {
        }
    }

    [Serializable]
    public class TextFontElement : TextElement
    {
        public TextFontElement()
        {
        }

        public TextFontElement(string family, Measurement height)
        {
            Family = family;
            Height = height;
        }

        public TextFontElement(IList<TextElement> children, string family, Measurement height)
            : base(children)
        {
            Family = family;
            Height = height;
        }

        [XmlAttribute]
        public string Family;
        [XmlAttribute]
        public Measurement Height;
    }

    [Serializable]
    public class TextBlock : TextElement
    {
        public TextBlock()
        {
        }

        public TextBlock(string content)
        {
            Content = content;
        }

        [XmlText]
        public string Content = null;
    }

    [Serializable]
    public class Text : TextElement
    {
        public Text() { }

        public Text(IList<TextElement> children, TextAlignment alignment, TextAlignment verticalAlignment)
            : base(children)
        {
            Alignment = alignment;
            VerticalAlignment = verticalAlignment;
        }

        public Text(string content, TextAlignment alignment, TextAlignment verticalAlignment)
        {
            Children.Add(new TextBlock(content));

            Alignment = alignment;
            VerticalAlignment = verticalAlignment;
        }

        [XmlIgnore]
        protected override bool ShouldCollapse => false;

        [XmlAttribute]
        public TextAlignment Alignment = TextAlignment.Middle;
        [XmlAttribute]
        public TextAlignment VerticalAlignment = TextAlignment.Middle;
    }
}