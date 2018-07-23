using OpenCAD.OpenCADFormat.Measures;

namespace OpenCAD.OpenCADFormat.CoordinateSystem
{
    public struct Point
    {
        public IMeasurement<Measures.Quantities.Length> X { get; private set; }
        public IMeasurement<Measures.Quantities.Length> Y { get; private set; }

        public Point(IMeasurement<Measures.Quantities.Length> x, IMeasurement<Measures.Quantities.Length> y)
        {
            X = x;
            Y = y;
        }
    }

    public struct Size
    {
        public IMeasurement<Measures.Quantities.Length> Width { get; private set; }
        public IMeasurement<Measures.Quantities.Length> Height { get; private set; }

        public Size(IMeasurement<Measures.Quantities.Length> width, IMeasurement<Measures.Quantities.Length> height)
        {
            Width = width;
            Height = height;
        }
    }

    public abstract class Transform
    {

    }

    public class TranslateTransform
    {
        public Point Offset { get; private set; }

        public TranslateTransform(Point offset)
        {
            Offset = offset;
        }
    }

    public class RotateTransform
    {
        public Point Center { get; private set; }
        public IMeasurement<Measures.Quantities.PlaneAngle> Angle { get; private set; }

        public RotateTransform(Point center, IMeasurement<Measures.Quantities.PlaneAngle> angle)
        {
            Center = center;
            Angle = angle;
        }
    }
}
