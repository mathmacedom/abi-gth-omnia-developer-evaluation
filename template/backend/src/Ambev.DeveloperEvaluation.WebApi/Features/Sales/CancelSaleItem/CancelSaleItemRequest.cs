namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

/// <summary>
/// Request model for cancel a sale
/// </summary>
public class CancelSaleItemRequest
{
    /// <summary>
    /// The unique identifier of the sale from which the product will be cancelled
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// The unique identifier of the sale item to be cancelled
    /// </summary>
    public Guid SaleItemId { get; set; }
}
