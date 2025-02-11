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
        Products = Products.JoinProductRequestDto();

        var validatidator = new CreateSaleRequestValidator();
        var validationResult = validatidator.Validate(this);

        return validationResult;
    }
}
