using OpenCAD.APIs.Measures;
using OpenCAD.OpenCADFormat.Drawing.Fonts;
using OpenCAD.OpenCADFormat.Drawing.TextNodes;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public abstract class TextNode : DrawingNode
    {
        internal protected TextNode(IList<TextNode> children)
        {
            Children = children.ToArray();
        }

        protected void SetParent(TextNode parent) => Parent = parent;

        protected void UnsetParent() => Parent = null;

        public SemiBoldNode GetSemiBold() => new SemiBoldNode(new TextNode[] { this });
        public BoldNode GetBold() => new BoldNode(new TextNode[] { this });
        public BlackNode GetBlack() => new BlackNode(new TextNode[] { this });
        public LightNode GetLight() => new LightNode(new TextNode[] { this });
        public ItalicNode GetItalic() => new ItalicNode(new TextNode[] { this });
        public ObliqueNode GetOblique() => new ObliqueNode(new TextNode[] { this });
        public UnderlineNode GetUnderline() => new UnderlineNode(new TextNode[] { this });
        public StrikeThroughNode GetStrikeThrough() => new StrikeThroughNode(new TextNode[] { this });
        public OverlineNode GetOverline() => new OverlineNode(new TextNode[] { this });
        public SuperscriptNode GetSuperscript() => new SuperscriptNode(new TextNode[] { this });
        public SubscriptNode GetSubscript() => new SubscriptNode(new TextNode[] { this });

        public FontNode ChangeFont(string family = null, Scalar? height = null, FontWeight? weight = null, FontStyle? style = null) => new FontNode(new TextNode[] { this }, family, height, weight, style);


        public virtual TextNode Collapse()
        {
            var children = CollapseChildren();
            if (children.Length == 0)
                return null;
            if (children.Length == 1)
            {
                var child = children[0];
            }
            return this;
        }

        public readonly TextNode[] Children;

        public bool HasChildren => Children.Length > 0;

        public TextNode Parent { get; private set; }

        public virtual TextNode[] CollapseChildren() => Children.AsParallel().Select(c => c.Collapse()).Where(c => c != null).ToArray();

        public virtual TextAlignment LineAlignment => Parent?.LineAlignment ?? TextAlignment.Leading;

        public virtual TextAlignment VerticalAlignment => Parent?.VerticalAlignment ?? TextAlignment.Leading;

        public virtual TextBaselineShift BaselineShift => Parent?.BaselineShift ?? TextBaselineShift.Baseline;

        public virtual TextDecoration Decoration => Parent?.Decoration ?? TextDecoration.None;

        public virtual Font Font => Parent?.Font ?? Font.Default;
    }
}