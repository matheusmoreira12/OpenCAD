using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public sealed class StrikeThroughNode : TextNode
    {
        public StrikeThroughNode(IList<TextNode> children) : base(children) { }

        public override TextDecoration Decoration => (Parent?.Decoration ?? TextDecoration.None) | TextDecoration.StrikeThrough;
    }
}