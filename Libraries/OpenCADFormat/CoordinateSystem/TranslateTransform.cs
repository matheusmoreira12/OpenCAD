namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public sealed class TranslateTransform : Transform
    {
        public Point Offset { get; }

        public TranslateTransform(Point offset)
        {
            Offset = offset;
        }
    }
}
