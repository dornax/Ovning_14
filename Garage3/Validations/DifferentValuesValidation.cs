using System.ComponentModel.DataAnnotations;

namespace Garage3.Validations
{
    public class DifferentValuesValidation : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            const string errorMessage = "Fail from Kalle Anka";

            return base.IsValid(value, validationContext);
        }
    }
}
