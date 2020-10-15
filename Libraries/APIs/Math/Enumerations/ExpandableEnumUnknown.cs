namespace OpenCAD.APIs.Math.Enumerations
{
    public sealed class ExpandableEnumUnknown<T> : ExpandableEnum<T> where T : ExpandableEnum<T>
    {
        public override string ToString() => Id.ToString();

        internal ExpandableEnumUnknown(int id) : base(id)
        {
        }
    }
}
