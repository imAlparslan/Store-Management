using CatalogManagement.Application.Common;
using CatalogManagement.Infrastructure.Persistence;
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
        services.AddDbContext<CatalogDbContext>(
            opt => 
                opt.UseSqlServer(connectionString: configuration.GetConnectionString("MSSQL")
            ));

        services.AddScoped<IUnitOfWorkManager, UnitOfWorkManager>();
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}
