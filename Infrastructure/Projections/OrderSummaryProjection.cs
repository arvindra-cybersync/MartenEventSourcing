using Domain.Events;
using Marten;
using Marten.Events.Aggregation;

namespace Infrastructure.Projections;

/// <summary>
/// Async projection that builds and maintains an OrderSummary read model from events.
/// Compatible with Marten 7.x / 8.x.
/// </summary>
public class OrderSummaryProjection : SingleStreamProjection<OrderSummary>
{
    public OrderSummaryProjection()
    {
        ProjectionName = "order_summary";
    }

    public void Apply(OrderSummary view, OrderCreated evt)
    {
        view.Id = evt.OrderId;
        view.CustomerId = evt.CustomerId;
        view.CustomerName = evt.CustomerName;
        view.TotalItems = 0;
        view.IsShipped = false;
    }

    public void Apply(OrderSummary view, ItemAdded evt)
    {
        view.TotalItems += evt.Quantity;
    }

    public void Apply(OrderSummary view, OrderShipped evt)
    {
        view.IsShipped = true;
    }
}
