using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
/// Command for retrieving a sale by its unique identifier
/// </summary>
/// <remarks>
/// This command is used to request a specific sale from the system 
/// using its <see cref="Id"/>. The response is represented by <see cref="GetSaleResult"/>.
/// </remarks>
public record GetSaleCommand : IRequest<GetSaleResult>
{
    /// <summary>
    /// The unique identifier of the sale to retrieve
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Initializes a new instance of GetSaleCommand
    /// </summary>
    /// <param name="id">The ID of the sale to retrieve</param>
    public GetSaleCommand(Guid id)
    {
        Id = id;
    }
}
