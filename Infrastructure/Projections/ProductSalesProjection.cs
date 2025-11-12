using Marten.Events.Projections;
using Domain.Events;
using Infrastructure.ReadModels;

namespace Infrastructure.Projections;

public class ProductSalesProjection : MultiStreamProjection<ProductSales, Guid>
{
    public ProductSalesProjection()
    {
        Identity<ItemAdded>(e => e.ItemId);

        ProjectEvent<ItemAdded>((view, @event) =>
        {
            view.Id = @event.ItemId;
            view.ProductName = string.IsNullOrEmpty(view.ProductName) ? @event.Item : view.ProductName;
            view.TotalQuantitySold += @event.Quantity;
            view.LastSaleAt = @event.OccurredAt == default
                ? DateTime.UtcNow : @event.OccurredAt;
        });
    }
}
