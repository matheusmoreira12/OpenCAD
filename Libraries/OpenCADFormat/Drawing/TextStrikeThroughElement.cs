using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextStrikeThroughElement : TextFormatElement
    {
        public TextStrikeThroughElement() { }

        public TextStrikeThroughElement(IList<object> children) : base(children) { }
    }
}