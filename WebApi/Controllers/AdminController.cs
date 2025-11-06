// WebApi/Controllers/AdminController.cs
using Microsoft.AspNetCore.Mvc;
using Marten;
using Marten.Events.Daemon;
using Infrastructure.Projections; 
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IDocumentStore _store;

    public AdminController(IDocumentStore store)
    {
        _store = store;
    }

    [HttpPost("projections/rebuild")]
    public async Task<IActionResult> RebuildProjections(CancellationToken ct)
    {
        // Build the projection daemon runtime
        var daemon = await _store.BuildProjectionDaemonAsync();

        // Rebuild your specific projection class
        await daemon.RebuildProjectionAsync(typeof(OrderSummaryProjection), ct);

        return Ok(new
        {
            message = "Projection rebuild triggered successfully",
            projection = nameof(OrderSummaryProjection),
            time = DateTime.UtcNow
        });
    }

    [HttpGet("projections/status")]
    public IActionResult Status()
    {
        return Ok(new { daemon = "ready", time = DateTime.UtcNow });
    }
}
