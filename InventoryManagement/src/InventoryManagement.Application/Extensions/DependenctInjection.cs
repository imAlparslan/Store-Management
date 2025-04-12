using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Application.Extensions;
public static class DependenctInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }
}
