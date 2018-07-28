namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IMetricPrefix
    {
        double Multiplier { get; }
        string Symbol { get; }
        string UISymbol { get; }
    }

    public sealed class MetricPrefix : IMetricPrefix
    {
        public double Multiplier { get; private set; }
        public string Symbol { get; private set; }
        public string UISymbol { get; private set; }

        public MetricPrefix(double multiplier, string symbol, string uiSymbol = null)
        {
            Multiplier = multiplier;
            Symbol = symbol;
            UISymbol = uiSymbol ?? symbol;
        }
    }

    public sealed class MetricPrefixes
    {
        public static readonly IMetricPrefix Deci = new MetricPrefix(0.1, "d");
        public static readonly IMetricPrefix Centi = new MetricPrefix(0.01, "c");
        public static readonly IMetricPrefix Milli = new MetricPrefix(0.001, "m");
        public static readonly IMetricPrefix Micro = new MetricPrefix(1e-6, "u", "μ");
        public static readonly IMetricPrefix Nano = new MetricPrefix(1e-9, "n");
        public static readonly IMetricPrefix Pico = new MetricPrefix(1e-12, "p");
        public static readonly IMetricPrefix Femto = new MetricPrefix(1e-15, "f");
        public static readonly IMetricPrefix Atto = new MetricPrefix(1e-18, "a");
        public static readonly IMetricPrefix Deca = new MetricPrefix(10, "da");
        public static readonly IMetricPrefix Hecto = new MetricPrefix(100, "h");
        public static readonly IMetricPrefix Kilo = new MetricPrefix(1000, "k");
        public static readonly IMetricPrefix Mega = new MetricPrefix(1e+6, "M");
        public static readonly IMetricPrefix Giga = new MetricPrefix(1e+9, "G");
        public static readonly IMetricPrefix Tera = new MetricPrefix(1e+12, "T");
        public static readonly IMetricPrefix Peta = new MetricPrefix(1e+15, "P");
        public static readonly IMetricPrefix Exa = new MetricPrefix(1e+18, "E");
    }
}