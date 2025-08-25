namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCartByCustomerId;

/// <summary>
/// Request model for getting a cart by customer ID
/// </summary>
public class GetCartByCustomerIdRequest
{
    /// <summary>
    /// The unique identifier of the customer's cart identifier to retrieve
    /// </summary>
    public Guid CustomerId { get; set; }
}
