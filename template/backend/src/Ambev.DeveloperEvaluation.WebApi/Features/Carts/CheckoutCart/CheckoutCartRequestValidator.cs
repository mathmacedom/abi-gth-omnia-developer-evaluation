using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CheckoutCart;

/// <summary>
/// Validator for <see cref="CheckoutCartRequest"/> that defines validation rules for checkout a cart.
/// </summary>
public class CheckoutCartRequestValidator : AbstractValidator<CheckoutCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the CheckoutCartRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Required and cannot be empty
    /// </remarks>
    public CheckoutCartRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Cart ID is required");
    }
}
