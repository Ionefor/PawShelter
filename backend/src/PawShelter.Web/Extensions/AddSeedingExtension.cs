using PawShelter.Accounts.Infrastructure.Seading;

namespace PawShelter.Web.Extensions;

public static class AddSeedingExtension
{
    public static async void AddSeeding(this IServiceProvider provider)
    {
        var accountSeeder = provider.GetRequiredService<AccountSeeder>();

        await accountSeeder.SeedAsync();
    }
}