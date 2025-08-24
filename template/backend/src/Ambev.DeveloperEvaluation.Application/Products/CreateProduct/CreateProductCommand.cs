using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Command for creating a new product.
/// </summary>
/// <remarks>
/// This command captures the required data for creating a product
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request
/// that returns a <see cref="CreateProductResult"/>.
/// 
/// The data provided in this command is validated using the
/// <see cref="CreateProductCommandValidator"/> which ensures that the fields
/// are correctly populated and follow the required rules.
/// </remarks>
public class CreateProductCommand : IRequest<CreateProductResult>
{
    /// <summary>
    /// Gets or sets the product name
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the product description
    /// </summary>
    public string Description { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets the unit price of the product
    /// </summary>
    public decimal UnitPrice { get; init; }

    /// <summary>
    /// Gets or sets the product category
    /// </summary>
    public string Category { get; init; } = string.Empty;

    /// <summary>
    /// Gets or sets whether the product is active
    /// </summary>
    public bool IsActive { get; init; } = true;

    /// <summary>
    /// Executes validation rules against the command data.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CreateProductCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
