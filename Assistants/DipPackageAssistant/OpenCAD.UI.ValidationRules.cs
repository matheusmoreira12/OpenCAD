using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace OpenCAD.UI.ValidationRules
{
    class NumericValidationRule : ValidationRule
    {
        protected static readonly Regex NUMBER_VALIDATION_REGEX = new Regex(@"^(\d+\.?\d*|.\d+)([eE][+-]?\d+)?$");

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string)
            {
                string text = (string)value;
                bool isValid = NUMBER_VALIDATION_REGEX.IsMatch((string)value);

                return new ValidationResult(isValid, null);
            }

            return new ValidationResult(false, null);
        }
    }
}