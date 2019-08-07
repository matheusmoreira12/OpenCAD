using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Measures
{
    public class Quantity
    {
        public Quantity(string name, string symbol, string uISymbol = null)
        {
            Name = name ?? throw new ArgumentNullException("name");
        }

        public string Name { get; private set; }

        public string Symbol { get; private set; }

        public string UISymbol { get; private set; }
    }
}