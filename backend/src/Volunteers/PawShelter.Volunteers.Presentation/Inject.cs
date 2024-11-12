using Microsoft.Extensions.DependencyInjection;
using PawShelter.Volunteers.Contracts;

namespace PawShelter.Volunteers.Presentation;

public static class Inject
{
    public static IServiceCollection AddVolunteersPresentation(this IServiceCollection services)
    {
        return services.AddScoped<IVolunteersContract, VolunteersContract>();
    }
}