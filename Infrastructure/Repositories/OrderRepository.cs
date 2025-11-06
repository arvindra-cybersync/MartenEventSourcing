using Domain.Aggregates;
using Domain.Events;
using Marten;

namespace Infrastructure.Repositories;

/// <summary>Repository responsible for persisting and retrieving order events.</summary>
public class OrderRepository
{
    private readonly IDocumentSession _session;

    public OrderRepository(IDocumentSession session)
    {
        _session = session;
    }

    public async Task<Guid> CreateOrderAsync(Guid customerId, string customerName)
    {
        var orderId = Guid.NewGuid();

        var evt = new OrderCreated
        {
            OrderId = orderId,
            CustomerId = customerId,
            CustomerName = customerName,
            CreatedAt = DateTime.UtcNow
        };

        _session.Events.StartStream<OrderAggregate>(orderId, evt);
        await _session.SaveChangesAsync();
        return orderId;
    }

    public async Task AddItemAsync(Guid orderId, string item, int quantity)
    {
        var itemId = Guid.NewGuid();
        var evt = new ItemAdded
        {
            OrderId = orderId,
            ItemId = itemId,
            Item = item,
            Quantity = quantity
        };

        _session.Events.Append(orderId, evt);
        await _session.SaveChangesAsync();
    }

    public async Task ShipOrderAsync(Guid orderId)
    {
        var evt = new OrderShipped
        {
            OrderId = orderId,
            ShippedAt = DateTime.UtcNow
        };

        _session.Events.Append(orderId, evt);
        await _session.SaveChangesAsync();
    }
}
