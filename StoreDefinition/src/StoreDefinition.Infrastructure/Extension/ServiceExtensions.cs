using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreDefinition.Infrastructure.Persistence;

namespace StoreDefinition.Infrastructure.Extension;
public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDefinitionDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("StoreDefinition"));

            options.ConfigureWarnings(warnings =>
            {
                warnings.Log(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning);
            });

        });
        return services;
    }
}
