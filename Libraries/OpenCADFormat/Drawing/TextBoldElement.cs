using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextBoldElement : TextFormatElement
    {
        public TextBoldElement() { }

        public TextBoldElement(IList<object> children) : base(children) { }
    }
}