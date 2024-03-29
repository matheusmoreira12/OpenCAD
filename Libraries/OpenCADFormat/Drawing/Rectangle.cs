﻿using OpenCAD.APIs.Measures;
using OpenCAD.OpenCADFormat.CoordinateSystem;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public enum RectangleType { TwoPoint, ThreePoint, Centered }

    public sealed class Rectangle : Shape
    {
        public readonly Point? CenterPoint;

        public readonly Point? StartCorner;

        public readonly Point? EndCorner;

        public readonly Scalar? Width;

        public readonly Scalar? Height;

        public readonly Scalar? Area;

        public readonly Scalar? Rotation;

        public Rectangle(
            Point? centerPoint,
            Point? startCorner,
            Point? endCorner,
            Scalar? width,
            Scalar? height,
            Scalar? area,
            Scalar? rotation,
            StrokeAttributes stroke,
            FillStyle fill) : base(
                stroke,
                fill)
        {
            CenterPoint = centerPoint;
            StartCorner = startCorner;
            EndCorner = endCorner;
            Width = width;
            Height = height;
            Area = area;
            Rotation = rotation;
        }
    }
}