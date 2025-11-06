namespace Domain.Events;

/// <summary>Raised when a new order is created.</summary>
public class OrderCreated
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
