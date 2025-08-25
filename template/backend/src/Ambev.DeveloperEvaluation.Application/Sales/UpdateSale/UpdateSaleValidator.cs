using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for <see cref="UpdateSaleCommand"/> that defines validation rules for sale creation.
/// </summary>
public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateSaleCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Id: Must not be empty
    /// - CustomerId: Must not be empty
    /// - Items: Must contain at least one item
    /// - Item Quantity: Must be greater than zero
    /// - Item UnitPrice: Must be greater than zero
    /// </remarks>
    public UpdateSaleCommandValidator()
    {
        RuleFor(sale => sale.Id)
            .NotEmpty().WithMessage("Sale Id is required");

        RuleFor(sale => sale.CustomerId)
            .NotEmpty().WithMessage("CustomerId is required");

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