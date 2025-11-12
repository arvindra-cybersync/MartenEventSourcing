using Domain.Aggregates;
using Marten;

namespace Application.Services;

/// <summary>
/// Demonstrates snapshot loading from the event store.
/// </summary>
public class OrderService
{
    private readonly IDocumentSession _session;

    public OrderService(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<OrderAggregate?> GetOrderAsync(Guid orderId, CancellationToken ct = default)
    {
        // Load current state from event stream (snapshot building)
        return await _session.Events.AggregateStreamAsync<OrderAggregate>(orderId, token: ct);
    }

    public async Task AddItemAsync(Guid orderId, Guid itemId, string item, int qty, CancellationToken ct = default)
    {
        var evt = new Domain.Events.ItemAdded
        {
            OrderId = orderId,
            ItemId = itemId,
            Item = item,
            Quantity = qty,
            OccurredAt = DateTime.UtcNow
        };

        _session.Events.Append(orderId, evt);
        await _session.SaveChangesAsync(ct);
    }
}
