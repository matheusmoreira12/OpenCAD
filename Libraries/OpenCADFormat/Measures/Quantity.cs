using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace OpenCAD.OpenCADFormat.Measures
{
    public abstract class Quantity
    {
        public string Name { get; }

        public string Symbol { get; }

        public Unit Unit = null;

        protected Quantity(string name, string symbol)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Symbol = symbol ?? throw new ArgumentNullException(nameof(symbol));
        }
    }
}