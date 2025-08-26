using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;

/// <summary>
/// Command for update item to a cart
/// </summary>
public record UpdateCartItemCommand : IRequest<UpdateCartItemResult>
{
    /// <summary>
    /// Gets or sets the external identifier of the cart from which the product will be updated
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the external identifier of the cart item to be updated
    /// </summary>
    public Guid CartItemId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product to be updated to cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Executes validation rules against the command data.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new UpdateCartItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
