using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextSuperscriptElement : TextFormatElement
    {
        public TextSuperscriptElement() { }

        public TextSuperscriptElement(IList<object> children) : base(children) { }
    }
}