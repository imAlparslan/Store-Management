using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StoreDefinition.Application.Common.Behaviors;
using System.Reflection;

namespace StoreDefinition.Application.Extensions;
public static class ServiceExtension
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddMediatR(
            opt =>
            {
                opt.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
                opt.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            });

        return services;
    }
}
