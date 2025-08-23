using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

/// <summary>
/// Validator for <see cref="CreateSaleRequest"/> that defines validation rules for sale creation.
/// </summary>
public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - CustomerId: Required and cannot be empty
    /// - BranchId: Required and cannot be empty
    /// - ProductIds: Must not be empty and all IDs must be valid GUIDs
    /// - TotalAmount: Must be greater than zero
    /// </remarks>
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required");

        RuleFor(sale => sale.BranchId)
            .NotEmpty().WithMessage("BranchId is required");

        RuleFor(sale => sale.ProductIds)
            .NotEmpty().WithMessage("ProductIds cannot be empty")
            .Must(productIds => productIds.All(id => id != Guid.Empty))
            .WithMessage("All ProductIds must be valid GUIDs");

        RuleFor(sale => sale.TotalAmount)
            .GreaterThan(0).WithMessage("TotalAmount must be greater than zero");
    }
}