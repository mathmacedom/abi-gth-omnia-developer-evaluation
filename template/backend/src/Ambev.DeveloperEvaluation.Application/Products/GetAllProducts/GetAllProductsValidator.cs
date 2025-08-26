using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Validator for <see cref="GetAllProductsCommand"/>.
/// </summary>
public class GetAllProductsValidator : AbstractValidator<GetAllProductsCommand>
{
    /// <summary>
    /// Initializes a new instance of the GetProductValidator with defined validation rules.
    /// </summary>
    public GetAllProductsValidator()
    {
    }
}
