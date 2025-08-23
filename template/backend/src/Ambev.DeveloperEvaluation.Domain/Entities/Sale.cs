using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// <summary>
/// Represents a sale transaction in the system, including customer, branch, items, and totals.
/// Follows DDD principles with validation and encapsulated business rules.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Unique sequential sale number.
    /// </summary>
    public string SaleNumber { get; private set; } = Guid.NewGuid().ToString("N")[..8].ToUpper();

    /// <summary>
    /// Date and time when the sale was made.
    /// </summary>
    public DateTime SaleDate { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// External identity reference for the customer.
    /// </summary>
    public Guid CustomerId { get; private set; }

    /// <summary>
    /// Denormalized customer name.
    /// </summary>
    public string CustomerName { get; private set; } = string.Empty;

    /// <summary>
    /// External identity reference for the branch.
    /// </summary>
    public Guid BranchId { get; private set; }

    /// <summary>
    /// Denormalized branch description.
    /// </summary>
    public string BranchName { get; private set; } = string.Empty;

    /// <summary>
    /// Collection of items sold in this transaction.
    /// </summary>
    public List<SaleItem> Items { get; private set; } = [];

    /// <summary>
    /// Indicates whether the sale is cancelled.
    /// </summary>
    public bool IsCancelled { get; private set; }

    /// <summary>
    /// Total sale amount (calculated from items).
    /// </summary>
    public decimal TotalAmount { get; private set; }

    /// <summary>
    /// Total sale discount (calculated from items).
    /// </summary>
    public decimal TotalDiscount { get; private set; }

    public Sale()
    {
    }

    /// <summary>
    /// Creates a new sale instance.
    /// </summary>
    /// <param name="customerId">External customer ID</param>
    /// <param name="customerName">Customer name</param>
    /// <param name="branchId">External branch ID</param>
    /// <param name="branchName">Branch name</param>
    public Sale(Guid customerId, string customerName, Guid branchId, string branchName)
    {
        CustomerId = customerId;
        CustomerName = customerName;
        BranchId = branchId;
        BranchName = branchName;
    }

    /// <summary>
    /// Adds an item to the sale applying business rules (discount tiers and limits).
    /// </summary>
    public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
    {
        if (IsCancelled)
            throw new InvalidOperationException("Cannot add items to a cancelled sale.");

        var item = new SaleItem(productId, productName, quantity, unitPrice);
        Items.Add(item);

        TotalAmount = Items.Sum(i => i.Total);
        TotalDiscount = Items.Sum(i => i.Discount);
    }

    /// <summary>
    /// Updates the sale and its items.
    /// </summary>
    public void UpdateSale(Guid branchId, Guid customerId, List<SaleItem> items)
    {
        CustomerId = customerId;
        BranchId = branchId;
        Items.Clear();
        TotalAmount = 0;
        TotalDiscount = 0;

        foreach (var item in Items)
        {
            Items.Add(item);
        }

        TotalAmount = Items.Sum(i => i.Total);
        TotalDiscount = Items.Sum(i => i.Discount);
    }

    /// <summary>
    /// Cancels the sale and all items.
    /// </summary>
    public void CancelSale()
    {
        if (IsCancelled) return;

        IsCancelled = true;
        foreach (var item in Items)
        {
            item.Cancel();
        }
    }

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
