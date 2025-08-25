using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCartItem;

/// <summary>
/// Validator for DeleteCartItemCommand
/// </summary>
public class DeleteCartItemCommandValidator : AbstractValidator<DeleteCartItemCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteCartItemCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CartId: Required and cannot be empty
    /// - CartItemId: Required and cannot be empty
    /// </remarks>
    public DeleteCartItemCommandValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("Cart ID is required");

        RuleFor(x => x.CartItemId)
            .NotEmpty()
            .WithMessage("Cart Item ID is required");
    }
}
