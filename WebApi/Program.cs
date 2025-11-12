using Infrastructure.Projections;
using Infrastructure.Repositories;
using JasperFx;
using JasperFx.Events.Daemon;
using JasperFx.Events.Projections;
using Marten;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MartenDb")
    ?? "Host=localhost;Port=5432;Database=MartenEventStore;Username=postgres;Password=Postgres@123";



builder.Services.AddMarten(opts =>
{
    opts.Connection(connectionString);
    opts.AutoCreateSchemaObjects = AutoCreate.All;

    // Add projections
    opts.Projections.Add<OrderSummaryProjection>(ProjectionLifecycle.Async);
    opts.Projections.Add<ProductSalesProjection>(ProjectionLifecycle.Async);
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
