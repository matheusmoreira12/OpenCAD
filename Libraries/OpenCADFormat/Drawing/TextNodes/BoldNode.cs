using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    [Serializable]
    public sealed class BoldNode : TextNode
    {
        public BoldNode(IList<TextNode> children) : base(children) { }

        public override Font Font => Parent?.Font.ChangeWeight(FontWeight.Bold);
    }
}