using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreDefinition.Application.Common.Repositories;
using StoreDefinition.Application.Services;
using StoreDefinition.Infrastructure.Persistence;
using StoreDefinition.Infrastructure.Persistence.Interceptors;
using StoreDefinition.Infrastructure.Repositories;
using StoreDefinition.Infrastructure.Services;

namespace StoreDefinition.Infrastructure.Extension;
public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDomainEventPublisherService, DomainEventPublisherService>();
        services.AddScoped<DomainEventPublisherInterceptor>();

        services.AddMessaging(configuration);

        services.AddDbContext<StoreDefinitionDbContext>(
            (sp, opt) =>
                opt.UseSqlServer(
                    connectionString: configuration.GetConnectionString("StoreDefinition"),
                    sqlOption => sqlOption.EnableRetryOnFailure())
                        .ConfigureWarnings
                        (
                            warnings => warnings.Log(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)
                        )
        .AddInterceptors(sp.GetRequiredService<DomainEventPublisherInterceptor>()));

        services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();

        return services;
    }

    private static IServiceCollection AddMessaging(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host("localhost", "/", h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
        });

        services.AddScoped<IEventPublisher, EventPublisher>();
        return services;
    }
}
