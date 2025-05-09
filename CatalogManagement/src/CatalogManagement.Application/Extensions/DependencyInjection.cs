﻿using CatalogManagement.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CatalogManagement.Application.Extensions;
public static class DependencyInjection
{

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

        services.AddMediatR(conf =>
        {
            conf.AddOpenBehavior(typeof(CommandValidationBehavior<,>));
            conf.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }

}
