using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StoreDefinition.Infrastructure.Persistence;
using StoreDefinition.Infrastructure.Persistence.Interceptors;
using StoreDefinition.Infrastructure.Services;

namespace StoreDefinition.Infrastructure.Extension;
public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<StoreDefinitionDbContext>(options =>
        {
            services.AddScoped<IDomainEventPublisherService, DomainEventPublisherService>();
            services.AddScoped<DomainEventPublisherInterceptor>();

            services.AddDbContext<StoreDefinitionDbContext>(
                (sp, opt) =>
                    opt.UseSqlServer(connectionString: configuration.GetConnectionString("StoreDefinition"))
            .AddInterceptors(sp.GetRequiredService<DomainEventPublisherInterceptor>()));

            options.ConfigureWarnings(warnings =>
            {
                warnings.Log(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning);
            });

        });
        return services;
    }
}
