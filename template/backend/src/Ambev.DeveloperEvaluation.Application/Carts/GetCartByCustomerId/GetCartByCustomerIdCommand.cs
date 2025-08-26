using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByCustomerId;

/// <summary>
/// Command for retrieving a cart by its customer's identifier.
/// </summary>
/// <remarks>
/// This command is used to request a cart by its customer's identifier from the system. The response is represented by <see cref="GetCartByCustomerIdResult"/>.
/// </remarks>
public record GetCartByCustomerIdCommand : IRequest<GetCartByCustomerIdResult>
{
    /// <summary>
    /// Gets or sets the external identifier of the customer who made the sale.
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// Executes validation rules against the command data.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new GetCartByCustomerIdValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
