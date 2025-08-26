namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleItemCancelledEvent
{
    public Guid SaleId { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public Guid ItemId { get; set; }
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Total { get; set; }
    public DateTime CancelledAt { get; set; }
}