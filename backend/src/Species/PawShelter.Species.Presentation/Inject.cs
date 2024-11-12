using Microsoft.Extensions.DependencyInjection;
using PawShelter.Species.Contracts;

namespace PawShelter.Species.Presentation;

public static class Inject
{
    public static IServiceCollection AddSpeciesPresentation(this IServiceCollection services)
    {
        return services.AddScoped<ISpeciesContract, SpeciesContract>();
    }
}