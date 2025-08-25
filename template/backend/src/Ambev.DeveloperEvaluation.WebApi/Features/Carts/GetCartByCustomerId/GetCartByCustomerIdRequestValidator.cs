using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByCustomerId;

/// <summary>
/// Validator for <see cref="GetCartByCustomerIdRequest"/> that defines validation rules for cart retrieval by its customer identification.
/// </summary>
public class GetCartByCustomerIdRequestValidator : AbstractValidator<GetCartByCustomerIdRequest>
{
    /// <summary>
    /// Initializes a new instance of the GetCartByCustomerIdRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerId: Required and cannot be empty
    /// </remarks>
    public GetCartByCustomerIdRequestValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty().WithMessage("Customer ID is required");
    }
}