using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.CheckoutCart;

/// <summary>
/// Command for creating a new cart.
/// </summary>
/// <remarks>
/// This command captures the required data for creating a cart
/// It implements <see cref="IRequest{TResponse}"/> to initiate the request
/// that returns a <see cref="GetSaleResult"/>.
/// 
/// The data provided in this command is validated using the
/// <see cref="CheckoutCartCommandValidator"/> which ensures that the fields
/// are correctly populated and follow the required rules.
/// </remarks>
public class CheckoutCartCommand : IRequest<GetSaleResult>
{
    /// <summary>
    /// Gets or sets the external identifier of the cart to be checked out.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Executes validation rules against the command data.
    /// </summary>
    /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
    public ValidationResultDetail Validate()
    {
        var validator = new CheckoutCartCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
