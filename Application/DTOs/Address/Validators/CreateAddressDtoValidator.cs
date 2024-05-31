using Application.DTOs.Address;
using FluentValidation;

public class CreateAddressDtoValidator : AbstractValidator<AddressDto>
{
    public CreateAddressDtoValidator()
    {
        RuleFor(a => a.Street)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(a => a.City)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

        RuleFor(a => a.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("{PropertyName} must be in the format xx-xxx.")
            .When(a => a.PostalCode != null);
    }
}