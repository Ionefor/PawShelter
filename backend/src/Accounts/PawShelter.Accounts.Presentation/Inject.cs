using Microsoft.Extensions.DependencyInjection;

namespace PawShelter.Accounts.Presentation;

public static class Inject
{
    public static IServiceCollection AddAccountsPresentation(this IServiceCollection services)
    {
       // return services.AddScoped<ISpeciesContract, Accounts>();
       return services;
    }
}