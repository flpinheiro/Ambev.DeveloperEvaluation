using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using FluentValidation.Results;
using MediatR;
using System.Threading;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public IEnumerable<ProductRequestDto> Products { get; set; } = [];

    internal ValidationResult Validate() 
    {
        var ids = Products.Select(p => p.ProductId).Distinct().ToList();
        var products = new List<ProductRequestDto>();

        foreach (var id in ids) 
        {
            var dto = new ProductRequestDto 
            { 
                ProductId = id,
                Quantity = Products.Where(p => p.ProductId == id).Sum(p => p.Quantity),
            };
            products.Add(dto);
        }
        Products = products;

        var validatidator = new CreateSaleRequestValidator();
        var validationResult = validatidator.Validate(this);

        return validationResult;
    }
}
