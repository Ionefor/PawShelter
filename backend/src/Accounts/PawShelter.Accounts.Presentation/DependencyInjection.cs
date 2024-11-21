using Microsoft.Extensions.DependencyInjection;
using PawShelter.Accounts.Contracts;
using PawShelter.Species.Contracts;

namespace PawShelter.Accounts.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddAccountsPresentation(this IServiceCollection services)
    {
       return services.AddScoped<IAccountsContract, AccountsContract>();
    }
}