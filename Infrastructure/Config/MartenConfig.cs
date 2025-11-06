using JasperFx;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using Weasel.Core;
using Weasel.Postgresql; 

namespace Infrastructure.Config;

public static class MartenConfig
{
    public static IServiceCollection AddMartenConfig(this IServiceCollection services, string connectionString)
    {
        services.AddMarten(options =>
        {
            options.Connection(connectionString);

            // Auto-create database schema objects for Marten
            options.AutoCreateSchemaObjects = AutoCreate.All; 
        });

        return services;
    }
}
