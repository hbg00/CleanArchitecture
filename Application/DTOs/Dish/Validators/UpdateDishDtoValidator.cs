using Application.DTOs.Restaurant;
using FluentValidation;


namespace Application.DTOs.Dish.Validators
{
    public class UpdateDishDtoValidator : AbstractValidator<UpdateDishDto>
    {
        public UpdateDishDtoValidator()
        {
            RuleFor(p => p.Name)
             .NotEmpty().WithMessage("{PropertyName} is required")
             .NotNull()
             .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.");

            RuleFor(p => p.Price)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

        }
    }
}