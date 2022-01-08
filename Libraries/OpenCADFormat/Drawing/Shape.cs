using System;

namespace OpenCAD.OpenCADFormat.Drawing
{
    //Represents a geometric shape.
    [Serializable]
    public abstract class Shape : DrawingNode
    {
        /// <summary>
        /// Gets or sets the stroke properties of this drawing element.
        /// </summary>
        public StrokeAttributes Stroke = StrokeAttributes.Default;

        /// <summary>
        /// Gets or sets the fill properties of this drawing element.
        /// </summary>
        public FillStyle Fill = FillStyles.None;
    }
}