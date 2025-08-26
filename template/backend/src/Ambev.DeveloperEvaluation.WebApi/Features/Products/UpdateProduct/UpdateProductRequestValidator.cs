using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Validator for <see cref="UpdateProductRequest"/> that defines validation rules for product update.
/// </summary>
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - ID: Must not be empty
    /// - Name: Must not be empty and must be between 2 and 200 characters
    /// - Description: Must not be empty and must not exceed 1000 characters
    /// - UnitPrice: Must be greater than zero and less than or equal to 999,999.99
    /// - Category: Must not be empty and must be between 2 and 100 characters
    /// </remarks>
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Product name is required")
            .Length(2, 200)
            .WithMessage("Product name must be between 2 and 200 characters");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Product description is required")
            .MaximumLength(1000)
            .WithMessage("Product description cannot exceed 1000 characters");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero")
            .LessThanOrEqualTo(999999.99m)
            .WithMessage("Unit price cannot exceed 999,999.99");

        RuleFor(x => x.Category)
            .NotEmpty()
            .WithMessage("Product category is required")
            .Length(2, 100)
            .WithMessage("Product category must be between 2 and 100 characters");
    }
}
