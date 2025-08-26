using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem;

/// <summary>
/// Validator for <see cref="CancelSaleItemRequest"/> that defines validation rules for sale deleting.
/// </summary>
public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
{
    /// <summary>
    /// Initializes a new instance of the CancelSaleItemRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleId: Required and cannot be empty
    /// - SaleItemId: Required and cannot be empty
    /// </remarks>
    public CancelSaleItemRequestValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty().WithMessage("Sale ID is required");

        RuleFor(x => x.SaleItemId)
            .NotEmpty().WithMessage("Sale Item ID is required");
    }
}
