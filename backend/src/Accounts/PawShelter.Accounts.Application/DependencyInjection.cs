﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Core.Abstractions;

namespace PawShelter.Accounts.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsApplication(this IServiceCollection services)
    {
        services.
            AddCommands().
            AddQueries().
            AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly).
            AddClasses(classes => classes.
                AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>))).
            AsSelfWithInterfaces().
            WithScopedLifetime());
        
        return services;
    }
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(DependencyInjection).Assembly).
            AddClasses(classes => classes.
                AssignableTo(typeof(IQueryHandler<,>))).
            AsSelfWithInterfaces().
            WithScopedLifetime());
        
        return services;
    }
}