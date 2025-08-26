using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.AddCartItem;

/// <summary>
/// Command for add item to a cart
/// </summary>
public record AddCartItemCommand : IRequest<AddCartItemResult>
{
    /// <summary>
    /// Gets or sets the external identifier of the cart from which the product will be added
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the external identifier of the product to be added
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product to be added to cart.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Executes validation rules against the command data.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new AddCartItemCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
