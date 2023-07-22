using System;
using System.Globalization;
using System.Windows.Controls;

namespace CaloriesPFC_Calculator_WPF.Infrastracture.ValidationRules
{
    class WeightValidationRule : ValidationRule
    {
        private const float MIN_POS_WEIGHT = 0.0f;

        public WeightValidationRule()
        {
        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            float weight = MIN_POS_WEIGHT;
            try
            {
                if (((string)value).Length >= 0)
                    weight = float.Parse((string)value);
            }
            catch (Exception e) 
            {
                return new ValidationResult(false, $"Illegal characters or {e.Message}");
            }

            if (weight < MIN_POS_WEIGHT)
            {
                return new ValidationResult(false, $"Please enter a weight greater than {MIN_POS_WEIGHT}");
            }

            return ValidationResult.ValidResult;
        }
    }
}
