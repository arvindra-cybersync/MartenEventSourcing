namespace Infrastructure.Projections;

/// <summary>Read model built asynchronously from order events.</summary>
public class OrderSummary
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public int TotalItems { get; set; }
    public bool IsShipped { get; set; }
}
