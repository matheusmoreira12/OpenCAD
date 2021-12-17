namespace OpenCAD.OpenCADFormat.Drawing.TextNodes
{
    public class TextSpan : TextNode
    {
        public static implicit operator TextSpan(string value) => new TextSpan(value);

        public static explicit operator string(TextSpan value) => value.Content;

        public TextSpan(string content) : base(new TextNode[0])
        {
            Content = content;
        }

        public readonly string Content;
    }
}