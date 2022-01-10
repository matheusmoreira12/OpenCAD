using System;

namespace OpenCAD.Modules.Math
{
    public abstract class ValueConversion
    {
        public abstract Type InputType { get; }

        public abstract Type OutputType { get; }

        public abstract object Convert(object value);
    }

    public sealed class ValueConversion<TIn, TOut> : ValueConversion
    {
        public ValueConversion(Func<TIn, TOut> converter)
        {
            this.converter = converter ?? throw new ArgumentNullException(nameof(converter));
        }

        public override Type InputType => typeof(TIn);

        public override Type OutputType => typeof(TOut);

        public override object Convert(object value) => converter((TIn)value);

        private readonly Func<TIn, TOut> converter;

        public TOut Convert(TIn value) => converter(value);
    }
}
