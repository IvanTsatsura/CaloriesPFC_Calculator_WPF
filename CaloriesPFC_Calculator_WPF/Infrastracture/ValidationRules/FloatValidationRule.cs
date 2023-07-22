using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CaloriesPFC_Calculator_WPF.Infrastracture.ValidationRules
{
    class FloatValidationRule : ValidationRule
    {
        public FloatValidationRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string pattern = @"^[0-9]*(?:\.[0-9]*)?$";
            Regex regex = new Regex(pattern);
            try
            {
                if (((string)value).Length >= 0)
                    if (!regex.IsMatch((string)value))
                        throw new Exception();
            }
            catch (Exception e)
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
