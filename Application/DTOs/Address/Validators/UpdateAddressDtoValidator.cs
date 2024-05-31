using FluentValidation;
namespace Application.DTOs.Address.Validators
{
    public class UpdateAddressDtoValidator : AbstractValidator<AddressDto>
    {
        public UpdateAddressDtoValidator()
        {
            RuleFor(a => a.Street)
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
               .When(a => a.Street != null);

            RuleFor(a => a.City)
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .When(a => a.City != null);

            RuleFor(a => a.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("{PropertyName} must be in the format xx-xxx.")
                .When(a => a.PostalCode != null);

        }
    }
}