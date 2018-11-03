using OpenCAD.OpenCADFormat.MetaInformation.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.MetaInformation
{
    [Serializable]
    public class MetadataField
    {
        public MetadataField(string name, string value, bool isRequired = false, bool isReadonly = false)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            IsRequired = isRequired;
            IsReadonly = isReadonly;
        }

        public MetadataField()
        {
            IsInvalid = true;
        }

        [XmlAttribute]
        public string Name;
        [XmlIgnore]
        public string Value;

        [XmlText]
        public XCData ValueCData
        {
            get => new XCData(Value.ToString());
            set => Value = value.ToString();
        }

        #region Flags
        [XmlAttribute]
        public bool IsRequired;
        [XmlAttribute]
        public bool IsReadonly;
        //Validity flag
        [XmlIgnore]
        public bool IsInvalid { get; } = false;
        #endregion

        public bool ShouldSerialize => !IsInvalid;
    }

    public class MetadataFieldCollection : Collection<MetadataField>, IDisposable
    {
        public MetadataFieldCollection(IList<MetadataField> list) : base(list) { }

        public MetadataFieldCollection() { }

        public int FindIndexByName(string name) => Array.FindIndex(this.ToArray(), field => field.Name == name);
        public MetadataField FindByName(string name) => Enumerable.First(this, field => field.Name == name);
        public IEnumerable<MetadataField> FindByValue(string value) => Enumerable.TakeWhile(this,
            field => field.Name == value);

        private bool getIsReadonly(MetadataField field) => field?.IsReadonly ?? false;
        private bool getIsRequired(MetadataField field) => field?.IsRequired ?? false;
        private bool getHasConflict(MetadataField field)
        {
            if (field == null) return false;

            return FindIndexByName(field?.Name) != -1;
        }

        protected override void RemoveItem(int index)
        {
            if (getIsRequired(this[index])) throw new FieldRequiredException();

            base.RemoveItem(index);
        }

        protected override void SetItem(int index, MetadataField item)
        {
            if (getIsReadonly(this[index])) throw new FieldReadOnlyException();

            base.SetItem(index, item);
        }

        protected override void InsertItem(int index, MetadataField field)
        {
            if (getHasConflict(field)) throw new FieldNameConflictException();

            base.InsertItem(index, field ?? throw new FieldNullException(nameof(field)));
        }

        protected override void ClearItems()
        {
            base.ClearItems();
        }

        public void InsertRange(int index, IEnumerable<MetadataField> collection)
        {
            foreach (var field in collection)
            {
                Insert(index, field);

                index++;
            }
        }

        public void AddRange(params MetadataField[] fields)
        {
            foreach (var field in fields) Add(field);
        }

        [XmlIgnore]
        public bool IsDisposed = false;

        void IDisposable.Dispose()
        {
            if (!IsDisposed)
            {
                Clear();
            }

            IsDisposed = true;
        }

        public bool ShouldSerialize => Count > 0;
    }

    [Serializable]
    public class Metadata
    {
        [XmlIgnore]
        private MetadataFieldCollection _fields = null;

        public Metadata(IList<MetadataField> fields)
        {
            Fields = new MetadataFieldCollection(fields);
        }

        public Metadata()
        {
            Fields = new MetadataFieldCollection();
        }

        [XmlElement("Field")]
        public MetadataFieldCollection Fields;

        public MetadataField GetFieldByName(string name) => _fields.FindByName(name);
        public IEnumerable<MetadataField> GetFieldsByValue(string value) => _fields.FindByValue(value);

        public void AddField(string name, string value, bool isRequired = false, bool isReadonly = false) =>
            _fields.Add(new MetadataField(name, value));

        public void AddMultipleFields(params MetadataField[] field) => _fields.AddRange(field);
    }
}