﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.Drawing
{
    /// <summary>
    /// Represents a group of drawing elements.
    /// </summary>
    public class DrawingGroup
    {
        /// <summary>
        /// Gets the drawing elements contained within this drawing group.
        /// </summary>
        public readonly DrawingNode[] Elements;

        public DrawingGroup(IReadOnlyList<DrawingNode> elements)
        {
            Elements = elements?.ToArray() ?? throw new ArgumentNullException(nameof(elements));
        }
    }
}
