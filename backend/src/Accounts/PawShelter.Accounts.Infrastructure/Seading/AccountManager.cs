using Microsoft.EntityFrameworkCore;
using PawShelter.Accounts.Application.Abstractions;
using PawShelter.Accounts.Domain.Accounts;
using PawShelter.Accounts.Infrastructure.DbContexts;

namespace PawShelter.Accounts.Infrastructure.Seading;

public class AccountManager(AccountsWriteDbContext accountsWriteDbContext) : IAccountManager
{
    public async Task CreateAdminAccount(AdminAccount adminAccount)
    {
        accountsWriteDbContext.Add(adminAccount);
        
        await accountsWriteDbContext.SaveChangesAsync();
    }
    public async Task CreateParticipantAccount(ParticipantAccount participantAccount)
    {
        accountsWriteDbContext.Add(participantAccount);
        
        await accountsWriteDbContext.SaveChangesAsync();
    }
    public async Task<bool> AdminAccountExists(CancellationToken cancellationToken = default)
    {
        return await accountsWriteDbContext.AdminAccounts.AnyAsync(cancellationToken);
    }
}