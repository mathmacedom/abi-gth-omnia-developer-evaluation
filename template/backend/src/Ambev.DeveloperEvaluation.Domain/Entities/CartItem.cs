using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a cart item/shopping cart item in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class CartItem : BaseEntity
{
    /// <summary>
    /// External identity reference for the cart.
    /// </summary>
    public Guid CartId { get; private set; }

    /// <summary>
    /// External identity reference for the product.
    /// </summary>
    public Guid ProductId { get; private set; }

    /// <summary>
    /// Denormalized product name.
    /// </summary>
    public string ProductName { get; private set; } = string.Empty;

    /// <summary>
    /// Quantity of the product added to the cart
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// The unit price of the item.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// The subtotal price for this item (quantity * unit price).
    /// </summary>
    public decimal Subtotal { get; private set; }

    /// <summary>
    /// Creates a new cart item instance.
    /// </summary>
    /// <param name="cartId">External cart ID</param>
    /// <param name="productId">External product ID</param>
    /// <param name="productName">Product name</param>
    /// <param name="quantity">Quantity of the product added to the cart</param>
    /// <param name="unitPrice">The unit price of the item</param>
    public CartItem(Guid cartId, Guid productId, string productName, int quantity, decimal unitPrice)
    {
        CartId = cartId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Subtotal = UnitPrice * Quantity;
    }
}