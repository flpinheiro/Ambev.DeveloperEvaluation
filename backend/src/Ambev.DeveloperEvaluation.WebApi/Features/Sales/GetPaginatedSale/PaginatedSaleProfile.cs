using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSale;

public class PaginatedSaleProfile: Profile
{
    public PaginatedSaleProfile()
    {
        CreateMap<GetPaginatedSaleRequest, GetPaginatedSalesCommand>();
        CreateMap<GetPaginatedSaleResult, GetPaginatedSaleResponse>();
    }
}
