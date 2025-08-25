using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCartItem;

/// <summary>
/// Request model for updating a cart item
/// </summary>
public class UpdateCartItemRequest
{
    /// <summary>
    /// The unique identifier of the cart from which the product will be updated
    /// </summary>
    [JsonIgnore]
    public Guid CartId { get; set; }

    /// <summary>
    /// The unique identifier of the cart item to be updated
    /// </summary>
    [JsonIgnore]
    public Guid CartItemId { get; set; }

    /// <summary>
    /// The quantity of the product to be updated to cart.
    /// </summary>
    public int Quantity { get; set; }
}
