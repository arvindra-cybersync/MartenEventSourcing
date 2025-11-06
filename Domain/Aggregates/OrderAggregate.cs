using Domain.Events;

namespace Domain.Aggregates;

/// <summary>Aggregate root for an Order. Handles event application logic.</summary>
public class OrderAggregate
{
    public Guid Id { get; private set; }
    public Guid CustomerId { get; private set; }
    public string CustomerName { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; }
    public bool IsShipped { get; private set; }
    public List<(Guid ItemId, string Item, int Quantity)> Items { get; } = new();

    // Create new order aggregate
    public static OrderAggregate Create(Guid orderId, Guid customerId, string customerName)
    {
        var aggregate = new OrderAggregate();
        var evt = new OrderCreated
        {
            OrderId = orderId,
            CustomerId = customerId,
            CustomerName = customerName,
            CreatedAt = DateTime.UtcNow
        };
        aggregate.Apply(evt);
        return aggregate;
    }

    public void AddItem(Guid itemId, string item, int quantity)
    {
        if (IsShipped)
            throw new InvalidOperationException("Cannot add items to a shipped order.");

        var evt = new ItemAdded
        {
            OrderId = Id,
            ItemId = itemId,
            Item = item,
            Quantity = quantity
        };
        Apply(evt);
    }

    public void Ship()
    {
        if (IsShipped)
            throw new InvalidOperationException("Order already shipped.");

        var evt = new OrderShipped
        {
            OrderId = Id,
            ShippedAt = DateTime.UtcNow
        };
        Apply(evt);
    }

    // Apply event handlers
    private void Apply(OrderCreated evt)
    {
        Id = evt.OrderId;
        CustomerId = evt.CustomerId;
        CustomerName = evt.CustomerName;
        CreatedAt = evt.CreatedAt;
    }

    private void Apply(ItemAdded evt)
    {
        Items.Add((evt.ItemId, evt.Item, evt.Quantity));
    }

    private void Apply(OrderShipped evt)
    {
        IsShipped = true;
    }
}
