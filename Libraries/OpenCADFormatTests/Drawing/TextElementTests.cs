using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.OpenCADFormat.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace OpenCAD.OpenCADFormat.Drawing.Tests
{
    [TestClass()]
    public class TextElementTests
    {
        [TestMethod()]
        public void TextElementTest()
        {
            Text text = Text.FromContent("Hello, world!");

            XmlSerializer serializer = new XmlSerializer(typeof(Text));
            StringWriter writer = new StringWriter();

            serializer.Serialize(writer, text);

            string actual = writer.ToString();

            string expected = "<Text>Hello, World</Text>";
            
            Assert.AreEqual(expected, actual);
        }
    }
}