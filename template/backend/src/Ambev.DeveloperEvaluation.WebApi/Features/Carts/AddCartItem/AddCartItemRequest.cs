using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.AddCartItem;

/// <summary>
/// Request model for updating a sale
/// </summary>
public class AddCartItemRequest
{
    /// <summary>
    /// The unique identifier of the cart
    /// </summary>
    [JsonIgnore]
    public Guid CartId { get; set; }

    /// <summary>
    /// The unique identifier of the product to be added
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// The quantity of the product to be added to cart.
    /// </summary>
    public int Quantity { get; set; }
}
