namespace Domain.Events;

/// <summary>Raised when an item is added to an order.</summary>
public class ItemAdded
{
    public Guid OrderId { get; set; }
    public Guid ItemId { get; set; }
    public string Item { get; set; } = string.Empty;
    public int Quantity { get; set; }
}
