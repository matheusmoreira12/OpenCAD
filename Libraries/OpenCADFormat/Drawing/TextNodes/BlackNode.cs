using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public sealed class BlackNode : TextNode
    {
        public BlackNode(IList<TextNode> children) : base(children) { }

        public override Font Font => Parent?.Font.ChangeWeight(FontWeight.Black);
    }
}