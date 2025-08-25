using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart;

/// <summary>
/// Validator for <see cref="CreateCartCommand"/> that defines validation rules for cart creation.
/// </summary>
public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Branch: Must not be empty and must not exceed 200 characters
    /// - CustomerId: Must not be empty
    /// </remarks>
    public CreateCartCommandValidator()
    {
        RuleFor(cart => cart.Branch)
            .NotEmpty()
            .WithMessage("Branch is required")
            .MaximumLength(200).WithMessage("Branch name must not exceed 200 characters.");

        RuleFor(cart => cart.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId is required");
    }
}