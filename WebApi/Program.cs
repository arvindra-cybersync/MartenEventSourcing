using Infrastructure.Projections;
using Infrastructure.Repositories;
using Marten;
using Marten.Events.Daemon.Resiliency;
using Marten.Events.Projections;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MartenDb")
    ?? "Host=localhost;Port=5432;Database=MartenEventStore;Username=postgres;Password=Postgres@123";

builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);
    opts.AutoCreateSchemaObjects = AutoCreate.All;
    opts.Projections.Add<OrderSummaryProjection>(ProjectionLifecycle.Async);
})
.AddAsyncDaemon(DaemonMode.Solo);

builder.Services.AddScoped<OrderRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();
