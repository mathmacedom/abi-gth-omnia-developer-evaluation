using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for <see cref="UpdateSaleCommand"/> that defines validation rules for sale creation.
/// </summary>
/// <remarks>
/// Validation rules include:
/// - CustomerId: Must not be empty
/// - BranchId: Must not be empty
/// - Items: Must contain at least one item
/// - Quantity: Must be greater than zero
/// - UnitPrice: Must be greater than zero
/// </remarks>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required");

        RuleFor(sale => sale.BranchId)
            .NotEmpty().WithMessage("BranchId is required");

        RuleFor(sale => sale.Items)
            .NotEmpty().WithMessage("At least one sale item is required");

        RuleForEach(sale => sale.Items).ChildRules(items =>
        {
            items.RuleFor(i => i.ProductId)
                .NotEmpty().WithMessage("ProductId is required");

            items.RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero");

            items.RuleFor(i => i.UnitPrice)
                .GreaterThan(0).WithMessage("Unit price must be greater than zero");
        });
    }
}