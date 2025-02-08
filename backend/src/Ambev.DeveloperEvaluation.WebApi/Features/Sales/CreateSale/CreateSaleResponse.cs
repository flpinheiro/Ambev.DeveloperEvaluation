﻿using Ambev.DeveloperEvaluation.Domain.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleResponse
{
    public Guid Id { get; set; }
    /// <summary>
    /// Gets the sale number.
    /// Sequence number generated by the system to identify the sale.
    /// </summary>
    public int Number { get; set; }

    /// <summary>
    /// Gets the date and time when the sale was made.
    /// use the time zone utc to save the date
    /// </summary>
    public DateTime Date { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets the total value of the sale.
    /// must be calculated based on the sum of the products in the sale.
    /// </summary>
    public decimal TotalValue { get; set; }

    /// <summary>
    /// gets the sale.status
    /// true = sale is active
    /// false = sale is canceled
    /// </summary>
    public bool Status { get; set; } = true;


    /// <summary>
    /// Gets the products in the sale.
    /// </summary>
    public IEnumerable<ProductResponseDto> Products { get; set; } = [];
}
