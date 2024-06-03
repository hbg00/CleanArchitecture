using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Identity.Validators
{
    public class EmailAddressComplexity : ValidationAttribute
    {
        private readonly string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success;
            }

            string email = value.ToString();

            if (!Regex.IsMatch(email, pattern))
            {
                return new ValidationResult("The email address is not valid.");
            }

            return ValidationResult.Success;
        }
    }
}
