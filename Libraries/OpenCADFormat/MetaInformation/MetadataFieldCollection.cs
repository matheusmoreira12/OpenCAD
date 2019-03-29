using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.MetaInformation
{
    public class MetadataFieldCollection : List<MetadataField>
    {
        public int FindIndexByName(string name) => Array.FindIndex(this.ToArray(), field => field.Name == name);
        public MetadataField FindByName(string name) => Enumerable.First(this, field => field.Name == name);
        public IEnumerable<MetadataField> FindByValue(string value) => Enumerable.TakeWhile(this,
            field => field.Name == value);

        public bool ShouldSerialize => Count > 0;
    }
}