using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class Text : TextNode
    {
        public Text(IList<TextNode> children) : base(children) { }

        public override TextNode Collapse() => this;
    }
}