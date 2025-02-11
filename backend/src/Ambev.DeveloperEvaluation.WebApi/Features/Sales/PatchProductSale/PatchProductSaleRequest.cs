using Ambev.DeveloperEvaluation.Application.Sales.PatchProductSale;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Validation;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;


namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.PatchProductSale;

public class PatchProductSaleRequest
{
    public Guid Id { get; internal set; }

    public IEnumerable<ProductRequestDto> Products { get; set; } = [];

    internal ValidationResult Validate()
    {
        Products = Products.JoinProductRequestDto();

        var validatidator = new PatchProductSaleRequestValidator();
        var validationResult = validatidator.Validate(this);

        return validationResult;
    }
}

public class PatchProductSaleValidator : Profile
{
    public PatchProductSaleValidator()
    {
        CreateMap<PatchProductSaleRequest, PatchProductSaleCommand>();
    }
}

public class PatchProductSaleRequestValidator : AbstractValidator<PatchProductSaleRequest>
{
    public PatchProductSaleRequestValidator()
    {
        RuleFor(x => x.Products)
            .NotEmpty()
            .WithMessage("It's necessary to add at least one Product")
            .ForEach(product => product.SetValidator(new ProductRequestDtoValidator()));
    }
}
