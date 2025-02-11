using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.RemoveProductSale;

public class RemoveProductSaleRequest
{
    public Guid Id { get; internal set; }

    public IEnumerable<Guid> Products { get; set; } = [];

    internal ValidationResult Validate()
    {
        var validator = new RemoveProductSaleValidator();
        var result = validator.Validate(this);
        return result;
    }
}