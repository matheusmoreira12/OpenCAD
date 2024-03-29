﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    /// <summary>
    /// Represents a group of drawing elements.
    /// </summary>
    public sealed class DrawingGroup
    {
        /// <summary>
        /// Gets the drawing elements contained within this drawing group.
        /// </summary>
        public readonly DrawingNode[] Elements;

        public DrawingGroup(params DrawingNode[] elements)
        {
            Elements = elements;
        }

        public DrawingGroup(IList<DrawingNode> elements)
        {
            Elements = elements?.ToArray() ?? throw new ArgumentNullException(nameof(elements));
        }
    }
}
