using OpenCAD.OpenCADFormat.DataConversion;
using OpenCAD.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OpenCAD.OpenCADFormat.DataStrings
{
    public abstract class DataStringItem
    {
        #region String Reading/Writing
        public static DataStringItem Parse(string content)
        {
            StringScanner scanner = new StringScanner(content);

            if (ReadFromString(scanner, out DataStringItem result))
                return result;

            return null;
        }

        internal static bool ReadFromString(StringScanner scanner, out DataStringItem item)
        {
            return DataStringFunction.ReadFromString(scanner, out item) ||
                DataStringSymbol.ReadFromString(scanner, out item) ||
                DataStringLiteral.ReadFromString(scanner, out item);
        }

        internal abstract void WriteToString(StringWriter writer);
        #endregion

        public DataStringItemCollection Items { get; private set; }
        public DataStringItem Parent { get; private set; }

        internal void DataStringItemAdded(DataStringItem parent)
        {
            Parent = parent;
        }

        public DataStringItem()
        {
            Items = new DataStringItemCollection(this);
        }
        public DataStringItem(IEnumerable<DataStringItem> items)
        {
            Items = new DataStringItemCollection(items, this);
        }
    }

    public class DataStringMainContext : DataStringItem
    {
        #region String Reading/Writing
        public static new DataStringMainContext Parse(string content)
        {
            StringScanner scanner = new StringScanner(content);

            return ReadFromString(scanner);
        }

        internal static DataStringMainContext ReadFromString(StringScanner scanner)
        {
            DataStringItem[] items = DataStringItemCollection.ReadFromString(scanner).ToArray();

            return new DataStringMainContext(items);
        }

        public override string ToString()
        {
            StringWriter writer = new StringWriter("");
            WriteToString(writer);

            return writer.Content;
        }

        internal override void WriteToString(StringWriter writer)
        {
            Items.WriteToString(writer);
        }
        #endregion

        public DataStringMainContext() : base() { }

        public DataStringMainContext(IEnumerable<DataStringItem> items) : base(items) { }
    }

    public abstract class DataStringLiteral : DataStringItem
    {
        #region String Reading/Writing
        internal static new bool ReadFromString(StringScanner scanner, out DataStringItem item)
        {
            return DataStringLiteralString.ReadFromString(scanner, out item) ||
                DataStringLiteralBinary.ReadFromString(scanner, out item) ||
                DataStringLiteralNumber.ReadFromString(scanner, out item);
        }
        #endregion
    }

    public class DataStringLiteralString : DataStringLiteral
    {
        #region String Reading/Writing
        internal static new bool ReadFromString(StringScanner scanner, out DataStringItem item)
        {
            using (var token = scanner.SaveIndex())
            {
                if (scanner.CurrentChar == GlobalConsts.STRING_ENCLOSING_CHAR)
                {
                    while (scanner.CurrentChar != GlobalConsts.STRING_ENCLOSING_CHAR)
                        scanner.Increment();

                    item = new DataStringLiteralString(scanner.GetString(token));
                }

                scanner.RestoreIndex(token);
                item = null;
                return false;
            }
        }

        internal override void WriteToString(StringWriter writer)
        {
            writer.Content += Utils.EncloseString(Value);
        }
        #endregion

        public string Value { get; private set; }

        public DataStringLiteralString(string value) : base()
        {
            Value = value;
        }
    }

    public enum DataStringLiteralBinaryRepresentation
    {
        Binary,
        Octal,
        Hexadecimal
    }

    public class DataStringLiteralBinary : DataStringItem
    {
        #region String Reading/Writing
        internal static new bool ReadFromString(StringScanner scanner, out DataStringItem item)
        {
            using (var token = scanner.SaveIndex())
            {
                if (scanner.CurrentChar == '0')
                {
                    scanner.Increment();

                    bool literalIsBinary = scanner.CurrentChar == 'b',
                        literalIsHexadecimal = scanner.CurrentChar == 'x';

                    if (literalIsBinary || literalIsHexadecimal)
                        scanner.Increment();

                    using (var repStrToken = scanner.SaveIndex())
                    {

                        while (literalIsBinary && GlobalConsts.BINARY_CHARSET.Contains(scanner.CurrentChar) ||
                            literalIsHexadecimal && GlobalConsts.HEXADECIMAL_CHARSET.Contains(scanner.CurrentChar) ||
                            !(literalIsHexadecimal || literalIsHexadecimal) && GlobalConsts.OCTAL_CHARSET.Contains(scanner.CurrentChar))
                            scanner.Increment();

                        string repStr = scanner.GetString(repStrToken);

                        if (repStr.Length > 0)
                        {
                            if (literalIsBinary)
                                item = new DataStringLiteralBinary(BinaryConversion.FromRepresentation(repStr, 2),
                                    DataStringLiteralBinaryRepresentation.Binary);
                            else if (literalIsHexadecimal)
                                item = new DataStringLiteralBinary(BinaryConversion.FromRepresentation(repStr, 16),
                                    DataStringLiteralBinaryRepresentation.Hexadecimal);
                            else
                                item = new DataStringLiteralBinary(BinaryConversion.FromRepresentation(repStr, 8),
                                    DataStringLiteralBinaryRepresentation.Octal);

                            return true;
                        }
                    }
                }

                scanner.RestoreIndex(token);
                item = null;
                return false;
            }
        }

        #region String Writing
        internal override void WriteToString(StringWriter writer)
        {
            int fromBase = OriginalRepresentation == DataStringLiteralBinaryRepresentation.Binary ? 2 :
                OriginalRepresentation == DataStringLiteralBinaryRepresentation.Octal ? 4 :
                OriginalRepresentation == DataStringLiteralBinaryRepresentation.Hexadecimal ? 16 : 0;

            writer.Content += $"{BinaryConversion.ToRepresentation(Value, fromBase)}";
        }
        #endregion
        #endregion

        public BitArray Value { get; private set; }

        public DataStringLiteralBinaryRepresentation OriginalRepresentation { get; private set; }

        public DataStringLiteralBinary(BitArray value, DataStringLiteralBinaryRepresentation originalRepresentation) : base()
        {
            Value = value;
            OriginalRepresentation = originalRepresentation;
        }
    }

    public abstract class DataStringLiteralNumber : DataStringLiteral
    {
        #region String Reading
        internal static new bool ReadFromString(StringScanner scanner, out DataStringItem item)
        {

            if (StringUtils.ReadDecimalString(scanner, out string decimalStr, out bool isFloatingPoint, out bool hasExponent))
            {
                if (isFloatingPoint)
                    item = new DataStringLiteralFloatingPoint(double.Parse(decimalStr, Conventions.STANDARD_CULTURE));
                else
                    item = new DataStringLiteralInteger(int.Parse(decimalStr, Conventions.STANDARD_CULTURE));

                return true;
            }
            else
            {
                item = null;
                return false;
            }
        }
        #endregion
    }

    public class DataStringLiteralFloatingPoint : DataStringLiteralNumber
    {
        #region String Writing
        internal override void WriteToString(StringWriter writer)
        {
            writer.Content += Value.ToString(Conventions.STANDARD_CULTURE);
        }
        #endregion

        public double Value { get; private set; }

        public DataStringLiteralFloatingPoint(double value) : base()
        {
            Value = value;
        }
    }

    public class DataStringLiteralInteger : DataStringLiteralNumber
    {
        #region String Writing
        internal override void WriteToString(StringWriter writer)
        {
            writer.Content += Value;
        }
        #endregion

        public int Value { get; private set; }

        public DataStringLiteralInteger(int value) : base()
        {
            Value = value;
        }
    }

    public class DataStringParameterSet : DataStringItem
    {
        #region String Reading/Writing
        internal static new bool ReadFromString(StringScanner scanner, out DataStringItem item)
        {
            using (var token = scanner.SaveIndex())
            {
                if (scanner.CurrentChar == GlobalConsts.LOOSE_DATASET_OPENING_CHAR)
                {
                    scanner.Increment();

                    item = new DataStringParameterSet();
                    item.Items.AddRange(DataStringItemCollection.ReadFromString(scanner).ToList());

                    if (scanner.CurrentChar == GlobalConsts.LOOSE_DATASET_CLOSING_CHAR)
                    {
                        scanner.Increment();
                        return true;
                    }
                }

                scanner.RestoreIndex(token);
                item = null;
                return false;
            }
        }

        public override string ToString()
        {
            StringWriter writer = new StringWriter("");
            WriteToString(writer);

            return writer.Content;
        }

        internal override void WriteToString(StringWriter writer)
        {
            writer.Content += GlobalConsts.LOOSE_DATASET_OPENING_CHAR;

            Items.WriteToString(writer);

            writer.Content += GlobalConsts.LOOSE_DATASET_CLOSING_CHAR;
        }
        #endregion

        public DataStringParameterSet() : base() { }
        public DataStringParameterSet(IEnumerable<DataStringItem> items) : base(items) { }
    }

    public class DataStringFunction : DataStringItem
    {
        #region String Reading/Writing
        internal static new bool ReadFromString(StringScanner scanner, out DataStringItem item)
        {
            using (var token = scanner.SaveIndex())
            {
                if (Utils.ReadIdentifier(scanner, out string functionName))
                {
                    if (scanner.CurrentChar == GlobalConsts.FUNC_PARAMS_OPENING_CHAR)
                    {
                        scanner.Increment();

                        item = new DataStringFunction(functionName);
                        item.Items.AddRange(DataStringItemCollection.ReadFromString(scanner).ToList());

                        if (scanner.CurrentChar == GlobalConsts.FUNC_PARAMS_CLOSING_CHAR)
                        {
                            scanner.Increment();
                            return true;
                        }
                    }
                }


                scanner.RestoreIndex(token);
                item = null;
                return false;
            }
        }

        public override string ToString()
        {
            StringWriter writer = new StringWriter("");
            WriteToString(writer);

            return writer.Content;
        }

        internal override void WriteToString(StringWriter writer)
        {
            writer.Content += Name + GlobalConsts.FUNC_PARAMS_OPENING_CHAR;

            Items.WriteToString(writer);

            writer.Content += GlobalConsts.FUNC_PARAMS_CLOSING_CHAR;
        }
        #endregion

        public string Name { get; private set; }

        public DataStringFunction(string name)
        {
            Name = name;
        }
        public DataStringFunction(string name, IEnumerable<DataStringItem> parameters) : base(parameters)
        {
            Name = name;
        }
    }

    public class DataStringSymbol : DataStringItem
    {
        #region String Reading/Writing
        internal static new bool ReadFromString(StringScanner scanner, out DataStringItem item)
        {
            if (Utils.ReadIdentifier(scanner, out string symbolName))
            {
                item = new DataStringSymbol(symbolName);
                return true;
            }

            item = null;
            return false;
        }

        internal override void WriteToString(StringWriter writer)
        {
            writer.Content += Name;
        }
        #endregion

        public string Name { get; private set; }

        public DataStringSymbol(string name)
        {

        }
    }

    public class DataStringItemCollection : List<DataStringItem>
    {
        #region String Reading/Writing
        internal static IEnumerable<DataStringItem> ReadFromString(StringScanner scanner)
        {
            DataStringItem generatedItem = null;

            while (DataStringItem.ReadFromString(scanner, out generatedItem) || Utils.ReadDataStringSeparator(scanner) ||
                Utils.ReadDataStringWhitespace(scanner))
            {
                if (generatedItem != null)
                    yield return generatedItem;

                generatedItem = null;
            }
        }

        internal void WriteToString(StringWriter writer)
        {
            for (int i = 0, c = Count; i < c; i++)
            {
                DataStringItem item = this[i];
                item.WriteToString(writer);

                if (i < c - 1)
                    writer.Content += GlobalConsts.SEPARATOR_CHARACTER + " ";
            }
        }
        #endregion

        public DataStringItem Parent { get; private set; }

        public new void Add(DataStringItem item)
        {
            item.DataStringItemAdded(Parent);
            base.Add(item);
        }

        public new void AddRange(IEnumerable<DataStringItem> collection)
        {
            foreach (var item in collection)
                item.DataStringItemAdded(Parent);

            base.AddRange(collection);
        }

        public DataStringItemCollection(DataStringItem parent)
        {
            Parent = parent;
        }

        public DataStringItemCollection(IEnumerable<DataStringItem> original, DataStringItem parent)
        {
            Parent = parent;
            AddRange(original);
        }
    }
}