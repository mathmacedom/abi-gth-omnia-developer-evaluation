using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCartItem;

/// <summary>
/// Validator for DeleteCartItemRequest
/// </summary>
public class DeleteCartItemRequestValidator : AbstractValidator<DeleteCartItemRequest>
{
    /// <summary>
    /// Initializes validation rules for DeleteCartItemRequest
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CartId: Required and cannot be empty
    /// - CartItemId: Required and cannot be empty
    /// - Quantity: Must be greater than 0 and less than or equal 20
    /// </remarks>
    public DeleteCartItemRequestValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("Cart ID is required");

        RuleFor(x => x.CartItemId)
            .NotEmpty()
            .WithMessage("Cart Item ID is required");
    }
}
