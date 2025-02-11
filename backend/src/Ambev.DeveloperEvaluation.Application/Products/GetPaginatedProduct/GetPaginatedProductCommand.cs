using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetPaginatedProduct;

public class GetPaginatedProductCommand : PaginatedCommand, IRequest<PaginatedList<GetPaginatedProductResult>>
{
}

public class GetPaginatedProductResult
{
    public Guid Id { get; set; }
    /// <summary>
    /// Gets the product name.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets the product description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets the product price.
    /// </summary>
    public decimal Price { get; set; }
}

public class GetPaginatedProductProfile : Profile
{
    public GetPaginatedProductProfile()
    {
        CreateMap<GetPaginatedProductCommand, GetPaginatedProductDto>();
        CreateMap<Product, GetPaginatedProductResult>();
    }
}
public class GetPaginatedProductHandler : IRequestHandler<GetPaginatedProductCommand, PaginatedList<GetPaginatedProductResult>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    public GetPaginatedProductHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }

    public async Task<PaginatedList<GetPaginatedProductResult>> Handle(GetPaginatedProductCommand request, CancellationToken cancellationToken)
    {
        var dto = _mapper.Map<GetPaginatedProductDto>(request);

        var products = await _productRepository.GetPaginatedProducts(dto, cancellationToken);

        var result = _mapper.Map<PaginatedList<GetPaginatedProductResult>>(products);

        return result;
    }
}