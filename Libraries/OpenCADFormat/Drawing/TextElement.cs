using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

using OpenCAD.OpenCADFormat.Measures;

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
        [XmlElement("b", typeof(TextBoldElement))]
        [XmlElement("Black", typeof(TextBlackElement))]
        [XmlElement("l", typeof(TextLightElement))]
        [XmlElement("i", typeof(TextItalicElement))]
        [XmlElement("Oblique", typeof(TextObliqueElement))]
        [XmlElement("u", typeof(TextUnderlineElement))]
        [XmlElement("s", typeof(TextStrikeThroughElement))]
        [XmlElement("Overline", typeof(TextOverlineElement))]
        [XmlElement("Emphasis", typeof(TextEmphasisElement))]
        [XmlElement("sub", typeof(TextSubscriptElement))]
        [XmlElement("sup", typeof(TextSuperscriptElement))]
        [XmlElement("font", typeof(TextFontElement))]
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
    public class TextFormat : TextElement
    {
        public TextFormat() { }

        public TextFormat(IList<object> children) : base(children) { }

        public TextSemiBoldElement GetSemiBold() => new TextSemiBoldElement(Children);
        public TextBoldElement GetBold() => new TextBoldElement(Children);
        public TextBlackElement GetBlack() => new TextBlackElement(Children);
        public TextLightElement GetLight() => new TextLightElement(Children);
        public TextItalicElement GetItalic() => new TextItalicElement(Children);
        public TextObliqueElement GetOblique() => new TextObliqueElement(Children);
        public TextUnderlineElement GetUnderline() => new TextUnderlineElement(Children);
        public TextOverlineElement GetOverline() => new TextOverlineElement(Children);
        public TextEmphasisElement GetEmphasis() => new TextEmphasisElement(Children);
        public TextSuperscriptElement GetSuperscript() => new TextSuperscriptElement(Children);
        public TextSubscriptElement GetSubscript() => new TextSubscriptElement(Children);

        public TextFontElement ChangeFont(string family, Measurement height) => new TextFontElement(Children)
        {
            Family = family,
            Height = height
        };
    }

    [Serializable]
    public class TextLightElement : TextFormat
    {
        public TextLightElement() { }

        public TextLightElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextSemiBoldElement : TextFormat
    {
        public TextSemiBoldElement() { }

        public TextSemiBoldElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextBoldElement : TextFormat
    {
        public TextBoldElement() { }

        public TextBoldElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextBlackElement : TextFormat
    {
        public TextBlackElement() { }

        public TextBlackElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextItalicElement : TextFormat
    {
        public TextItalicElement() { }

        public TextItalicElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextObliqueElement : TextFormat
    {
        public TextObliqueElement() { }

        public TextObliqueElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextUnderlineElement : TextFormat
    {
        public TextUnderlineElement() { }

        public TextUnderlineElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextStrikeThroughElement : TextFormat
    {
        public TextStrikeThroughElement() { }

        public TextStrikeThroughElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextOverlineElement : TextFormat
    {
        public TextOverlineElement() { }

        public TextOverlineElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextEmphasisElement : TextFormat
    {
        public TextEmphasisElement() { }

        public TextEmphasisElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextSuperscriptElement : TextFormat
    {
        public TextSuperscriptElement() { }

        public TextSuperscriptElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextSubscriptElement : TextFormat
    {
        public TextSubscriptElement() { }

        public TextSubscriptElement(IList<object> children) : base(children) { }
    }

    [Serializable]
    public class TextFontElement : TextFormat
    {
        public TextFontElement() { }

        public TextFontElement(IList<object> children) : base(children) { }

        [XmlAttribute]
        public string Family = null;

        public bool ShouldSerializeFamily => Family == default(string);

        [XmlAttribute]
        public Measurement Height;

        public bool ShouldSerializeHeight => Height == default(Measurement);
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
        public TextAlignment Alignment = default(TextAlignment);

        public bool ShouldSerializeAlignment() => Alignment != default(TextAlignment);

        [XmlAttribute]
        public TextAlignment VerticalAlignment = default(TextAlignment);

        public bool ShouldSerializeVerticalAlignment() => Alignment != default(TextAlignment);
    }
}