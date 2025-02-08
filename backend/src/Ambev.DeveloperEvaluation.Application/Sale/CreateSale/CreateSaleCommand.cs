using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleCommand : IRequest<CreateSaleResult>
{
    public IEnumerable<ProductRequestDto> Products { get; set; } = [];
}
