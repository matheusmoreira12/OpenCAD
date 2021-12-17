using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public class ObliqueNode : TextNode
    {
        public ObliqueNode(IList<TextNode> children) : base(children) { }

        public override Font Font => Parent?.Font.ChangeStyle(FontStyle.Oblique);
    }
}