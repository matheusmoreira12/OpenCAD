using System;
using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class MetricPrefix: IDisposable
    {
        public double Multiplier { get; private set; }
        public string Symbol { get; private set; }
        public string UISymbol { get; private set; }

        public MetricPrefix(double multiplier, string symbol, string uiSymbol = null)
        {
            Multiplier = multiplier;
            Symbol = symbol;
            UISymbol = uiSymbol ?? symbol;

            MetricPrefixes.AddPrefix(this);
        }

        public bool Disposed { get; private set; } = false;

        private void dispose()
        {
            MetricPrefixes.RemovePrefix(this);
        }

        void IDisposable.Dispose()
        {
            if (Disposed == false)
                dispose();

            Disposed = true;
        }
    }

    public sealed class MetricPrefixes
    {
        internal static void AddPrefix(MetricPrefix prefix) => SupportedPrefixes.Add(prefix);
        internal static void RemovePrefix(MetricPrefix prefix) => SupportedPrefixes.Remove(prefix);

        public static readonly List<MetricPrefix> SupportedPrefixes = new List<MetricPrefix>();

        public static readonly MetricPrefix Deci = new MetricPrefix(0.1, "d");
        public static readonly MetricPrefix Centi = new MetricPrefix(0.01, "c");
        public static readonly MetricPrefix Milli = new MetricPrefix(0.001, "m");
        public static readonly MetricPrefix Micro = new MetricPrefix(1e-6, "u", "μ");
        public static readonly MetricPrefix Nano = new MetricPrefix(1e-9, "n");
        public static readonly MetricPrefix Pico = new MetricPrefix(1e-12, "p");
        public static readonly MetricPrefix Femto = new MetricPrefix(1e-15, "f");
        public static readonly MetricPrefix Atto = new MetricPrefix(1e-18, "a");
        public static readonly MetricPrefix Deca = new MetricPrefix(10, "da");
        public static readonly MetricPrefix Hecto = new MetricPrefix(100, "h");
        public static readonly MetricPrefix Kilo = new MetricPrefix(1000, "k");
        public static readonly MetricPrefix Mega = new MetricPrefix(1e+6, "M");
        public static readonly MetricPrefix Giga = new MetricPrefix(1e+9, "G");
        public static readonly MetricPrefix Tera = new MetricPrefix(1e+12, "T");
        public static readonly MetricPrefix Peta = new MetricPrefix(1e+15, "P");
        public static readonly MetricPrefix Exa = new MetricPrefix(1e+18, "E");
    }
}