namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// Request model for retrieving all products with optional filters
/// </summary>
public class GetAllProductsRequest
{
    /// <summary>
    /// Filter by active products only (optional)
    /// </summary>
    public bool? ActiveOnly { get; set; }

    /// <summary>
    /// Filter by category (optional)
    /// </summary>
    public string? Category { get; set; }
}
