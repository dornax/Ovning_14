using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Garage3.Validations
{
    public class DifferentValuesValidation : ValidationAttribute
    {
        public string OtherProperty { get; }

        public DifferentValuesValidation(string otherProperty)
        {
            ArgumentNullException.ThrowIfNull(otherProperty);

            OtherProperty = otherProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(OtherProperty)!;
            object? otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null)!;

            const string errorMessage = "Last name can not be ethe same as First name!";

            if (otherPropertyValue is string otherValue)
            {
                if (value is string input)
                {
                    return input != otherValue ?
                        ValidationResult.Success :
                            new ValidationResult(errorMessage);
                }
            }
            return new ValidationResult(errorMessage);
        }
    }
}
