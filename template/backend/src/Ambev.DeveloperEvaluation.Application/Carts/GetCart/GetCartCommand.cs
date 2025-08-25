using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Command for retrieving a cart by its unique identifier
/// </summary>
/// <remarks>
/// This command is used to request a specific cart from the system 
/// using its <see cref="Id"/>. The response is represented by <see cref="GetCartResult"/>.
/// </remarks>
public record GetCartCommand : IRequest<GetCartResult>
{
    /// <summary>
    /// The unique identifier of the cart to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetCartCommand
    /// </summary>
    /// <param name="id">The ID of the cart to retrieve</param>
    public GetCartCommand(Guid id)
    {
        Id = id;
    }
}
