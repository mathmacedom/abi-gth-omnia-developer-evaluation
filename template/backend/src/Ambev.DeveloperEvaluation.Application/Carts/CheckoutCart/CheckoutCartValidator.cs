using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CheckoutCart;

/// <summary>
/// Validator for <see cref="CheckoutCartCommand"/> that defines validation rules for cart creation.
/// </summary>
public class CheckoutCartCommandValidator : AbstractValidator<CheckoutCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the CheckoutCartCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// </remarks>
    public CheckoutCartCommandValidator()
    {
        RuleFor(sale => sale.Id)
            .NotEmpty()
            .WithMessage("Cart Id is required");
    }
}