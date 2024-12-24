using CatalogManagement.Application.Common.Repositories;
using CatalogManagement.Infrastructure.Persistence;
using CatalogManagement.Infrastructure.Persistence.Interceptors;
using CatalogManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CatalogManagement.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<DomainEventPublisher>();

        services.AddDbContext<CatalogDbContext>(
            (sp, opt) =>
                opt.UseSqlServer(connectionString: configuration.GetConnectionString("MSSQL"))
                .AddInterceptors(sp.GetRequiredService<DomainEventPublisher>())
            );

        services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductGroupRepository, ProductGroupRepository>();

        return services;
    }
}
