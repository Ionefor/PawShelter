using Microsoft.Extensions.DependencyInjection;

namespace PawShelter.Accounts.Infrastructure.Seading;

public class AccountSeeder(IServiceScopeFactory scopeFactory)
{
    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        using var scope = scopeFactory.CreateScope();

        var service = scope.ServiceProvider.GetRequiredService<AccountsSeederService>();

        await service.SeedAsync(cancellationToken);
    }
}