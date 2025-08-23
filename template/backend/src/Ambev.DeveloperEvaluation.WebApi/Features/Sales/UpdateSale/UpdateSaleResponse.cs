using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;

/// <summary>
/// API response model for UpdateSale operation
/// </summary>
public class UpdateSaleResponse
{
    /// <summary>
    /// The unique identifier of the updated sale
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
    /// The updated total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// The date and time when the sale was last updated
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// The list of items included in the sale
    /// </summary>
    public List<SaleItemResponse> Items { get; set; } = new();
}
