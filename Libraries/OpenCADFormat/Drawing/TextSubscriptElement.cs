using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextSubscriptElement : TextFormatElement
    {
        public TextSubscriptElement() { }

        public TextSubscriptElement(IList<object> children) : base(children) { }
    }
}