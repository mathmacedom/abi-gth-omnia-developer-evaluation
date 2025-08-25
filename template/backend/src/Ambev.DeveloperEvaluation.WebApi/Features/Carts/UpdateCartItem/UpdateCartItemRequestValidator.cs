using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCartItem;

/// <summary>
/// Validator for <see cref="UpdateCartItemRequest"/> that defines validation rules for cart item update.
/// </summary>
public class UpdateCartItemRequestValidator : AbstractValidator<UpdateCartItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateCartItemRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CartId: Required and cannot be empty
    /// - CartItemId: Required and cannot be empty
    /// - Quantity: Must be greater than 0 and less than or equal 20
    /// </remarks>
    public UpdateCartItemRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("Cart ID is required");

        RuleFor(x => x.CartItemId)
            .NotEmpty()
            .WithMessage("Cart Item ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items.");
    }
}
