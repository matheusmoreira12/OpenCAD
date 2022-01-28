using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public sealed class SuperscriptNode : TextNode
    {
        public SuperscriptNode(IList<TextNode> children) : base(children) { }

        public override TextBaselineShift BaselineShift => TextBaselineShift.Superscript;
    }
}