using Ambev.DeveloperEvaluation.Domain.Dtos;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductRequestDtoValidator : AbstractValidator<ProductRequestDto>
{
    public ProductRequestDtoValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("ProductId is required.");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20)
            .WithMessage("quantity must not be greater thean 20");
    }
}