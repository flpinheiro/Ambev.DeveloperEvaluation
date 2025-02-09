using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using System.Security.Principal;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
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

    /// <summary>
    /// Gets the product quantity.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets the product status.
    /// </summary>
    public ProductStatus Status { get; set; }

    /// <summary>
    /// Performs validation of the <see cref="Product" /> entity using the <see cref="ProductValidator" /> rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">name format and length</list>
    /// <list type="bullet">Desciption format and length</list>
    /// <list type="bullet">Price must be greater than zero</list>
    /// <list type="bullet">quantity must be greater than zero</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    /// <summary>
    /// Gets the products in the sale.
    /// </summary>
    public IEnumerable<ProductSale> ProductSales { get; set; } = [];
}