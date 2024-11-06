using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Application.Abstractions;
using PawShelter.Application.Volunteers.Queries.GetVolunteersWithPagination;
using PawShelter.Application.Volunteers.UseCases.AddPet;
using PawShelter.Application.Volunteers.UseCases.AddPetPhotos;
using PawShelter.Application.Volunteers.UseCases.Create;
using PawShelter.Application.Volunteers.UseCases.Delete;
using PawShelter.Application.Volunteers.UseCases.UpdateMainInfo;
using PawShelter.Application.Volunteers.UseCases.UpdateRequisites;
using PawShelter.Application.Volunteers.UseCases.UpdateSocialNetworks;

namespace PawShelter.Application;

public static class Inject
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.
            AddCommands().
            AddQuries().
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
    
    private static IServiceCollection AddQuries(this IServiceCollection services)
    {
        services.Scan(scan => scan.FromAssemblies(typeof(Inject).Assembly).
            AddClasses(classes => classes.
                AssignableTo(typeof(IQueryHandler<,>))).
            AsSelfWithInterfaces().
            WithScopedLifetime());
        
        return services;
    }
}