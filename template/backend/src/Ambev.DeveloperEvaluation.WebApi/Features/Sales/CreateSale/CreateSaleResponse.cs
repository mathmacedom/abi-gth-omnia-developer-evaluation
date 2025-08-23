namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// API response model for CreateSale operation
/// </summary>
public class CreateSaleResponse
{
    /// <summary>
    /// The unique identifier of the created sale
    /// </summary>
    public Guid Id { get; set; }

     /// <summary>
    /// The identifier of the customer making the purchase
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The branch where the sale was registered
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The date and time when the sale was created
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The list of items included in the sale
    /// </summary>
    public List<SaleItemResponse> Items { get; set; } = new();
}

/// <summary>
/// Represents a single product item within a sale
/// </summary>
public class SaleItemResponse
{
    /// <summary>
    /// The identifier of the product
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product sold
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// The price per unit of the product
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The discount applied to this item
    /// </summary>
    public decimal Discount { get; set; }

    /// <summary>
    /// The total amount for this item (Quantity × UnitPrice - Discount)
    /// </summary>
    public decimal Total { get; set; }
}
