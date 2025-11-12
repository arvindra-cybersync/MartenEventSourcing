using Microsoft.AspNetCore.Mvc;
using Infrastructure.ReadModels;
using Marten;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IQuerySession _querySession;

    public ProductsController(IQuerySession querySession)
    {
        _querySession = querySession;
    }

    [HttpGet("{id:guid}/sales")]
    public async Task<IActionResult> GetSales(Guid id)
    {
        var doc = await _querySession.LoadAsync<ProductSales>(id);
        if (doc == null) return NotFound();
        return Ok(doc);
    }

    [HttpGet("top")]
    public async Task<IActionResult> Top([FromQuery] int n = 10)
    {
        var list = await _querySession.Query<ProductSales>()
                                       .OrderByDescending(x => x.TotalQuantitySold)
                                       .Take(n)
                                       .ToListAsync();
        return Ok(list);
    }
}
