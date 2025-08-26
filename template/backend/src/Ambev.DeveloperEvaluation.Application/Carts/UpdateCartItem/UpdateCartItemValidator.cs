using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCartItem;

/// <summary>
/// Validator for UpdateCartItemCommand
/// </summary>
public class UpdateCartItemCommandValidator : AbstractValidator<UpdateCartItemCommand>
{
    /// <summary>
    /// Initializes validation rules for UpdateCartItemCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CartId: Required and cannot be empty
    /// - CartItemId: Required and cannot be empty
    /// - Quantity: Must be greater than 0 and less than or equal 20
    /// </remarks>
    public UpdateCartItemCommandValidator()
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
