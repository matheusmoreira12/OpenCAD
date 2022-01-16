using System;

namespace OpenCAD.OpenCADFormat.MetaAnnotation
{
    public class MetadataField
    {
        public MetadataField(string name, string value, bool acceptsEmptyValue = true, bool isRequired = false, bool isReadonly = false)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            IsRequired = isRequired;
            IsReadonly = isReadonly;
            AcceptsEmptyValue = acceptsEmptyValue;
        }

        public readonly string Name;

        public readonly string Value;

        public readonly bool AcceptsEmptyValue;

        public readonly bool IsRequired;

        public readonly bool IsReadonly;
    }
}