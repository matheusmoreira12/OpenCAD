using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Libraries
{
    [Serializable]
    public class Parameter
    {
        [XmlAttribute]
        public string Key = "";
        [XmlAttribute]
        public string Value = "";
        [XmlAttribute]
        public bool IsVisible = true;
        [XmlAttribute]
        public bool IsRequired = false;
        [XmlAttribute]
        public bool IsReadOnly = false;
    }

    public class ParameterList : List<Parameter>
    {
        public Parameter GetByKey(string key)
        {
            try
            {
                return this.First(p => p.Key == key);
            }
            catch
            {
                return null;
            }
        }

        public new void Add(Parameter parameter)
        {
            if (GetByKey(parameter.Key) != null)
                throw new Exceptions.ConflictingParameterKeyException(parameter.Key);

            base.Add(parameter);
        }
    }

    namespace Exceptions
    {
        public class ConflictingParameterKeyException : LibraryException
        {
            const string DEFAULT_MESSAGE = "The specified parameter key is already in use.";

            public ConflictingParameterKeyException(string key, string message) : base(message)
            {
                Data["Key"] = key;
            }
            public ConflictingParameterKeyException(string key) : this(key, DEFAULT_MESSAGE)
            {
            }
        }
    }
}