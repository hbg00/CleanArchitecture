using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Domain.Identity
{
    public class PasswordComplexityAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return new ValidationResult("Password is required.");

            var password = value as string;

            if (string.IsNullOrWhiteSpace(password))
                return new ValidationResult("Password cannot be empty or whitespace.");

            if (password.Length < 6)
                return new ValidationResult("Password must be at least 6 characters long.");

            if (password.Any(char.IsWhiteSpace))
                return new ValidationResult("Password cannot contain whitespace characters.");

            if (!Regex.IsMatch(password, @"[A-Z]"))
                return new ValidationResult("Password must contain at least one uppercase letter.");

            if (!Regex.IsMatch(password, @"[a-z]"))
                return new ValidationResult("Password must contain at least one lowercase letter.");

            if (!Regex.IsMatch(password, @"\d"))
                return new ValidationResult("Password must contain at least one digit.");

            if (!Regex.IsMatch(password, @"[\W_]"))
                return new ValidationResult("Password must contain at least one special character.");

            return ValidationResult.Success;
        }
    }
}
