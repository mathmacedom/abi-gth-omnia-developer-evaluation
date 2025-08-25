namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCartItem;

/// <summary>
/// Request model for deleting a cart item
/// </summary>
public class DeleteCartItemRequest
{
    /// <summary>
    /// The unique identifier of the cart
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// The unique identifier of the cart item to be updated
    /// </summary>
    public Guid CartItemId { get; set; }
}
