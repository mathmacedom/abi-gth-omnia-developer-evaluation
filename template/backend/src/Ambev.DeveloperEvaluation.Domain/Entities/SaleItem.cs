using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a product item within a sale, including discounts and calculated totals.
/// </summary>
public class SaleItem : BaseEntity
{
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Total => (Quantity * UnitPrice) - Discount;
    public bool IsCancelled { get; private set; }

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
    }
}
