using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;

/// <summary>
/// Validator for <see cref="CreateCartRequest"/> that defines validation rules for product creation.
/// </summary>
public class CreateCartRequestValidator : AbstractValidator<CreateCartRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateCartRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Branch: Must not be empty and must not exceed 200 characters
    /// - CustomerId: Must not be empty
    /// </remarks>
    public CreateCartRequestValidator()
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