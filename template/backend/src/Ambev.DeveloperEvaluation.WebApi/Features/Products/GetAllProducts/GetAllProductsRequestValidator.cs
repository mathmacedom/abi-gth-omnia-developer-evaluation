using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

/// <summary>
/// Validator for <see cref="GetAllProductsRequest"/> that defines validation rules for products retrieval.
/// </summary>
public class GetAllProductsRequestValidator : AbstractValidator<GetAllProductsRequest>
{
    /// <summary>
    /// Initializes a new instance of the GetAllProductsRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Category: When provided, must not exceed 100 characters in length.
    /// </remarks>
    public GetAllProductsRequestValidator()
    {
        RuleFor(x => x.Category)
            .MaximumLength(100)
            .When(x => !string.IsNullOrEmpty(x.Category))
            .WithMessage("Category cannot exceed 100 characters");
    }
}
