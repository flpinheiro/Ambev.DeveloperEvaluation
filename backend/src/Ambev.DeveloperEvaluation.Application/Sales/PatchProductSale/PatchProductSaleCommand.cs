using Ambev.DeveloperEvaluation.Domain.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.PatchProductSale;

public class PatchProductSaleCommand : IRequest<bool>
{
    public Guid Id { get; internal set; }

    public IEnumerable<ProductRequestDto> Products { get; set; } = [];
}
