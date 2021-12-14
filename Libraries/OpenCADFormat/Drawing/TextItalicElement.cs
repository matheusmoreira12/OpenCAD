using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextItalicElement : TextFormatElement
    {
        public TextItalicElement() { }

        public TextItalicElement(IList<object> children) : base(children) { }
    }
}