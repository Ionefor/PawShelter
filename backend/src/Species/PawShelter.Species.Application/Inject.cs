﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Core.Abstractions;

namespace PawShelter.Species.Application;

public static class Inject
{
    public static IServiceCollection AddSpeciesApplication(this IServiceCollection services)
    {
        services.
            AddCommands().
            AddQueries().
            AddValidatorsFromAssembly(typeof(Inject).Assembly);
        
        return services;
    }

    private static IServiceCollection AddCommands(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly).
            AddClasses(classes => classes.
                AssignableToAny(typeof(ICommandHandler<,>), typeof(ICommandHandler<>))).
            AsSelfWithInterfaces().
            WithScopedLifetime());
        
        return services;
    }
    
    private static IServiceCollection AddQueries(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly).
            AddClasses(classes => classes.
                AssignableTo(typeof(IQueryHandler<,>))).
            AsSelfWithInterfaces().
            WithScopedLifetime());
        
        return services;
    }
}