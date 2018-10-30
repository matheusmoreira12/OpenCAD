using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat
{
    public class MetadataField : IXmlSerializable
    {
        public MetadataField(string name, string value, bool isRequired = false, bool isReadonly = false)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Value = value ?? throw new ArgumentNullException(nameof(value));
            IsRequired = isRequired;
            IsReadonly = isReadonly;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }

        #region Flags
        public bool IsRequired { get; private set; }
        public bool IsReadonly { get; private set; }
        #endregion

        XmlSchema IXmlSerializable.GetSchema() => null;

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.NodeType != XmlNodeType.Element)
                throw new InvalidOperationException("Cannot serialize metadata field. A XML element was expected.");

            reader.MoveToFirstAttribute();

            if (reader.MoveToAttribute(nameof(Name))) Name = reader.ReadContentAsString();
            if (reader.MoveToAttribute(nameof(IsReadonly))) IsReadonly = reader.ReadContentAsBoolean();
            if (reader.MoveToAttribute(nameof(IsRequired))) IsRequired = reader.ReadContentAsBoolean();

            reader.MoveToContent();

            if (reader.NodeType == XmlNodeType.CDATA)
                Value = reader.ReadContentAsString();
        }

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString(nameof(Name), Name);
            writer.WriteAttributeString(nameof(IsRequired), IsRequired.ToString());
            writer.WriteAttributeString(nameof(IsReadonly), IsReadonly.ToString());

            writer.WriteCData(Value);
        }
    }

    public class MetadataFieldCollection : Collection<MetadataField>
    {
        public int FindIndexByName(string name) => Array.FindIndex(this.ToArray(), field => field.Name == name);
        public MetadataField FindByName(string name) => Enumerable.First(this, field => field.Name == name);
        public IEnumerable<MetadataField> FindByValue(string name) => Enumerable.TakeWhile(this,
            field => field.Name == name);

        private void checkReadonly(MetadataField field)
        {
            if (field.IsReadonly) throw new InvalidOperationException($"Cannot change field {field.Name}." +
                $" Field is read-only.");
        }

        private void checkRequired(MetadataField field)
        {
            if (field.IsRequired) throw new InvalidOperationException($"Cannot remove field {field.Name}." +
                $" Field is required.");
        }

        private void checkConflict(MetadataField field)
        {
            bool nameIsConficting = FindIndexByName(field.Name) != -1;

            if (nameIsConficting) throw new InvalidOperationException($"Cannot add field {field.Name}. " +
                $" A metadata field with the specified name already exists.");
        }

        protected override void RemoveItem(int index)
        {
            checkRequired(this[index]);

            base.RemoveItem(index);
        }

        protected override void SetItem(int index, MetadataField item)
        {
            checkReadonly(this[index]);

            base.SetItem(index, item);
        }

        protected override void InsertItem(int index, MetadataField item)
        {
            checkConflict(this[index]);

            base.InsertItem(index, item);
        }

        protected override void ClearItems()
        {
            foreach (var field in this)
                checkRequired(field);

            base.ClearItems();
        }
    }

    [Serializable]
    public class Metadata
    {
        [XmlIgnore]
        private List<MetadataField> fields;

        [XmlElement("Field")]
        public IList<MetadataField> Fields
        {
            get => fields;
            set
            {
                Fields.Clear();

                fields.AddRange(value);
            }
        }

        public int FindIndex(string name) => fields.FindIndex(field => field.Name == name);
        public MetadataField FindByName(string name) => fields.Find(field => field.Name == name);
        public List<MetadataField> FindByValue(string name) => fields.FindAll(field => field.Name == name);

        public void Add(string name, string value, bool isRequired = false, bool isReadonly = false) =>
            Fields.Add(new MetadataField(name, value));
    }
}
