using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCartByCustomerId;

/// <summary>
/// Validator for <see cref="GetCartByCustomerIdCommand"/>.
/// </summary>
public class GetCartByCustomerIdValidator : AbstractValidator<GetCartByCustomerIdCommand>
{
    /// <summary>
    /// Initializes a new instance of the GetCartByCustomerIdValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerId: Must not be empty
    /// </remarks>
    public GetCartByCustomerIdValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty()
            .WithMessage("CustomerId is required");
    }
}
