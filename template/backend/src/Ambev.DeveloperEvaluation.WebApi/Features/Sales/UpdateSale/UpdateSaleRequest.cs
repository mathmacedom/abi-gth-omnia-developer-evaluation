using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Request model for updating a sale
/// </summary>
public class UpdateSaleRequest
{
    /// <summary>
    /// The unique identifier of the sale
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }

    /// <summary>
    /// The list of product identifiers included in the sale
    /// </summary>
    public List<Guid> ProductIds { get; set; } = new();

    /// <summary>
    /// The updated total amount
    /// </summary>
    public decimal TotalAmount { get; set; }
}
