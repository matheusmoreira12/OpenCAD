using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextBlackElement : TextFormatElement
    {
        public TextBlackElement() { }

        public TextBlackElement(IList<object> children) : base(children) { }
    }
}