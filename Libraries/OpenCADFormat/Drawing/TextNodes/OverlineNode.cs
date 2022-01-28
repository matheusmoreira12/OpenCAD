using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public sealed class OverlineNode : TextNode
    {
        public OverlineNode(IList<TextNode> children) : base(children) { }

        public override TextDecoration Decoration => (Parent?.Decoration ?? TextDecoration.None) | TextDecoration.Overline;
    }
}