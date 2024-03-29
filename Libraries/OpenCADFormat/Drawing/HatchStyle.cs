﻿using System;

namespace OpenCAD.OpenCADFormat.Drawing
{
    public sealed class HatchStyle
    {
        public HatchStyle(StrokeStyle[] strokeStyles)
        {
            StrokeStyles = strokeStyles ?? throw new ArgumentNullException(nameof(strokeStyles));
        }

        public StrokeStyle[] StrokeStyles { get; private set; }
    }
}