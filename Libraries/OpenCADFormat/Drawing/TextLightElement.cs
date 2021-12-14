using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextLightElement : TextFormatElement
    {
        public TextLightElement() { }

        public TextLightElement(IList<object> children) : base(children) { }
    }
}