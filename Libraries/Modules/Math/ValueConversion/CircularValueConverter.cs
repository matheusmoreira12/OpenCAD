using System;

namespace OpenCAD.Modules.Math.ValueConversion
{
    public sealed class CircularValueConverter : ValueConverter
    {
        public override Type InputType => typeof(object);

        public override Type OutputType => typeof(object);

        public override object Convert(object value) => value;

        public override object ConvertBack(object value) => value;
    }
}
