using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a cart/shopping cart in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// External identity reference for the customer.
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// Denormalized customer name.
    /// </summary>
    public string CustomerName { get; private set; } = string.Empty;

    /// <summary>
    /// Collection of items inside the cart
    /// </summary>
    public List<CartItem> Items { get; set; } = new();

    /// <summary>
    /// Status of the cart
    /// </summary>
    public CartStatus Status { get; set; }

    /// <summary>
    /// Creates a new cart instance.
    /// </summary>
    /// <param name="customerId">External customer ID</param>
    /// <param name="customerName">Customer name</param>
    /// <param name="items">Items added to the cart</param>
    /// <param name="status">Status of the cart</param>
    public Cart(Guid customerId, string customerName, List<CartItem> items, CartStatus status)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        Items = items;
        Status = status;
    }
}