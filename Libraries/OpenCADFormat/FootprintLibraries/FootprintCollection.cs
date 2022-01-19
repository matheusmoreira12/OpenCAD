﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.FootprintLibraries
{
    public class FootprintCollection : Collection<Footprint>
    {
        public FootprintCollection()
        {
        }

        public FootprintCollection(IList<Footprint> list) : base(list)
        {
        }
    }
}