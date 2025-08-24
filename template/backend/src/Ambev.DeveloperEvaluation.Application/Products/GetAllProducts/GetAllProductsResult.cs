using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Represents the response returned after successfully retrieving all products.
/// </summary>
public class GetAllProductsResult
{
    /// <summary>
    /// Gets or sets the list of products.
    /// </summary>
    public List<GetProductResult> Products { get; set; } = new();
}