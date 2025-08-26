namespace Ambev.DeveloperEvaluation.Domain.Events;

public class SaleModifiedEvent
{
    public Guid SaleId { get; set; }
    public DateTime ModifiedAt { get; set; }
    public List<SaleItemModifiedEvent> ModifiedItems { get; set; } = [];
}

public class SaleItemModifiedEvent
{
    public Guid ItemId { get; set; }
    public int OldQuantity { get; set; }
    public int NewQuantity { get; set; }
    public decimal OldDiscount { get; set; }
    public decimal NewDiscount { get; set; }
    public decimal OldTotal { get; set; }
    public decimal NewTotal { get; set; }
}
