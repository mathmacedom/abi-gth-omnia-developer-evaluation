using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product item within a sale, including discounts and calculated totals.
/// </summary>
public class SaleItem : BaseEntity
{
    /// <summary>
    /// External identity reference for the sale.
    /// </summary>
    public Guid SaleId { get; private set; }

    /// <summary>
    /// External identity reference for the product.
    /// </summary>
    public Guid ProductId { get; private set; }

    /// <summary>
    /// Denormalized product name.
    /// </summary>
    public string ProductName { get; private set; } = string.Empty;

    /// <summary>
    /// Quantity of the product sold
    /// </summary>
    public int Quantity { get; private set; }

    /// <summary>
    /// The unit price of the item.
    /// </summary>
    public decimal UnitPrice { get; private set; }

    /// <summary>
    /// The discount applied to this item based on quantity tiers.
    /// </summary>
    public decimal Discount { get; private set; }

    /// <summary>
    /// The total price for this item after applying the discount.
    /// </summary>
    public decimal Total { get; private set; }

    /// <summary>
    /// Indicates whether the item has been cancelled.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// Date and time when the sale was cancelled.
    /// </summary>
    public DateTime? CancelledAt { get; set; }

    public SaleItem()
    {
        
    }

    /// <summary>
    /// Creates a new sale item instance.
    /// </summary>
    /// <param name="productId">External product ID</param>
    /// <param name="productName">Product name</param>
    /// <param name="quantity">Quantity of the product sold</param>
    /// <param name="unitPrice">The unit price of the item</param>
    public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        if (quantity < 1)
            throw new ArgumentException("Quantity must be greater than zero.");

        if (quantity > 20)
            throw new ArgumentException("Cannot sell more than 20 identical items.");

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Total = (Quantity * UnitPrice) - Discount;

        ApplyDiscount();
    }

    private void ApplyDiscount()
    {
        if (Quantity >= 10 && Quantity <= 20)
        {
            //20% discount for 10 to 20 items
            Discount = Quantity * UnitPrice * 0.20m;
        }
        else if (Quantity >= 4)
        {
            //10% discount for 4 to 9 items
            Discount = Quantity * UnitPrice * 0.10m;
        }
        else
        {
            //No discount for less than 4 items
            Discount = 0;
        }
    }

    public void Cancel()
    {
        IsCancelled = true;
        CancelledAt = DateTime.UtcNow;
    }
}
