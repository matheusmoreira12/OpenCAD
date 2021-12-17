using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public class UnderlineNode : TextNode
    {
        public UnderlineNode(IList<TextNode> children) : base(children) { }

        public override TextDecoration Decoration => (Parent?.Decoration ?? TextDecoration.None) | TextDecoration.Underline;
    }
}