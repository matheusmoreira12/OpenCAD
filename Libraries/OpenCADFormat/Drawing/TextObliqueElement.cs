using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    [Serializable]
    public class TextObliqueElement : TextFormatElement
    {
        public TextObliqueElement() { }

        public TextObliqueElement(IList<object> children) : base(children) { }
    }
}