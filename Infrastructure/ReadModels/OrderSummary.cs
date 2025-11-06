// Infrastructure/ReadModels/OrderSummary.cs
// Read model that will be built/updated by the async projection.
// Stored as a Marten document (JSON in Postgres).
namespace Infrastructure.ReadModels;

public class OrderSummary
{
    // Marten will treat 'Id' as document id by convention
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public int TotalItems { get; set; }
    public bool IsShipped { get; set; }
    public DateTime CreatedAt { get; set; }
}
