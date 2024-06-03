using Domain.Identity.Validators;
using System.ComponentModel.DataAnnotations;

namespace Domain.Identity
{
    public class RegistrationRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddressComplexity]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Username must be at least 6 characters long.")]
        public string UserName { get; set; }

        [Required]
        [PasswordComplexity]
        public string Password { get; set; }
    }
}