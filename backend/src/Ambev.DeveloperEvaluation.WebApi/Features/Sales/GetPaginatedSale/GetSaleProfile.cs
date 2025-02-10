using Ambev.DeveloperEvaluation.Application.Sales.GetPaginatedSales;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetPaginatedSale;

public class GetSaleProfile: Profile
{
    public GetSaleProfile()
    {
        CreateMap<GetSalePaginated, GetSaleCommand>();
        CreateMap<GetPaginatedSaleResult, GetPaginatedSaleResponse>();
    }
}
