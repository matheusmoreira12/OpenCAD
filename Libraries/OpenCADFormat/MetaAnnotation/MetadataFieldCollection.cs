using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenCAD.OpenCADFormat.MetaAnnotation
{
    public sealed class MetadataFieldCollection : Collection<MetadataField>
    {
        private static readonly Exception FIELD_REQUIRED_EXCEPTION
            = new InvalidOperationException("Cannot remove metadata field. Field is required.");

        private static readonly Exception FIELD_READONLY_EXCEPTION
            = new InvalidOperationException("Cannot remove metadata field. Field is readonly.");

        public MetadataFieldCollection()
        {
        }

        public MetadataFieldCollection(IList<MetadataField> list) : base(list)
        {
        }

        private void ValidateClearItems()
        {
            foreach (var item in this)
            {
                if (item.IsRequired)
                    throw FIELD_REQUIRED_EXCEPTION;
                if (item.IsReadonly)
                    throw FIELD_READONLY_EXCEPTION;
            }
        }

        private void ValidateRemoveItem(int index)
        {
            if (index > Count - 1)
                return;
            if (this[index].IsRequired)
                throw FIELD_REQUIRED_EXCEPTION;
            if (this[index].IsReadonly)
                throw FIELD_READONLY_EXCEPTION;
        }

        private void ValidateSetItem(int index)
        {
            if (index <= Count - 1 && this[index].IsReadonly)
                throw new InvalidOperationException();
        }

        protected override void ClearItems()
        {
            ValidateClearItems();
            base.ClearItems();
        }

        protected override void RemoveItem(int index)
        {
            ValidateRemoveItem(index);
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, MetadataField item)
        {
            ValidateSetItem(index);
            base.SetItem(index, item);
        }
    }
}