using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Validator for CancelSaleItemCommand
/// </summary>
public class CancelSaleItemValidator : AbstractValidator<CancelSaleItemCommand>
{
    /// <summary>
    /// Initializes validation rules for CancelSaleItemCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - SaleId: Required and cannot be empty
    /// - SaleItemId: Required and cannot be empty
    /// </remarks>
    public CancelSaleItemValidator()
    {
        RuleFor(x => x.SaleId)
            .NotEmpty()
            .WithMessage("Sale ID is required");

        RuleFor(x => x.SaleItemId)
            .NotEmpty()
            .WithMessage("Sale Item ID is required");
    }
}
