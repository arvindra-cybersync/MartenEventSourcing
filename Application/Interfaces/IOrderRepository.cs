using Domain.Entities;

namespace Application.Interfaces;

public interface IOrderRepository
{
    Task<Order> GetAsync(Guid id, CancellationToken ct);
    Task SaveAsync(Order order, CancellationToken ct);
}
