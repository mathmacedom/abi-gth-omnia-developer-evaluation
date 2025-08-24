namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// API response model for GetAllProducts operation
/// </summary>
public class GetAllProductsResponse
{
    /// <summary>
    /// List of products
    /// </summary>
    public List<ProductSummary> Products { get; set; } = new();
}

/// <summary>
/// Summary information for a product
/// </summary>
public class ProductSummary
{
    /// <summary>
    /// The unique identifier of the product
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The name of the product
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The unit price of the product
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// The category of the product
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether the product is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// The date and time when the product was created
    /// </summary>
    public DateTime CreatedAt { get; set; }
}
