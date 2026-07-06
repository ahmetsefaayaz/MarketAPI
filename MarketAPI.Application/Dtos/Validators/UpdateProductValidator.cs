using FluentValidation;
using MarketAPI.Application.Dtos.ProductDto;

namespace MarketAPI.Application.Dtos.Validators;

public class UpdateProductValidator: AbstractValidator<UpdateProductDto>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Name)
            .NotNull().WithMessage("Name is required")
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long")
            .MaximumLength(40).WithMessage("Name must be less than 40 characters long");
        RuleFor(x => x.Price)
            .NotNull().WithMessage("Price is required")
            .GreaterThan(0).WithMessage("Price must be greater than 0");
        RuleFor(x => x.Stock)
            .NotNull().WithMessage("Stock is required")
            .GreaterThanOrEqualTo(0).WithMessage("Stock cannot be less than 0");
    }
}