namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Represents a request to create a new sale in the system.
/// </summary>
public class CreateSaleRequest
{
    /// <summary>
    /// The identifier of the customer making the purchase
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// The branch where the sale was registered
    /// </summary>
    public Guid BranchId { get; set; }

    /// <summary>
    /// The list of products included in the sale
    /// </summary>
    public List<Guid> ProductIds { get; set; } = new();

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal TotalAmount { get; set; }
}
