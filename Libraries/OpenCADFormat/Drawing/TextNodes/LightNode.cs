using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public class LightNode : TextNode
    {
        public LightNode(IList<TextNode> children) : base(children) { }

        public override Font Font => Parent?.Font.ChangeWeight(FontWeight.Light);
    }
}