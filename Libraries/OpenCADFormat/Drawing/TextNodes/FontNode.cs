using OpenCAD.APIs.Measures;
using OpenCAD.OpenCADFormat.Drawing.Fonts;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public class FontNode : TextNode
    {
        public FontNode(IList<TextNode> children, string family = null, Scalar? height = null, FontWeight? weight = null, FontStyle? style = null) : base(children)
        {
            Family = family;
            Height = height;
            Weight = weight;
            Style = style;
        }

        public readonly string Family = null;

        public readonly Scalar? Height = null;

        public readonly FontWeight? Weight = null;

        public readonly FontStyle? Style = null;

        public override Font Font => new Font(
            Family ?? Parent?.Font.Family ?? Font.Default.Family,
            Height ?? Parent?.Font.Height ?? Font.Default.Height,
            Weight ?? Parent?.Font.Weight ?? Font.Default.Weight,
            Style ?? Parent?.Font.Style ?? Font.Default.Style
        );
    }
}