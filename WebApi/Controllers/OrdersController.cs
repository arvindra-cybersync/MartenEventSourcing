using Infrastructure.Projections;
using Infrastructure.ReadModels;
using Infrastructure.Repositories;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly OrderRepository _repository;
    private readonly IQuerySession _querySession;

    public OrdersController(OrderRepository repository, IQuerySession querySession)
    {
        _repository = repository;
        _querySession = querySession;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Guid customerId, string customerName)
    {
        var orderId = await _repository.CreateOrderAsync(customerId, customerName);
        return Ok(new { orderId });
    }

    [HttpPost("{orderId}/add-item")]
    public async Task<IActionResult> AddItem(Guid orderId, string item, int quantity)
    {
        await _repository.AddItemAsync(orderId, item, quantity);
        return Ok();
    }

    [HttpPost("{orderId}/ship")]
    public async Task<IActionResult> Ship(Guid orderId)
    {
        await _repository.ShipOrderAsync(orderId);
        return Ok();
    }

    [HttpGet("{orderId}/summary")]
    public async Task<IActionResult> GetSummary(Guid orderId)
    {
        var summary = await _querySession.LoadAsync<OrderSummary>(orderId);
        return summary is null ? NotFound() : Ok(summary);
    }
}
