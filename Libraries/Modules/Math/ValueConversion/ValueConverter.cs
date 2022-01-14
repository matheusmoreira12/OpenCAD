using System;

namespace OpenCAD.Modules.Math.ValueConversion
{
    public abstract class ValueConverter
    {
        public abstract Type InputType { get; }

        public abstract Type OutputType { get; }

        public abstract object Convert(object value);

        public abstract object ConvertBack(object value);
    }

    public abstract class ValueConverter<TIn, TOut> : ValueConverter
    {
        public override Type InputType => typeof(TIn);

        public override Type OutputType => typeof(TOut);

        public override object Convert(object value) => Convert((TIn)value);

        public override object ConvertBack(object value) => ConvertBack((TOut)value);

        public abstract TOut Convert(TIn value);

        public abstract TIn ConvertBack(TOut value);
    }
}
