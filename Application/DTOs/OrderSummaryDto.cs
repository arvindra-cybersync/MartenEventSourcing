namespace Application.DTOs;

/// <summary>
/// Simple read model for displaying order summaries.
/// </summary>
public class OrderSummaryDto
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public int TotalItems { get; set; }
    public bool IsShipped { get; set; }
    public DateTime CreatedAt { get; set; }
}
