using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;

/// <summary>
/// Command for delete item to a cart
/// </summary>
public record DeleteCartItemCommand : IRequest<DeleteCartItemResult>
{
    /// <summary>
    /// Gets or sets the external identifier of the cart from which the product will be deleted
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the external identifier of the cart item to be deleted
    /// </summary>
    public Guid CartItemId { get; set; }

    /// <summary>
    /// Executes validation rules against the command data.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new DeleteCartItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
