using OpenCAD.OpenCADFormat.DocumentFoundation;
using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.FootprintLibraries
{
    public sealed class FootprintLibrary : Document
    {
        public readonly string Name;

        public readonly List<Footprint> Footprints;

        public FootprintLibrary()
        {
            Name = "*";
            Footprints = new List<Footprint>();
        }

        public FootprintLibrary(string name, IList<Footprint> footprints, Metadata metadata): base(metadata)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Footprints = new List<Footprint>(footprints ?? throw new ArgumentNullException(nameof(footprints)));
        }
    }
}
