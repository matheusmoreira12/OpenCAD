using System;

namespace OpenCAD.OpenCADFormat.Drawing
{
    //Represents a geometric shape.
    public abstract class Shape : DrawingNode
    {
        protected internal Shape(StrokeAttributes stroke, FillStyle fill)
        {
            Stroke = stroke;
            Fill = fill ?? throw new ArgumentNullException(nameof(fill));
        }

        /// <summary>
        /// Gets the stroke properties of this drawing element.
        /// </summary>
        public readonly StrokeAttributes Stroke;

        /// <summary>
        /// Gets the fill properties of this drawing element.
        /// </summary>
        public readonly FillStyle Fill;
    }
}