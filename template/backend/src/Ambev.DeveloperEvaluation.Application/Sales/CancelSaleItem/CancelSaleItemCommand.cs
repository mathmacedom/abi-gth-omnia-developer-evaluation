using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Command for deleting a user
/// </summary>
public record CancelSaleItemCommand : IRequest<CancelSaleItemResponse>
{
    /// <summary>
    /// Gets or sets the external identifier of the sale from which the product will be cancelled
    /// </summary>
    public Guid SaleId { get; set; }

    /// <summary>
    /// Gets or sets the external identifier of the sale item to be cancelled
    /// </summary>
    public Guid SaleItemId { get; set; }

    /// <summary>
    /// Executes validation rules against the command data.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CancelSaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
