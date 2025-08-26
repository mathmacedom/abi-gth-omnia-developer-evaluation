namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

/// <summary>
/// API response model for CancelSaleItem operation
/// </summary>
public class CancelSaleItemResponse
{
    /// <summary>
    /// Message indicating the result of the cancellation operation
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
