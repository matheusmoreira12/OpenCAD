using OpenCAD.OpenCADFormat.DataTypes;

namespace OpenCAD.OpenCADFormat.Measures
{
    public interface IMetricPrefix
    {
        BigFloat Multiplier { get; }
        string Symbol { get; }
    }

    public sealed class MetricPrefix : IMetricPrefix
    {
        public BigFloat Multiplier { get; private set; }
        public string Symbol { get; private set; }

        public MetricPrefix(BigFloat multiplier, string symbol)
        {
            Multiplier = multiplier;
            Symbol = symbol;
        }
    }

    public static class MetricPrefixes
    {
        public static IMetricPrefix Deci = new MetricPrefix(0.1, "d");
        public static IMetricPrefix Centi = new MetricPrefix(0.01, "c");
        public static IMetricPrefix Milli = new MetricPrefix(0.001, "m");
        public static IMetricPrefix Micro = new MetricPrefix(1e-6, "μ");
        public static IMetricPrefix Nano = new MetricPrefix(1e-9, "n");
        public static IMetricPrefix Pico = new MetricPrefix(1e-12, "p");
        public static IMetricPrefix Femto = new MetricPrefix(1e-15, "f");
        public static IMetricPrefix Atto = new MetricPrefix(1e-18, "a");
        public static IMetricPrefix Deca = new MetricPrefix(10, "da");
        public static IMetricPrefix Hecto = new MetricPrefix(100, "h");
        public static IMetricPrefix Kilo = new MetricPrefix(1000, "k");
        public static IMetricPrefix Mega = new MetricPrefix(1e+6, "M");
        public static IMetricPrefix Giga = new MetricPrefix(1e+9, "G");
        public static IMetricPrefix Tera = new MetricPrefix(1e+12, "T");
        public static IMetricPrefix Peta = new MetricPrefix(1e+15, "P");
        public static IMetricPrefix Exa = new MetricPrefix(1e+18, "E");
    }
}