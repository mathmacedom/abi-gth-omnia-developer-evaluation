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
    /// Branch where the cart was created.
    /// </summary>
    public string Branch { get; private set; } = string.Empty;

    /// <summary>
    /// External identity reference for the customer.
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// Collection of items inside the cart
    /// </summary>
    public List<CartItem> Items { get; set; } = new();

    /// <summary>
    /// Status of the cart
    /// </summary>
    public CartStatus Status { get; set; }

    public Cart()
    {
        
    }

    /// <summary>
    /// Creates a new cart instance.
    /// </summary>
    /// <param name="branch">Branch name</param>
    /// <param name="customerId">External customer ID</param>
    /// <param name="customerName">Customer name</param>
    /// <param name="items">Items added to the cart</param>
    /// <param name="status">Status of the cart</param>
    public Cart(string branch, Guid customerId, List<CartItem> items, CartStatus status)
    {
        Branch = branch;
        CustomerId = customerId;
        Items = items;
        Status = status;
    }

    /// <summary>
    /// Updates the status of the cart.
    /// </summary>
    /// <param name="status">The status to be updated</param>
    public void UpdateStatus(CartStatus status)
    {
        Status = status;
    }
}