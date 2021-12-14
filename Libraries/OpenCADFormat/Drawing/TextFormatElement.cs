using System;
using System.Collections.Generic;
using OpenCAD.APIs.Measures;

namespace OpenCAD.OpenCADFormat.Drawing
{
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
}