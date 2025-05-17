using InventoryManagement.Application.Common;
using InventoryManagement.Infrastructure.Persistence;
using InventoryManagement.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreDefinitionProtos;
using System.Reflection;

namespace InventoryManagement.Infrastructure.Extensions;
public static class DependenctInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddDatabase(configuration);
        services.AddMessaging(configuration);
        services.AddGrpcClients(configuration);

        return services;
    }
    private static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpcClient<StoreDefinitionGrpc.StoreDefinitionGrpcClient>(opt =>
        {
            opt.Address = new Uri("https://localhost:7261");
        });

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

        services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();

        return services;
    }
}
