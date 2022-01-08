using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public class SemiBoldNode : TextNode
    {
        public SemiBoldNode(IList<TextNode> children) : base(children) { }

        public override Font Font => Parent?.Font.ChangeWeight(FontWeight.SemiBold);
    }
}