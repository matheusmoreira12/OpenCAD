using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public class ItalicNode : TextNode
    {
        public ItalicNode(IList<TextNode> children) : base(children) { }

        public override Font Font => Parent?.Font.ChangeStyle(FontStyle.Italic);
    }
}