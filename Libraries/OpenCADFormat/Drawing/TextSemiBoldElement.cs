using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextSemiBoldElement : TextFormatElement
    {
        public TextSemiBoldElement() { }

        public TextSemiBoldElement(IList<object> children) : base(children) { }
    }
}