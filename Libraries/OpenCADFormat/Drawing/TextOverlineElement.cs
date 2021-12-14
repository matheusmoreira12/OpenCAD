using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextOverlineElement : TextFormatElement
    {
        public TextOverlineElement() { }

        public TextOverlineElement(IList<object> children) : base(children) { }
    }
}