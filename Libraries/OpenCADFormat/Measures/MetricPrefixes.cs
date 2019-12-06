using System.Collections.Generic;

namespace OpenCAD.OpenCADFormat.Measures
{
    public sealed class MetricPrefixes
    {
        public static readonly MetricPrefix Deci = new MetricPrefix("Deci", 0.1, "d");
        public static readonly MetricPrefix Centi = new MetricPrefix("Centi", 0.01, "c");
        public static readonly MetricPrefix Milli = new MetricPrefix("Milli", 0.001, "m");
        public static readonly MetricPrefix Micro = new MetricPrefix("Micro", 1e-6, "u", "μ");
        public static readonly MetricPrefix Nano = new MetricPrefix("Nano", 1e-9, "n");
        public static readonly MetricPrefix Pico = new MetricPrefix("Pico", 1e-12, "p");
        public static readonly MetricPrefix Femto = new MetricPrefix("Femto", 1e-15, "f");
        public static readonly MetricPrefix Atto = new MetricPrefix("Atto", 1e-18, "a");
        public static readonly MetricPrefix Deca = new MetricPrefix("Deca", 10, "da");
        public static readonly MetricPrefix Hecto = new MetricPrefix("Hecto", 100, "h");
        public static readonly MetricPrefix Kilo = new MetricPrefix("Kilo", 1000, "k");
        public static readonly MetricPrefix Mega = new MetricPrefix("Mega", 1e+6, "M");
        public static readonly MetricPrefix Giga = new MetricPrefix("Giga", 1e+9, "G");
        public static readonly MetricPrefix Tera = new MetricPrefix("Tera", 1e+12, "T");
        public static readonly MetricPrefix Peta = new MetricPrefix("Peta", 1e+15, "P");
        public static readonly MetricPrefix Exa = new MetricPrefix("Exa", 1e+18, "E");
    }
}