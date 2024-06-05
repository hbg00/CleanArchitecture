using Application.DTOs.Address.Validators;
using FluentValidation;

namespace Application.DTOs.Restaurant.Validators
{
    public class UpdateRestaurantDtoValidator : AbstractValidator<UpdateRestaurantDto>
    {
        public UpdateRestaurantDtoValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.")
                .When(p => p.Name != null);

            RuleFor(p => p.Category)
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                 .When(p => p.Category != null);

            RuleFor(p => p.ContactEmail)
                .EmailAddress().WithMessage("{PropertyName} must be a valid email address.")
                .Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("{PropertyName} must be a valid email address.");

            RuleFor(p => p.ContactName)
                .MaximumLength(25).WithMessage("{PropertyName} must not exceed 50 characters.")
                .When(p => p.ContactName != null);

            RuleFor(a => a.Address)
                .SetValidator(new UpdateAddressDtoValidator())
                .When(a => a.Address != null);
        }
    }
}