using Microsoft.EntityFrameworkCore;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Domain.Accounts;
using PawShelter.Accounts.Infrastructure.DbContexts;

namespace PawShelter.Accounts.Infrastructure.Seading;

public class AccountManager(AccountDbContext accountDbContext) : IAccountManager
{
    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        accountDbContext.Add(adminAccount);
        
        await accountDbContext.SaveChangesAsync();
    }
    public async Task CreateParticipantAccount(ParticipantAccount participantAccount)
    {
        accountDbContext.Add(participantAccount);
        
        await accountDbContext.SaveChangesAsync();
    }
    public async Task<bool> AdminAccountExists(CancellationToken cancellationToken = default)
    {
        return await accountDbContext.AdminAccounts.AnyAsync(cancellationToken);
    }
}