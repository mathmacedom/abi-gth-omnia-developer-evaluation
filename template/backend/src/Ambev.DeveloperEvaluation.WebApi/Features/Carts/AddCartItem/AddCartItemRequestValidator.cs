using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddCartItem;

/// <summary>
/// Validator for <see cref="AddCartItemRequest"/> that defines validation rules for add a cart item.
/// </summary>
public class AddCartItemRequestValidator : AbstractValidator<AddCartItemRequest>
{
    /// <summary>
    /// Initializes validation rules for AddCartItemRequest.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CartId: Required and cannot be empty
    /// - ProductId: Required and cannot be empty
    /// - Quantity: Must be greater than 0
    /// </remarks>
    public AddCartItemRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("Cart ID is required");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");
    }
}
