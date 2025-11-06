namespace Domain.Events;

/// <summary>Raised when the order has been shipped.</summary>
public class OrderShipped
{
    public Guid OrderId { get; set; }
    public DateTime ShippedAt { get; set; }
}
