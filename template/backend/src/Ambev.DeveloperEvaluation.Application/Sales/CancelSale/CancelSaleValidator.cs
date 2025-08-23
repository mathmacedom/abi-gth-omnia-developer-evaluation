using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Validator for CancelSaleCommand
/// </summary>
public class CancelSaleValidator : AbstractValidator<CancelSaleCommand>
{
    /// <summary>
    /// Initializes validation rules for CancelSaleCommand
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Required and cannot be empty
    /// </remarks>
    public CancelSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}
