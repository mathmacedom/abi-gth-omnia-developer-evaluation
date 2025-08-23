namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;

/// <summary>
/// API response model for CancelSale operation
/// </summary>
public class CancelSaleResponse
{
    /// <summary>
    /// Message indicating the result of the cancellation operation
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
