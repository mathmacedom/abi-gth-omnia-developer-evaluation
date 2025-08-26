using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.CheckoutCart;

/// <summary>
/// Request model for checkout a cart by ID
/// </summary>
public class CheckoutCartRequest
{
    /// <summary>
    /// The unique identifier of the cart to be checked out
    /// </summary>
    [JsonIgnore]
    public Guid Id { get; set; }
}
