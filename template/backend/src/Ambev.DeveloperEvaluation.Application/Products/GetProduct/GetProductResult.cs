namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Represents the response returned after successfully retrieving a product.
/// </summary>
public class GetProductResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the product.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product.
    /// </summary>
    public decimal UnitPrice { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the boolean that indicates whether the product is active.
    /// </summary>
    public bool IsActive { get; set; }

    public GetProductResult(Guid id, string name, decimal unitPrice, string description, string category, bool isActive)
    {
        Id = id;
        Name = name;
        UnitPrice = unitPrice;
        Description = description;
        Category = category;
        IsActive = isActive;
    }
}