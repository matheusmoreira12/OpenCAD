using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCAD.OpenCADFormat.Drawing;
using System.Xml;
using System.Xml.Serialization;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Text text = new Text("Hello, world!", TextAlignment.Middle, TextAlignment.Middle);

            XmlSerializer serializer = new XmlSerializer(typeof(Text));
            XmlWriter writer = XmlWriter.Create(System.IO.Path.GetTempFileName());

            serializer.Serialize(writer, text);
        }
    }
}
