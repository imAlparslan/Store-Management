using InventoryManagement.Application.Common;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InventoryManagement.Infrastructure.Extensions;
public static class DependenctInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDatabase(configuration);
        services.AddMessaging(configuration);


        return services;
    }
    private static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            //x.AddConsumer<ShopCreatedIntegrationEventConsumer>();
            x.AddConsumers(Assembly.GetExecutingAssembly());

            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration.GetConnectionString("RabbitMq"));
                cfg.ConfigureEndpoints(context);
            });
        });

        //   services.AddScoped<IEventPublisher, EventPublisher>();
        return services;
    }

    private static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(
            options =>
            {
                options.UseSqlServer(
                    connectionString: configuration.GetConnectionString("InventoryManagement"),
                    sqlOption => sqlOption.EnableRetryOnFailure())
                    .ConfigureWarnings(configuration => configuration.Log(RelationalEventId.PendingModelChangesWarning));
            });
        
        services.AddScoped<IStockRepository, StockRepository>();

        return services;
    }
}
