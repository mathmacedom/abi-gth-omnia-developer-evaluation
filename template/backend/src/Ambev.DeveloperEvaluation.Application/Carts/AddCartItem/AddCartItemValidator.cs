using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.AddCartItem;

/// <summary>
/// Validator for AddCartItemCommand
/// </summary>
public class AddCartItemCommandValidator : AbstractValidator<AddCartItemCommand>
{
    /// <summary>
    /// Initializes validation rules for AddCartItemCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CartId: Required and cannot be empty
    /// - ProductId: Required and cannot be empty
    /// - Quantity: Must be greater than 0
    /// </remarks>
    public AddCartItemCommandValidator()
    {
        RuleFor(x => x.CartId)
            .NotEmpty()
            .WithMessage("Cart ID is required");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0);
    }
}
