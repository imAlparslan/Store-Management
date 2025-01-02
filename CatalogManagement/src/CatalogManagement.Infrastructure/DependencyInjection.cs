using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Infrastructure.Persistence;
using CatalogManagement.Infrastructure.Persistence.Interceptors;
using CatalogManagement.Infrastructure.Repositories;
using CatalogManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogManagement.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IDomainEventPublisherService, DomainEventPublisherService>();
        services.AddScoped<DomainEventPublisher>();

        services.AddDbContext<CatalogDbContext>(
            (sp, opt) =>
                opt.UseSqlServer(
                    connectionString: configuration.GetConnectionString("MSSQL"),
                    sqlOption => sqlOption.EnableRetryOnFailure())
            .AddInterceptors(sp.GetRequiredService<DomainEventPublisher>())
            );

        services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductGroupRepository, ProductGroupRepository>();

        return services;
    }
}
