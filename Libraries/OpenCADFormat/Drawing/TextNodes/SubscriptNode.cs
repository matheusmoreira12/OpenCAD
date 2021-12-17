using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public class SubscriptNode : TextNode
    {
        public SubscriptNode(IList<TextNode> children) : base(children) { }

        public override TextBaselineShift BaselineShift => TextBaselineShift.Subscript;
    }
}