using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Represents the response returned after successfully retrieving a cart.
/// </summary>
public class GetCartResult
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string Branch { get; set; } = string.Empty;
    public List<CartItemResult> Items { get; set; } = new();
    public CartStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CartItemResult
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal Subtotal { get; set; }
}