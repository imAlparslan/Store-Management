using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Application.Common.Behaviors;
using System.Reflection;

namespace InventoryManagement.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            cfg.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
        return services;
    }
}