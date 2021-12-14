using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextUnderlineElement : TextFormatElement
    {
        public TextUnderlineElement() { }

        public TextUnderlineElement(IList<object> children) : base(children) { }
    }
}