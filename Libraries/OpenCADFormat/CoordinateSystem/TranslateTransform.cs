namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public class TranslateTransform : Transform
    {
        public Point Offset { get; }

        public TranslateTransform(Point offset)
        {
            Offset = offset;
        }
    }
}
