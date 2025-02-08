using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Dtos;

public class ProductRequestDto
{
    /// <summary>
    /// Gets the product id and product information.
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets the product quantity.
    /// </summary>
    public int Quantity { get; set; }
}
