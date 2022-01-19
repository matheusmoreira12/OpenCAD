using OpenCAD.OpenCADFormat.DocumentFoundation;
using OpenCAD.OpenCADFormat.MetaAnnotation;
using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.FootprintLibraries
{
    public sealed class FootprintLibrary : Document
    {
        public readonly string Name;

        public readonly FootprintCollection Footprints;

        public FootprintLibrary(
            DateTime created,
            DateTime modified,
            Version sourceVersion,
            Metadata metadata,
            IList<Footprint> footprints) : base(created, modified, sourceVersion, metadata)
        {
            Footprints = new FootprintCollection(footprints ?? throw new ArgumentNullException(nameof(footprints)));
        }
    }
}
