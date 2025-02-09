
using Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleRequest
{
    public Guid Id { get; internal set; }
}
public class DeleteSaleValidator: Profile
{
    public DeleteSaleValidator()
    {
        CreateMap<DeleteSaleRequest, DeleteSaleCommand>();
    }
}
