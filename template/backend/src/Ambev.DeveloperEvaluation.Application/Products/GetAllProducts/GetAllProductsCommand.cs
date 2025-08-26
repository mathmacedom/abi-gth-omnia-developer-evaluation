using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Command for retrieving all products
/// </summary>
/// <remarks>
/// This command is used to request all products from the system. The response is represented by <see cref="GetAllProductsResult"/>.
/// </remarks>
public record GetAllProductsCommand : IRequest<GetAllProductsResult>
{
    /// <summary>
    /// Filter by active products only (optional)
    /// </summary>
    public bool? ActiveOnly { get; set; }

    /// <summary>
    /// Filter by category (optional)
    /// </summary>
    public string? Category { get; set; }

    public GetAllProductsCommand()
    {
        
    }

    /// <summary>
    /// Initializes a new instance of GetProductCommand
    /// </summary>
    /// <param name="activeOnly">Filter by active products only (optional)</param>
    /// <param name="category">Filter by category (optional)</param>
    public GetAllProductsCommand(bool? activeOnly, string? category)
    {
        ActiveOnly = activeOnly;
        Category = category;
    }
}
