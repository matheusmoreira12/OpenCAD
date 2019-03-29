namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public class TranslateTransform : Transform
    {
        public Point Offset { get; private set; }

        public TranslateTransform(Point offset)
        {
            Offset = offset;
        }
    }
}
