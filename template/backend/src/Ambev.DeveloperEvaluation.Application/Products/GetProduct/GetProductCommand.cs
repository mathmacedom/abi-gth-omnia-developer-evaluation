using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Command for retrieving a product by its unique identifier
/// </summary>
/// <remarks>
/// This command is used to request a specific product from the system 
/// using its <see cref="Id"/>. The response is represented by <see cref="GetProductResult"/>.
/// </remarks>
public record GetProductCommand : IRequest<GetProductResult>
{
    /// <summary>
    /// The unique identifier of the product to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetProductCommand
    /// </summary>
    /// <param name="id">The ID of the product to retrieve</param>
    public GetProductCommand(Guid id)
    {
        Id = id;
    }
}
