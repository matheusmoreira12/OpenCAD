using System;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class MetricPrefix
    {
        public double Multiplier { get; private set; }

        public string Name { get; private set; }

        public string Symbol { get; private set; }

        public string UISymbol { get; private set; }

        public MetricPrefix(string name, double multiplier, string symbol, string uiSymbol = null)
        {
            Name = name;
            Multiplier = multiplier;
            Symbol = symbol;
            UISymbol = uiSymbol ?? symbol;
        }

        public bool Disposed { get; private set; } = false;
    }
}