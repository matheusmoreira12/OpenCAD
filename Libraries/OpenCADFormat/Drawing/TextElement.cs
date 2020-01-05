using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using OpenCAD.APIs.Measures;

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

    [Serializable]
    public class TextFormatElement : TextElement
    {
        public TextFormatElement() { }

        public TextFormatElement(IList<object> children) : base(children) { }

        public TextSemiBoldElement GetSemiBold() => new TextSemiBoldElement(Children);
        public TextBoldElement GetBold() => new TextBoldElement(Children);
        public TextBlackElement GetBlack() => new TextBlackElement(Children);
        public TextLightElement GetLight() => new TextLightElement(Children);
        public TextItalicElement GetItalic() => new TextItalicElement(Children);
        public TextObliqueElement GetOblique() => new TextObliqueElement(Children);
        public TextUnderlineElement GetUnderline() => new TextUnderlineElement(Children);
        public TextOverlineElement GetOverline() => new TextOverlineElement(Children);
        public TextSuperscriptElement GetSuperscript() => new TextSuperscriptElement(Children);
        public TextSubscriptElement GetSubscript() => new TextSubscriptElement(Children);

        public TextFontElement ChangeFont(string family, Scalar height) => new TextFontElement(Children)
        {
            Family = family,
            Height = height
        };
    }

    [Serializable]
    public class TextLightElement : TextFormatElement
    {
        public TextLightElement() { }

        public TextLightElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextSemiBoldElement : TextFormatElement
    {
        public TextSemiBoldElement() { }

        public TextSemiBoldElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextBoldElement : TextFormatElement
    {
        public TextBoldElement() { }

        public TextBoldElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextBlackElement : TextFormatElement
    {
        public TextBlackElement() { }

        public TextBlackElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextItalicElement : TextFormatElement
    {
        public TextItalicElement() { }

        public TextItalicElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextObliqueElement : TextFormatElement
    {
        public TextObliqueElement() { }

        public TextObliqueElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextUnderlineElement : TextFormatElement
    {
        public TextUnderlineElement() { }

        public TextUnderlineElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextStrikeThroughElement : TextFormatElement
    {
        public TextStrikeThroughElement() { }

        public TextStrikeThroughElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextOverlineElement : TextFormatElement
    {
        public TextOverlineElement() { }

        public TextOverlineElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextSuperscriptElement : TextFormatElement
    {
        public TextSuperscriptElement() { }

        public TextSuperscriptElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextSubscriptElement : TextFormatElement
    {
        public TextSubscriptElement() { }

        public TextSubscriptElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextFontElement : TextFormatElement
    {
        public TextFontElement() { }

        public TextFontElement(IList<object> children) : base(children) { }

        [XmlAttribute]
        public string Family = null;

        [XmlAttribute]
        public Scalar? Height = null;
    }

    [Serializable]
    public class Text : TextElement
    {
        public static Text FromContent(string content) => new Text(new[] { content });

        public Text() { }

        public Text(IList<object> children) : base(children) { }

        [XmlIgnore]
        protected override bool ShouldCollapse => false;

        [XmlAttribute]
        public TextAlignment? Alignment = null;

        [XmlAttribute]
        public TextAlignment? VerticalAlignment = null;
    }
}