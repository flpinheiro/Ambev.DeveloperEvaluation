using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.WebApi.Common;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetPaginatedProduct;

public class GetPaginatedProductRequest : PaginatedRequest
{
}

public class GetPaginatedProductResponse 
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