using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PawShelter.Core.Abstractions;
using PawShelter.SharedKernel;
using PawShelter.SharedKernel.Definitions;
using PawShelter.Species.Application;
using PawShelter.Species.Application.Species;
using PawShelter.Species.Infrastructure.DbContexts;
using PawShelter.Species.Infrastructure.Repositories;

namespace PawShelter.Species.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddSpeciesInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContexts().
            AddDatabase().
            AddRepositories();

        return services;
    }
    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISpeciesRepository, SpeciesRepository>();
        
        return services;
    }
    private static IServiceCollection AddDbContexts(this IServiceCollection services)
    {
        services.AddScoped<WriteDbContext>();
        services.AddScoped<IReadDbContext, ReadDbContext>();

        return services;
    }
    private static IServiceCollection AddDatabase(this IServiceCollection services)
    {
        services.AddKeyedScoped<IUnitOfWork, UnitOfWork>(ModulesName.Species);
        
        return services;
    }
}