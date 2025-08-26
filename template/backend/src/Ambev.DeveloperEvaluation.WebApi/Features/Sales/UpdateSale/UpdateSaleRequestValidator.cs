using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

/// <summary>
/// Validator for <see cref="UpdateSaleRequest"/> that defines validation rules for sale update.
/// </summary>
public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Required and cannot be empty
    /// - ProductIds: Must not be empty and all IDs must be valid GUIDs
    /// - TotalAmount: Must be greater than zero
    /// </remarks>
    public UpdateSaleRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Sale ID is required");

        RuleFor(sale => sale.ProductIds)
            .NotEmpty().WithMessage("ProductIds cannot be empty")
            .Must(productIds => productIds.All(id => id != Guid.Empty))
            .WithMessage("All ProductIds must be valid GUIDs");

        RuleFor(x => x.TotalAmount)
            .GreaterThan(0).WithMessage("TotalAmount must be greater than zero");
    }
}
